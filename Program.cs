using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Parser test");

HttpClientHandler handler = new HttpClientHandler()
{
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
};

HttpClient client = new HttpClient(handler);

HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://catalog.wb.ru/menu/v11/api?locale=ru&lang=ru&id=131289&collapse&dest=-1255987");

//request.Headers.Add("accept", "*/*");
//request.Headers.Add("accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
//request.Headers.TryAddWithoutValidation("if-none-match", "640452");
//request.Headers.Add("origin", "https://www.wildberries.ru");
//request.Headers.Add("priority", "u=1, i");
//request.Headers.Add("referer", "https://www.wildberries.ru/catalog/dom/mebel");
//request.Headers.Add("sec-ch-ua", "\"Not A(Brand\";v=\"8\", \"Chromium\";v=\"132\", \"Google Chrome\";v=\"132\"");
//request.Headers.Add("sec-ch-ua-mobile", "?0");
//request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
//request.Headers.Add("sec-fetch-dest", "empty");
//request.Headers.Add("sec-fetch-mode", "cors");
//request.Headers.Add("sec-fetch-site", "cross-site");
//request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36");

HttpResponseMessage response = await client.SendAsync(request);
response.EnsureSuccessStatusCode();
string responseBody = await response.Content.ReadAsStringAsync();

Console.WriteLine(responseBody);

app.Run();
