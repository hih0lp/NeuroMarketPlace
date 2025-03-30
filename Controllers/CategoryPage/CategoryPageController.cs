using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroWeb;
using NeuroWeb.JSONConverters.DataBaseConverters;
using NeuroWeb.Models;
using PetProjectC_NeuroWeb.JSONConverters.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.ClientConverters;
using System.Text.Json;
//using Microsoft
using static System.Net.WebRequestMethods;

namespace PetProjectC_NeuroWeb.Controllers.CategoryPage
{
    public class CategoryPageController : Controller
    {
        
        public ActionResult CategoryPage()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetCategories(bool isMainCategory = true)
        {

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CategoryClientJSONConverter());

            using (var db = new DataBase())
            {
                var products = db.ProductModels.ToList();
                var categories = db.CategoryModels.ToList();

                List<CategoryClientDTOModel> categoryDTOList = new List<CategoryClientDTOModel>();
                

                foreach (var category in categories)
                { 
                    CategoryClientDTOModel categoryDTO = new CategoryClientDTOModel();

                    categoryDTO.name = category.Name;
                    categoryDTO.pictureUrl = "https://basket-12.wbbasket.ru/vol1731/part173167/173167093/images/big/1.webp";
                    
                    //categoryDTO.pictureUrl = products.Where(p => p.CategoryName == category.Name).FirstOrDefault().PictureUrl; //это верно

                    if (isMainCategory)
                    {
                        if (category.IsChildrenOnly)
                        {
                            categoryDTOList.Add(categoryDTO);
                        }
                    }

                    else if (!category.IsChildrenOnly)
                    {
                        categoryDTOList.Add(categoryDTO);
                    }

                    

                    
                }

                //return await context.Response.WriteAsJsonAsync(categoryDTOList, options);
                return Json(categoryDTOList, options);
            }
        }

        //Убрать логику в модель 


        
    }
}
