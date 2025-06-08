using DailyNotificationService.Background;
using DailyNotificationService.Configurations;
using DailyNotificationService.Data;
using DailyNotificationService.Services;
using DailyNotificationService.Services.DB;
using DailyNotificationService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<OneSignalSettings>(builder.Configuration.GetSection("OneSignal"));
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowSpecificOrigin",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000") // Or the desired origin(s)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddSingleton<NotificationService>();
builder.Services.AddScoped<IDbOperationService, DbOperationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHostedService<NotificationWorker>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Daily Notification Service", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowSpecificOrigin");

// app.MapOptions("/", () => Results.Ok()).RequireCors("AllowSpecificOrigin");
app.Run();
