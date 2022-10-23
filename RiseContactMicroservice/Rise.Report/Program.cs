using Microsoft.Extensions.Options;
using Rise.Shared.Abstract;
using System;
using MongoDB.Driver;
using MongoDatabaseSettings = Rise.Shared.Models.MongoDatabaseSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;
//configure mongo settings
builder.Services.Configure<MongoDatabaseSettings>(configuration.GetSection("MongoDatabaseSettings"));
builder.Services.AddSingleton<IMongoDatabaseSettings, MongoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(new MongoClient(configuration["Cap:MongoDbConnection"]));


builder.Services.AddCap(x =>
{
    x.UseRabbitMQ(x =>
    {
        x.HostName = configuration["RabbitMq:ConnectionString"];
        x.UserName = "admin";
        x.Password = "123456";
        x.Port = -1;
        x.VirtualHost = "/";
    });
    x.UseMongoDB(configuration["Cap:MongoDbConnection"]);

});

var app = builder.Build();

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
