using Microsoft.AspNetCore.Authorization;

namespace PetProjectC_NeuroWeb.Modules.OrderModule.Core
{
    public class OrderOperations
    {
        [Authorize]
        public async Task OrderObject(HttpContext context, string article, string userId)
        {
            using (var db = new DataBase())
            {
                var findinguser = db.Users.ToList().FirstOrDefault(user => user.Id == userId);
                findinguser!.ProductsList.Add(article);
            }
        }

    }
}
