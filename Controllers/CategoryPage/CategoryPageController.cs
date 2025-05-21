using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroWeb;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using PetProjectC_NeuroWeb.Models.CategoryModels;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PetProjectC_NeuroWeb.Controllers.CategoryPage
{
    public class CategoryPageController : Controller
    {
        private ILogger<CategoryPageController> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { Converters = { new WBCategoryToClientJSONConverter() } };
        public CategoryPageController(ILogger<CategoryPageController> logger)
        {
            _logger = logger;
        }

        public ActionResult CategoryPage()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCategories(string categoryName = "")
        {
            WildberriesDataActionModel wildberriesCategoryActionModel = new WildberriesDataActionModel();
            _logger.LogInformation("Запрос на получение категорий получен"); //закомментить, если не нужна отладка

            var categoryList = await wildberriesCategoryActionModel.GetCategoriesAsync(categoryName);

            foreach (var category in categoryList)
            {
                Console.WriteLine(category.Name);   
            }

            return Json(categoryList, _jsonOptions);
        }
    }
}
