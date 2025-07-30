using HelloToAsp.Configs;
using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.DB;
using HelloToAsp.Policies.Handlers;
using HelloToAsp.Policies.Requirements;
using HelloToAsp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// bellow codes are middleware pipeline

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
