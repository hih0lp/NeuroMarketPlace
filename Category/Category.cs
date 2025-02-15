using System.Net;
using System.Text.Json;

namespace WBParser.Category
{
    public class Category
    {
        public Category(int id)
        {
            Id = id;
        }
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string ShardKey { get; set; } = null!;
        public bool ChildrenOnly { get; set; }

        public async static Task<List<Category>> ParseCategories()
        {
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

            return await response.Content.ReadFromJsonAsync<List<Category>>(jsonOptions);
        }
    }
}
