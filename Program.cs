﻿using HelloToAsp.Configs;
using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.DB;
using HelloToAsp.Middleware;
using HelloToAsp.Policies.Handlers;
using HelloToAsp.Policies.Requirements;
using HelloToAsp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ToDoListDbConnectionString");
// represent connection of the database
builder.Services.AddDbContext<ToDoListContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList API", Version = "v1" });
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header, // The token is expected in the HTTP header
            Description = "Please enter a valid token",
            Name = "Authorization", // The header name that will contain the token
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        }
    );
});

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"), // in query path
        new HeaderApiVersionReader("X-Version"), // in request header
        new MediaTypeApiVersionReader("ver") // in response header
        );
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV"; // in url
        options.SubstituteApiVersionInUrl = true;
    });

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration((ctx.Configuration)));

builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("ToDoList") // maybe we have different providers like todolist, users and etc 
    .AddEntityFrameworkStores<ToDoListContext>()
    .AddDefaultTokenProviders()
    // وقتی کاربر می‌خواهد رمز عبورش را ریست کند، یک لینک با یک توکن (کد امنیتی) برایش ایمیل می‌شود
    // وقتی کاربر اکانتش را تأیید می‌کند، یک توکن به ایمیلش فرستاده می‌شود
    // وقتی دو مرحله‌ای (2FA) فعال است، یک کد برای کاربر ارسال می‌شود
    .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
    // used when authentication is required but the user isn't authenticated (challenging them to authenticate)
    // Automatic on each request, Validate credentials, create identity

    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    // used by default when the system needs to authenticate a user (determine who they are)
    // When authorization fails, Tell client how to authenticate
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddScoped<IAuthorizationHandler, ToDoListOwnerHandler>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ToDoListOwner", policy =>
        policy.Requirements.Add(new ToDoListOwnerRequirement()));

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024; // in bytes
    options.UseCaseSensitivePaths = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// bellow codes are middleware pipeline

app.UseMiddleware<ExceptionMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseResponseCaching();

app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(10)
        };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
        new string[] { "Accept-Encoding" };

    await next();
}); // here we put middleware code directly in our program.cs

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
