using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroWeb;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ToClientConverters;
using PetProjectC_NeuroWeb.Models.CategoryModels;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PetProjectC_NeuroWeb.Controllers.CategoryPage
{
    public class CategoryPageController : Controller
    {
        private ILogger<CategoryPageController> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions 
        { 
            Converters = 
            { new WBCategoryToClientJSONConverter(), 
                new WBProductsToClientJSONConverter() } 
        };
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

            if (categoryList != null && categoryList.Any())
            {
                foreach (var category in categoryList)
                {
                    Console.WriteLine(category.Name);   
                }
                return Json(categoryList, _jsonOptions);

            }
            else
            {
                WildberriesProductsActionModel actionModel = new WildberriesProductsActionModel();
                var products = await actionModel.GetProductsAsync(categoryName);

                foreach (var product in products)
                {
                    Console.WriteLine(product.Name);
                }


                return Json(products, _jsonOptions);
            }
        }
    }
}
