using System.Net;
using System.Text.Json;

namespace WBParser.Category
{
    public class CategoryEndpoints
    {
        public async static Task GetCategories(HttpContext context)
        { 
            var jsonOptions = new JsonSerializerOptions();

            jsonOptions.Converters.Add(new CategoryConverter());



            var categories = await Category.ParseCategories();
            var furnitureCategories = new List<Category>();

            foreach (var category in categories)
            {
                if (category.Url.Contains("/catalog/dom/mebel/"))
                {
                    furnitureCategories.Add(category);
                }
            }


            await context.Response.WriteAsJsonAsync(furnitureCategories);
        }
    }
}
