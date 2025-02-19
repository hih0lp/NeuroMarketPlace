using Microsoft.AspNetCore.Mvc;
using NeuroWeb.Models;
using System.Text.Json;
using NeuroWeb.JSONConverters.DataBaseConverters;
using NeuroWeb;

namespace PetProjectC_NeuroWeb.Controllers.Parser
{
    public class WBParserController : Controller
    {
        public async Task<StatusCodeResult> PutCategoryInDB()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CategoryJSONConverter());

            
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("insert");//вставить адрес


            if (response.IsSuccessStatusCode)
            {
                CategoryDTOModel categoryDTO = await response.Content.ReadFromJsonAsync<CategoryDTOModel>(options);

                if (categoryDTO != null)
                {
                    using (var db = new DataBase())
                    {
                        var categoryList = db.CategoryModels.ToList();
                        var category = categoryList.FirstOrDefault(s => s.name == categoryDTO.name);

                        if (category != null)
                        {
                            return StatusCode(401);
                        }

                        else
                        {
                            DataBaseCategoryModel DBCategoryModel = new DataBaseCategoryModel(
                                categoryDTO.id,
                                categoryDTO.name,
                                categoryDTO.relationRef,
                                category.isChildrenOnly,
                                null);

                            db.CategoryModels.Add(DBCategoryModel);
                            await db.SaveChangesAsync();

                            return StatusCode(200);
                        }

                    }
                }

            }

            return StatusCode(400);

            //HttpClient client = new HttpClient();

            //HttpResponseMessage response = await client.GetAsync();//вставить адрес

            //var categoryJSON = await response.Content.ReadFromJsonAsync<JsonResult>();

            //return categoryJSON;
        }
    }




}
