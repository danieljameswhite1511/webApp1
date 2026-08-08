using System.Security.Cryptography;
using System.Threading.Tasks;
using Application.ServiceCollectionExtensions;
using Domain.auth.Services;
using Domain.Common.GlobalConfig;
using Infrastructure.Identity.Users;
using Infrastructure.Persistence;
using Infrastructure.ServiceCollectionExtension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApp1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddApplicationConfig();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IdentityDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("identity") );
});
    

builder.Services.AddIdentityCore<AppUser>(options => {
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedAccount = false;
    
}).AddEntityFrameworkStores<IdentityDbContext>().AddUserValidator<UserEmailValidator>()
.AddDefaultTokenProviders();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<SecurityKeys>(builder.Configuration.GetSection("SecurityKeys"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Resolve AsymmetricTokenService from IoC container to get the Public Key
        using var serviceProvider = builder.Services.BuildServiceProvider();
        var tokenService = serviceProvider.GetRequiredService<IAsymmetricTokenService>();
        
        // Export public key and create RsaSecurityKey for verification
        var rsa = RSA.Create();
        rsa.ImportFromPem(tokenService.GetPublicKeyPem());
        var publicKey = new RsaSecurityKey(rsa);

        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings?.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings?.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = publicKey // <--- Using RSA Public Key instead of Symmetric key
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Pull token from cookie if present, otherwise fallback to Authorization header
                if (context.Request.Cookies.TryGetValue("jwt", out var token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });

/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretKey")))
        };
        options.Events = new JwtBearerEvents {
            OnMessageReceived = context => {
                context.Token = context.Request.Cookies["jwt"];
                return Task.CompletedTask;
            },
            OnTokenValidated = context => Task.CompletedTask,
            OnChallenge = challenge => {
               var token= challenge.HttpContext.Request.Headers["Authorization"];
                return Task.CompletedTask;
            },
            OnForbidden = forbidden => {
                return Task.CompletedTask;
            }
        };
    });
    */

builder.Services.AddCors(options => {
        options.AddPolicy("CorsPolicy", policy => {
            policy.WithOrigins("https://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();
app.SeedIdentityData();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();