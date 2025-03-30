using NeuroWeb.JSONConverters.DataBaseConverters;
using NeuroWeb.Models;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using System.Text.Json;
using PetProjectC_NeuroWeb;
using NeuroWeb;
using PetProjectC_NeuroWeb.JSONConverters.DataBaseConverters;
using System.Net;

namespace PetProjectC_NeuroWeb.Services.ParserService.Ports
{
    public class WBParserService : IParserService
    {
        public async void AddCategoryInDB(object state)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CategoryDBJSONConverter());


            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7259/wildberries/categories");
                

            if (response.IsSuccessStatusCode)
            {

                List<CategoryParserDTOModel> categoryDTOList = await response.Content.ReadFromJsonAsync<List<CategoryParserDTOModel>>(options);

                foreach (var categoryDTO in categoryDTOList)
                {
                    if (categoryDTO != null)
                    {
                        using (var db = new DataBase())
                        {

                            var categoryList = db.CategoryModels.ToList();



                            var category = categoryList.FirstOrDefault(s => s.Name == categoryDTO.name);


                            if (category == null)
                            { 

                                //List<DataBaseProductModel> list = null!;

                                DataBaseCategoryModel DBCategoryModel = new DataBaseCategoryModel(
                                    categoryDTO.id,
                                    categoryDTO.name,
                                    categoryDTO.relationRef,
                                    categoryDTO.isChildrenOnly);

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
            options.Converters.Add(new ProductsJSONConverter());

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromDays(1);

            HttpResponseMessage response = await client.GetAsync("https://localhost:7259/wildberries/products");


            if (response.IsSuccessStatusCode)
            {
                List<ProductDTOModel> productsDTOList = await response.Content.ReadFromJsonAsync<List<ProductDTOModel>>(options);

                
                foreach (var productDTO in productsDTOList)
                {
                    

                    using (var db = new DataBase())
                    {
                        var productsList = db.ProductModels.ToList();

                        var product = productsList.FirstOrDefault(s => s.Name == productDTO.name);

                        if (product == null && productDTO.url != null)
                        {
                            DataBaseProductModel DBProductModel = new DataBaseProductModel(
                                productDTO.id,
                                productDTO.name,
                                productDTO.brand,
                                productDTO.reviewRating,
                                productDTO.categoryName,
                                productDTO.url,
                                productDTO.price,
                                productDTO.pictureUrl);

                            db.ProductModels.Add(DBProductModel);
                            await db.SaveChangesAsync();
                        }
                    }
                }
                
            }
        }

    }
}
