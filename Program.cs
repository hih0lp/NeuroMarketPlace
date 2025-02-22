using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Rewrite;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using PetProjectC_NeuroWeb.Services.TimeService.Ports;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IParserService>();
builder.Services.AddTransient<TimeService>();


//var options = new RewriteOptions()
//    .AddRewrite("products/addproduct", );
    


var app = builder.Build();
app.Services.GetService<TimeService>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WBParser}/{action=PutCategoryInDB}/{id?}");

app.Run();


// Lets celebrate and suck some dicks