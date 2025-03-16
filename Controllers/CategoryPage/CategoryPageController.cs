using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroWeb;
using NeuroWeb.JSONConverters.DataBaseConverters;
using NeuroWeb.Models;


namespace PetProjectC_NeuroWeb.Controllers.CategoryPage
{
    public class CategoryPageController : Controller
    {
        
        public ActionResult CategoryPage()
        {
            return View();
        }


        public JsonResult GetCategories()
        {
            JsonOptions options = new JsonOptions();
            options.JsonSerializerOptions.Converters.Add(new CategoryJSONConverter());

            using (var db = new DataBase())
            {
                var products = db.ProductModels.ToList();
                var categories = db.CategoryModels.ToList();

                CategoryParserDTOModel categoryDTO = new CategoryParserDTOModel();

                foreach (var category in categories)
                {
                    categoryDTO.name = category.Name;
                    categoryDTO.
                }
            }
        }

    }
}
