using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using NeuroWeb.Models;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using System.Text.Json;
using PetProjectC_NeuroWeb;
using NeuroWeb;
using System.Net;

namespace PetProjectC_NeuroWeb.Services.ParserService.Ports
{
    public class WBParserService : IParserService
    {
        public async void AddCategoryInDB(object state)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new WBCategoryToDBJSONConverter());


            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7259/wildberries/categories");
                

            if (response.IsSuccessStatusCode)
            {

                List<CategoryModel> categoryDTOList = await response.Content.ReadFromJsonAsync<List<CategoryModel>>(options); //DTO категорий (парсер)

                foreach (var categoryDTO in categoryDTOList)
                {
                    if (categoryDTO != null)
                    {
                        using (var db = new DataBase())
                        {
                            var categoryList = db.CategoryModels.ToList();
                            var category = categoryList.FirstOrDefault(s => s.Name == categoryDTO.Name);

                            if (category == null)
                            { 

                                //List<DataBaseProductModel> list = null!;

                                CategoryModel DBCategoryModel = new CategoryModel(
                                    categoryDTO.Id,
                                    categoryDTO.Name,
                                    categoryDTO.RelationRef,
                                    categoryDTO.IsChildrenOnly,
                                    null); //last param = "WB"

                                db.CategoryModels.Add(DBCategoryModel);
                                await db.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }



        public async void AddProductsInDB(object state)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new WBProductsToDBJSONConverter());

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromDays(1);

            HttpResponseMessage response = await client.GetAsync("https://localhost:7259/wildberries/products");


            if (response.IsSuccessStatusCode)
            {
                List<ProductModel> productsDTOList = await response.Content.ReadFromJsonAsync<List<ProductModel>>(options);

                
                foreach (var productDTO in productsDTOList)
                {
                    

                    using (var db = new DataBase())
                    {
                        var productsList = db.ProductModels.ToList();

                        var product = productsList.FirstOrDefault(s => s.Name == productDTO.Name);

                        if (product == null && productDTO.PictureUrl != null)
                        {
                            ProductModel DBProductModel = new ProductModel(
                                productDTO.Id,
                                productDTO.Name,
                                productDTO.Brand,
                                productDTO.ReviewRating,
                                productDTO.CategoryName,
                                productDTO.Url,
                                productDTO.Price,
                                productDTO.PictureUrl);

                            db.ProductModels.Add(DBProductModel);
                            await db.SaveChangesAsync();
                        }
                    }
                }
                
            }
        }

    }
}
