using System.Reflection;
using System.Text;

using Identity.API.Grpc;
using Identity.API.Interceptors;
using Identity.Core;
using Identity.Core.Data;
using Identity.Core.Models;
using Identity.Core.Options;
using Identity.Core.Services;
using Identity.Core.Services.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Spines.Shared.Mediators.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var coreAssembly = Assembly.GetAssembly(typeof(AssemblyReference))
    ?? throw new AppDomainUnloadedException("Unable to get assembly reference.");

builder.Services.Configure<AuthenticationOptions>(
    builder.Configuration.GetSection(AuthenticationOptions.Configuration));

builder.Services.AddScoped<IIdentityMediator, IdentityMediator>();
builder.Services.AddScoped<IUserMediator, UserMediator>();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

#region Entity Framework
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});
#endregion

#region Auth
var authConfiguration = builder.Configuration.GetSection(AuthenticationOptions.Configuration).Get<AuthenticationOptions>()
    ?? throw new ApplicationException($"Missing {AuthenticationOptions.Configuration} values in appsettings.");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authConfiguration.Issuer,
        ValidAudience = authConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(authConfiguration.SigninKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
#endregion

builder.Services.AddMediator(coreAssembly);

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ExceptionLoggerInterceptor>();
});
builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<IdentityService>();
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    context.Database.Migrate();
}

app.Run();
