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
        
        public ActionResult CategoryPage()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetCategories()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new WBCategoryToClientJSONConverter());
            CategoryModel categoryModel = new CategoryModel();

            var allCategories = await categoryModel.GetCategoriesAsync();
            List<CategoryModel> categoryList = new List<CategoryModel>();

            foreach (var category in allCategories)
            {
                if (!category.IsChildrenOnly)
                {
                    categoryList.Add(category);
                }
            }
            //return await context.Response.WriteAsJsonAsync(categoryDTOList, options);
            return Json(categoryList, options);
        }
    }
}
