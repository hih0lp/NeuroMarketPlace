using System.Net;
using System.Text.Json;

namespace WBParser
{
    public class CategoryEndpoints
    {
        public async static Task GetCategories(HttpContext context)
        {
            // /catalog/dom/mebel

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
        }
    }
}
