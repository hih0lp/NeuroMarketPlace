using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Configuration.UserSecrets;
using PetProjectC_NeuroWeb.Modules.AuthorizationModule.Core;

namespace PetProjectC_NeuroWeb.Modules.ProductModule.Core
{
    public static class ProductOperationsService
    {
        public static async Task AddProductToCatalog(HttpContext context)
        {
            var form = context.Request.Form;

            string productId = Guid.NewGuid().ToString();
            var descriptionImage = form["description"].ToString();
            var files = form.Files;

            using(var ms = new MemoryStream())
            {
                foreach (var file in files)
                {
                    await file.CopyToAsync(ms);
                }

                byte[] imagesBytes = ms.ToArray(); //массив байтов-картинок
                List<string> listImages = new List<string>();
                StreamWriter sw = new StreamWriter($"products/{productId}/image.txt");//создание папки по артиклю и запись картинки в байтовом
                

                foreach (var image in imagesBytes)
                {
                    /*listImages.Add(Convert.ToString(image));*/ //добавление картинок в массив байтов
                    image.ToString();
                    sw.WriteLine(image);//запись в файл
                }

                sw.Close();

                using(var db = new DataBase())
                {
                    var products = db.Products.ToList();
                    Product product = new Product(productId, descriptionImage);
                    products.Add(product);
                    db.SaveChanges();
                }

                

                
            }            
        }

        public static async Task<Product> GetProductById(string productId)
        {
            var db = new DataBase();
            
            var products = db.Products.ToList();
            var findingProduct = products.FirstOrDefault(u => u.Id == productId);

            if (findingProduct != null)
            {
                return findingProduct;
            }
            return null;

            
        }

        public static async Task AddProductToUserCart(string accessJWTToken, string productId)
        {
            var userClaims = TokenOperations.DecodeToken(accessJWTToken);
            var userId = userClaims["userId"];

            using (var db = new DataBase())
            {
                var users = db.Users.ToList();
                var user = users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    user.ProductsList.Add(productId);
                }

                else
                {
                    //return an authorization/registration page
                }
            }
        }
    }
}

//создать отдельную папку с артиклем предмета, там будет лежать картинка битовая. В БД будет лежать описание и характеристика