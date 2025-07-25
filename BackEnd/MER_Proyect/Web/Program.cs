using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web.Config;
using Web.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Utilities.authorization;
using Business.Email.config;
using Business.Email;
using Utilities.BackgroundServicess;


var builder = WebApplication.CreateBuilder(args);


// Controllers
//builder.Services.AddControllers();
builder.Services.AddControllers();

//swager
builder.Services.AddCustomSwagger();


// Añadir autenticación JWT 
builder.Services.AddJwtAuthentication(builder.Configuration);

// Registrar servicios de autenticación
//builder.Services.AddScoped<JwtService>();

builder.Services.AddHttpContextAccessor();
// CORS
builder.Services.AddCustomCors(builder.Configuration);

// ✅ extensión para la base de datos
builder.Services.AddDatabase(builder.Configuration);


// 🔧 Aquí registras tu cola y servicio en segundo plano
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<QueuedHostedService>();






// Repositorios y servicios
builder.Services.AddAppServices();


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, Business.Email.EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
//app.UseAuthorization();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
