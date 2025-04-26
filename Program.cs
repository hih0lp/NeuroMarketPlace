using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Rewrite;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using PetProjectC_NeuroWeb.Services.ParserService.Ports;
using PetProjectC_NeuroWeb.Services.TimeService.Ports;
using System.Text;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IParserService, WBParserService>(); //почитать про фоновые процессы 
//builder.Services.AddTransient<TimeService>();



var app = builder.Build();
//app.Services.GetService<TimeService>(); //парсер для категорий и продуктов


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CategoryPage}/{action=CategoryPage}/{id?}");



app.UseStaticFiles();


//var staticFilesOptions = new FileServerOptions();
//staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Clear();
//staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Add("/documents/CategoryPage.cshtml");
//app.UseFileServer(staticFilesOptions);


app.Run();


// Lets celebrate and suck some dicks