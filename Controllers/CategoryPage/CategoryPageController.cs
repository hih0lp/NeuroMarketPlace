using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetProjectC_NeuroWeb.Controllers.CategoryPage
{
    public class CategoryPageController : Controller
    {
        
        public ActionResult CategoryPage()
        {
            return View();
        }

        
        public JsonResult GetCategory()
        {

        }

    }
}
