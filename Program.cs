using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using PetProjectC_NeuroWeb;
using PetProjectC_NeuroWeb.Modules.AuthorizationModule.Core;
using PetProjectC_NeuroWeb.Modules.UserModule.UserModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Configuration.AddJsonFile("appsettings.json");

var tokenOptions = new AuthOptions
{
    Issuer = builder.Configuration["Authentication:Issuer"],
    Audience = builder.Configuration["Authentication:Audience"],
    Key = builder.Configuration["Authentication:Key"]
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = tokenOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = tokenOptions.GetSymmetricKey(),
            ValidateIssuerSigningKey = true
        };
    });


TokenOperations.tokenOptions = tokenOptions;
builder.Services.RegisterModules();





var app = builder.Build();

var staticFilesOptions = new FileServerOptions();
staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Clear();
staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Add("/documents/LoginPage.html");
app.UseFileServer(staticFilesOptions);

app.Run();


// Lets celebrate and suck some dicks