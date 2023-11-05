using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text;
using TailorMeWebApi.GraphQL.Types;
using TailorMeWebApi.Interfaces;
using TailorMeWebApi.Models;
using System.Net.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using TailorMeWebApi.Enums;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

namespace TailorMeWebApi.Services
{
    public class UserService : IUserInterface
    {
        private readonly DbContextClass dbContextClass;

        public UserService(DbContextClass dbContextClass)
        {
            this.dbContextClass = dbContextClass;
        }

        public async Task<List<User>> GetUsers()
        {

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InJoZWpxbmZyYXRldXFobXhrdXFyIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTkxMTM5OTIsImV4cCI6MjAxNDY4OTk5Mn0.O70lAfz5xieJRknm7HbL0lMHgl2xNLv1LzBJ3bbGicQ");

            HttpResponseMessage userResponse = await httpClient.GetAsync("https://rhejqnfrateuqhmxkuqr.supabase.co/functions/v1/get-users");
            HttpResponseMessage sizesResponse = await httpClient.GetAsync("https://rhejqnfrateuqhmxkuqr.supabase.co/functions/v1/get-sizes");

            var users = new List<User>();
            var sizes = new List<Sizes>();

            if (userResponse.IsSuccessStatusCode)
            {
                users = await userResponse.Content.ReadAsAsync<List<User>>();
            }

            if (sizesResponse.IsSuccessStatusCode)
            {
                sizes = await sizesResponse.Content.ReadAsAsync<List<Sizes>>();
            }

            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    var sizeToAdd = new List<BrandSize>();

                    if (user?.ChestMeasurement != null && user?.WaistMeasurement != null && user?.HipMeasurement != null)
                    {
                        foreach(var size in sizes)
                        {
                            var chestSize = string.Empty;
                            var waistSize = string.Empty;
                            var hipSize = string.Empty;

                            if (user?.ChestMeasurement >= size.ChestMinMeasurement && user?.ChestMeasurement <= size.ChestMaxMeasurement)
                            {
                                chestSize = size.SizeName;
                            }

                            if (user?.WaistMeasurement >= size.WaistMinMeasurement && user?.WaistMeasurement <= size.WaistMaxMeasurement)
                            {
                                waistSize = size.SizeName;
                            }

                            if (user?.HipMeasurement >= size.HipMinMeasurement && user?.HipMeasurement <= size.HipMaxMeasurement)
                            {
                                hipSize = size.SizeName;
                            }

                            string largestSizeString = string.Empty;

                            SizeEnum largestSize = new SizeEnum();

                            if (!string.IsNullOrEmpty(chestSize) && !string.IsNullOrEmpty(waistSize) && !string.IsNullOrEmpty(hipSize))
                            {
                                SizeEnum enumSize1 = (SizeEnum)Enum.Parse(typeof(SizeEnum), chestSize);
                                SizeEnum enumSize2 = (SizeEnum)Enum.Parse(typeof(SizeEnum), waistSize);
                                SizeEnum enumSize3 = (SizeEnum)Enum.Parse(typeof(SizeEnum), hipSize);

                                largestSize = (SizeEnum)Math.Max((int)enumSize1, Math.Max((int)enumSize2, (int)enumSize3));
                                largestSizeString = largestSize.ToString();

                                sizeToAdd.Add(new BrandSize(){ BrandName = size.BrandName, SizeName = largestSizeString });
                            }

                        }
                    }
                    user.Sizes = sizeToAdd;
                }
            }

            return users;
        }

        public async Task<bool> CreateUserAsync(CreateUserModel userModel)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InJoZWpxbmZyYXRldXFobXhrdXFyIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTkxMTM5OTIsImV4cCI6MjAxNDY4OTk5Mn0.O70lAfz5xieJRknm7HbL0lMHgl2xNLv1LzBJ3bbGicQ");

            string baseUrl = "https://rhejqnfrateuqhmxkuqr.supabase.co/functions/v1/create-user"; // Replace with your actual Edge function URL
            client.BaseAddress = new Uri(baseUrl);

            var parameters = new Dictionary<string, string> { { "email", userModel.Email ?? string.Empty }, { "username", $"{userModel.FirstName} {userModel.LastName}" }, { "chestMeasurement", userModel.ChestMeasurement.ToString() }, { "waistMeasurement", userModel.WaistMeasurement.ToString() }, { "hipMeasurement", userModel.HipMeasurement.ToString() } };
            var encodedContent = JsonConvert.SerializeObject(parameters);

            var content = new StringContent(encodedContent.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage request = await client.PostAsync("https://rhejqnfrateuqhmxkuqr.supabase.co/functions/v1/create-user", content);
            var response = await request.Content.ReadAsStringAsync();

            return response == "duplicate key value violates unique constraint \"UserTable_Email_key\"" || string.IsNullOrEmpty(response) ? false : true;
        }
    }
}
