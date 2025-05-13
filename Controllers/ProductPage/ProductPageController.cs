using Microsoft.AspNetCore.Mvc;

namespace PetProjectC_NeuroWeb.Controllers.ProductPage
{
    public class ProductPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
