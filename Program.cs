using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WBParser;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Parser test");

var jsonOptions = new JsonSerializerOptions();

jsonOptions.Converters.Add(new CategoryConverter());

HttpClientHandler handler = new HttpClientHandler()
{
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
};

HttpClient client = new HttpClient(handler);

HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://catalog.wb.ru/menu/v11/api?locale=ru&lang=ru&id=131289&collapse&dest=-1255987");

HttpResponseMessage response = await client.SendAsync(request);
response.EnsureSuccessStatusCode();

var responseBody = await response.Content.ReadFromJsonAsync<List<Category>>(jsonOptions);

for (var i = 0; i < 10; i++)
    Console.WriteLine(responseBody[i].Name);

app.Run();
