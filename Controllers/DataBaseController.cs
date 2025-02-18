using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WBParser.Controllers
{
    public class DataBaseController : Controller
    {
        private DbContext _db;



        public DataBaseController()
        {
            _db.Database.EnsureCreated();
        }

        [NonAction]
        public Task<NoContentResult> AddInDB() => null;
    }
}
