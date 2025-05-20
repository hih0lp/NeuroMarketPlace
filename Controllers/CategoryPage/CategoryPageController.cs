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

        public ActionResult CategoryPage()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetCategories(string categoryName = "")
        {
            WildberriesDataActionModel categoryActionModel = new WildberriesDataActionModel();
            _logger.LogInformation("Запрос на получение категорий отправлен"); //закомментить, если не нужна отладка
            
            var categoryList = await categoryActionModel.GetCategoriesAsync(categoryName);
            

            return Json(categoryList, _jsonOptions);
        }
    }
}
