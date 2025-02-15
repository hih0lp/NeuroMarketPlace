using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WBParser;
using WBParser.Category;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Parser test");
app.MapGet("/wildberries/categories", (HttpContext context) => CategoryEndpoints.GetCategories(context));

//for (var i = 0; i < 10; i++)
//    Console.WriteLine(responseBody[i].Name);

//var categories = await Category.ParseCategories();
//var furnitureCategories = new List<Category>();

//foreach (var category in categories)
//{
//    if (category.Url.Contains("/catalog/dom/mebel/"))
//    {
//        furnitureCategories.Add(category);
//    }
//}

//foreach (var category in furnitureCategories)
//{
//    Console.WriteLine(category.ChildrenOnly);
//}

app.Run();
