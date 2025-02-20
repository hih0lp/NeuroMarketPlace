using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


//var options = new RewriteOptions()
//    .AddRewrite("products/addproduct", );
    


var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WBParser}/{action=PutCategoryInDB}/{id?}");

app.Run();


// Lets celebrate and suck some dicks