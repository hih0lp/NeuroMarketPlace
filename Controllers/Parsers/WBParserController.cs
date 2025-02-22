using Microsoft.AspNetCore.Mvc;
using NeuroWeb.Models;
using System.Text.Json;
using NeuroWeb.JSONConverters.DataBaseConverters;
using NeuroWeb;

namespace PetProjectC_NeuroWeb.Controllers.Parsers
{
    public class WBParserController : Controller
    {
        public async Task PutCategoryInDB()
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


            //HttpClient client = new HttpClient();

            //HttpResponseMessage response = await client.GetAsync();//вставить адрес

            //var categoryJSON = await response.Content.ReadFromJsonAsync<JsonResult>();

            //return categoryJSON;
        }
    }




}
