using Microsoft.EntityFrameworkCore;
using NeuroWeb;
using PetProjectC_NeuroWeb.Models.CategoryModels;
using PetProjectC_NeuroWeb.Models.Products;

namespace PetProjectC_NeuroWeb.Models.Products
{
    public class ProductModel
    {
        public ProductModel() { }

        //public ProductModel(int id, string name, string brand, double reviewRating, string categoryName, string url, int price, string pictureUrl)//, string marketPlace
        //{
        //    _id = id;
        //    _name = name;
        //    _brand = brand;
        //    _reviewRating = reviewRating;
        //    _categoryName = categoryName;
        //    _url = url;
        //    _price = price;
        //    _pictureUrl = pictureUrl;
        //    //_marketPlace = marketPlace;
        //}

        //private int _id;
        //private string _name;
        //private string _brand;
        //private double _reviewRating;
        //private string _categoryName;
        //private string _url;
        //private int _price;
        //private string _pictureUrl;
        //private WildberriesCategoryModel _category;
        //private string _marketPlace;


        public int Id { get;  set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double ReviewRating { get; set; }
        public string CategoryName { get; set; }
        public string Url { get; set; }
        public int Price { get; set; }
        public string PictureUrl { get; set; }
        public WildberriesCategoryModel Category { get; set; }
        //public string MarketPlace { get => _marketPlace; set => _marketPlace = value; }


    }
}


public class WildberriesProductsActionModel
{
    public WildberriesProductsActionModel() { }

    public async Task<List<ProductModel>> GetProductsAsync(string categoryName)
    {
        using (var db = new DataBase())
        {
            var products = db.ProductModels.Include(x => x.Category).Where(_ => _.CategoryName == categoryName).ToList();

            return products;
        }
    }
}