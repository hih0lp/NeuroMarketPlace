using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using System.Text.Json;
using PetProjectC_NeuroWeb;
using NeuroWeb;
using System.Net;
using PetProjectC_NeuroWeb.Models.CategoryModels;
using PetProjectC_NeuroWeb.Models.Products;

namespace PetProjectC_NeuroWeb.Services.ParserService.Ports
{
    public class WBParserService : IParserService
    {
        public async Task AddCategoryInDB()//param = object state
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new WBCategoryToDBJSONConverter());


            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7259/wildberries/categories");
                

            if (response.IsSuccessStatusCode)
            {
                List<WildberriesCategoryModel> gettingCategoryList = await response.Content.ReadFromJsonAsync<List<WildberriesCategoryModel>>(options); //DTO категорий (парсер)

                foreach (var categoryDTO in gettingCategoryList)
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

                                WildberriesCategoryModel DBCategoryModel = new WildberriesCategoryModel()
                                {
                                    Id = categoryDTO.Id,
                                    Name = categoryDTO.Name,
                                    PictureUrl = categoryDTO.PictureUrl,
                                    Products = categoryDTO.Products,
                                    RelationRef = categoryDTO.RelationRef,
                                    IsChildrenOnly = categoryDTO.IsChildrenOnly,
                                };

                                db.CategoryModels.Add(DBCategoryModel);
                                await db.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }



        public async Task AddProductsInDB()//param = object state
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new WBProductsToDBJSONConverter());

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromDays(1);

            HttpResponseMessage response = await client.GetAsync("https://localhost:7259/wildberries/products");

            //Возможно, даже не возможно, а скорее всего, придется накатывать кэш для решения проблем с запросами в бд и обновлением ее во время парсинга
            if (response.IsSuccessStatusCode)
            {
                List<ProductModel> gettingProductsList = await response.Content.ReadFromJsonAsync<List<ProductModel>>(options);

                
                foreach (var productDTO in gettingProductsList)
                {
                    using (var db = new DataBase())
                    {
                        if (productDTO.PictureUrl != null)
                        {
                            var productsList = db.ProductModels.ToList();
                            var product = productsList.FirstOrDefault(s => s.Name == productDTO.Name);

                            if (product == null)
                            {
                                ProductModel DBProductModel = new ProductModel()
                                {
                                    Id = productDTO.Id,
                                    Name = productDTO.Name,
                                    Brand = productDTO.Brand,
                                    ReviewRating = productDTO.ReviewRating,
                                    CategoryName = productDTO.CategoryName,
                                    Url = productDTO.Url,
                                    Price = productDTO.Price,
                                    PictureUrl = productDTO.PictureUrl,
                                    Category = productDTO.Category,
                                };

                                db.ProductModels.Add(DBProductModel);
                                await db.SaveChangesAsync();
                            }
                            else if (product != null)
                            {
                                product = productDTO;
                                db.Update(product);

                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
    }
}
