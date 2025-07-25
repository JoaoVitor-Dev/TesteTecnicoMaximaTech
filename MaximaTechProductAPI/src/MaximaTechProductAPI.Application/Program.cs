using MaximaTechProductAPI.Application;
using MaximaTechProductAPI.Core.Interfaces;
using MaximaTechProductAPI.Infrastructure.Database;
using MaximaTechProductAPI.Infrastructure.Repository;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(connectionString));

var dbInitializer = new DatabaseInitializer(connectionString);
dbInitializer.Initialize();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
