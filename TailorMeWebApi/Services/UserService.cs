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

            HttpResponseMessage response = await httpClient.GetAsync("https://rhejqnfrateuqhmxkuqr.supabase.co/functions/v1/get-users");

            var users = new List<User>();

            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<List<User>>();
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
