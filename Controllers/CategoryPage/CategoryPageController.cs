using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroWeb;
using NeuroWeb.JSONConverters.DataBaseConverters;
using NeuroWeb.Models;
using PetProjectC_NeuroWeb.JSONConverters.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.ClientConverters;
using System.Text.Json;

namespace PetProjectC_NeuroWeb.Controllers.CategoryPage
{
    public class CategoryPageController : Controller
    {
        
        public ActionResult CategoryPage()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetCategories()
        {

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CategoryClientJSONConverter());

            using (var db = new DataBase())
            {
                var products = db.ProductModels.ToList();
                var categories = db.CategoryModels.ToList();

                List<CategoryClientDTOModel> categoryDTOList = new List<CategoryClientDTOModel>();
                CategoryClientDTOModel categoryDTO = new CategoryClientDTOModel();

                foreach (var category in categories)
                {
                    var pictureUrl = products.FirstOrDefault(s => s.CategoryName == category.Name)!.PictureUrl;
                    categoryDTO.name = category.Name;
                    categoryDTO.pictureUrl = pictureUrl;

                    categoryDTOList.Add(categoryDTO);
                }

                //return await context.Response.WriteAsJsonAsync(categoryDTOList, options);
                return Json(categoryDTOList, options);
            }




            
        }

    }
}
