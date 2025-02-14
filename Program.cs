using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WBParser;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Parser test");

//for (var i = 0; i < 10; i++)
//    Console.WriteLine(responseBody[i].Name);

app.Run();
