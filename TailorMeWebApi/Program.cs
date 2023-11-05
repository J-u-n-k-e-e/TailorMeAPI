using Microsoft.EntityFrameworkCore;
using TailorMeWebApi;
using TailorMeWebApi.GraphQL.Mutations;
using TailorMeWebApi.GraphQL.Queries;
using TailorMeWebApi.GraphQL.Types;
using TailorMeWebApi.Interfaces;
using TailorMeWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContextClass>(o => o.UseInMemoryDatabase("GraphQlDemo"));

//GraphQL Config
builder.Services.AddGraphQLServer()
    .AddQueryType<UserQueries>()
    .AddMutationType<UserMutations>();

builder.Services.AddTransient<IUserInterface, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContextClass>();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
