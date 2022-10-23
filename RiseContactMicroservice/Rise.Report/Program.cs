using Microsoft.Extensions.Options;
using Rise.Shared.Abstract;
using System;
using MongoDB.Driver;
using Refit;
using Rise.Report.DataAccess.Abstract;
using Rise.Report.DataAccess.Concreate;
using Rise.Report.Rest;
using MongoDatabaseSettings = Rise.Shared.Models.MongoDatabaseSettings;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;
//configure mongo settings
builder.Services.Configure<MongoDatabaseSettings>(configuration.GetSection("MongoDatabaseSettings"));
builder.Services.AddSingleton<IMongoDatabaseSettings, MongoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(new MongoClient(configuration["Cap:MongoDbConnection"]));

//automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IReportService, ReportService>();

//rest 
builder.Services.AddRefitClient<IRefitPersonService>()
    .ConfigureHttpClient((sp, c) =>
    {
        c.BaseAddress = new Uri("http://risecontact/api");
        c.Timeout = TimeSpan.FromMinutes(1);
    });

builder.Services.AddCap(x =>
{
x.UseRabbitMQ(x =>
{
    x.HostName = configuration["RabbitMq:ConnectionString"];
    x.UserName = "guest";
    x.Password = "guest";
    //x.Port = -1;
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
