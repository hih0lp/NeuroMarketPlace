using Microsoft.AspNetCore.Mvc;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ToClientConverters;
using System.Drawing.Text;
using System.Text.Json;

namespace PetProjectC_NeuroWeb.Controllers.ProductPage
{
    public class ProductsPageController : Controller
    {
        private readonly ILogger<ProductsPageController> _logger;
        private readonly static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions() { Converters = { new WBProductsToClientJSONConverter()} };

        public ProductsPageController(ILogger<ProductsPageController> logger)
        {
            _logger = logger;
        }

        public IActionResult ProductsPage()
        {
            return View();
        }
    }
}
