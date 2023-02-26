using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryEfCore.IReposetory;
using RepositoryEfCore.Logic;
using System;
using TrackingDataApi;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddUnitOfWork<AppDbContext>();

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
             .AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("defaultConnection")))
             .AddUnitOfWork<AppDbContext>();

var app = builder.Build();
///var  ttt = app.Services.GetServices<IUnitOfWork<AppDbContext>>();

//builder.Services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();


//var unitOfWork = ttt.FirstOrDefault();

//var unitOfWork = builder.Services.AddUnitOfWork<AppDbContext>();

//unitOfWork.dbco  DbContext.Database.GetAppliedMigrations();
//unitOfWork.DbContext.Database.Migrate();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
