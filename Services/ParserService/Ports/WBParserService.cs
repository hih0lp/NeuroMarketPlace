using NeuroWeb.JSONConverters.DataBaseConverters;
using NeuroWeb.Models;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using System.Text.Json;
using PetProjectC_NeuroWeb;
using NeuroWeb;

namespace PetProjectC_NeuroWeb.Services.ParserService.Ports
{
    public class WBParserService : IParserService
    {
        public async void AddCategoryInDB(object state)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CategoryJSONConverter());


            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://gavno.somee.com/wildberries/categories");//вставить адрес


            if (response.IsSuccessStatusCode)
            {

                List<CategoryDTOModel> categoryDTOList = await response.Content.ReadFromJsonAsync<List<CategoryDTOModel>>(options);

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
                                Console.WriteLine(1111);

                                List<DataBaseProductModel> list = null!;

                                DataBaseCategoryModel DBCategoryModel = new DataBaseCategoryModel(
                                    categoryDTO.id,
                                    categoryDTO.name,
                                    categoryDTO.relationRef,
                                    categoryDTO.isChildrenOnly,
                                    null);

                                db.CategoryModels.Add(DBCategoryModel);
                                await db.SaveChangesAsync();

                            }
                        }
                    }
                }
            }
        }

        public async Task AddProductInDB()
        {

        }

    }
}
