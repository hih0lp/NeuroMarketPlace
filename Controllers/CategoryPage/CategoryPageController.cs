using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroWeb;
using NeuroWeb.Models;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PetProjectC_NeuroWeb.Controllers.CategoryPage
{

    public class CategoryPageController : Controller
    {
        
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { Converters = { new WBCategoryToClientJSONConverter() } };
        public ActionResult CategoryPage()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetCategories(string categoryName = "")
        {
            CategoryModel categoryModel = new CategoryModel();

            var categoryList = await categoryModel.GetCategoriesAsync(categoryName);

            

            return Json(categoryList, _jsonOptions);
        }
    }
}
