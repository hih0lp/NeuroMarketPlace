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
        public async Task<ActionResult> GetCategories(string categoryName = "")
        {
            WildberriesCategoryActionModel wildberriesCategoryActionModel = new WildberriesCategoryActionModel();
            _logger.LogInformation("Запрос на получение категорий получен"); //закомментить, если не нужна отладка

            var categoryList = await wildberriesCategoryActionModel.GetCategoriesAsync(categoryName);

            if (categoryList != null)
            {
                foreach (var category in categoryList)
                {
                    Console.WriteLine(category.Name);   
                }
            }
            else
            {
                return RedirectToAction("ProductsPage", "ProductsPage", categoryName);
            }



            return Json(categoryList, _jsonOptions);
        }
    }
}
