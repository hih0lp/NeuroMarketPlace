using NeuroWeb.Models;

namespace NeuroWeb.Models
{
    public class ProductDTOModel
    {
        public ProductDTOModel() { }


        public int id;
        public string name;
        public string brand;
        public double reviewRating;
        public string categoryName;
        public string url;
        public int price;
        public string pictureUrl;
    }

    public class DataBaseProductModel
    {
        public DataBaseProductModel() { }
        public DataBaseProductModel(int id, string name, string brand, double reviewRating, string categoryName, string url, int price, string pictureUrl, DataBaseCategoryModel? category)
        {
            _id = id;
            _name = name;
            _brand = brand;
            _reviewRating = reviewRating;
            _categoryName = categoryName;
            _url = url;
            _price = price;
            _pictureUrl = pictureUrl;
            _category = category;
        }

        private int _id;
        private string _name;
        private string _brand;
        private double _reviewRating;
        private string _categoryName;
        private string _url;
        private int _price;
        private string _pictureUrl;
        private DataBaseCategoryModel? _category;


        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Brand { get => _brand; set => _brand = value; }
        public double ReviewRating { get => _reviewRating; set => _reviewRating = value; }
        public string CategoryName { get => _categoryName; set => _categoryName = value; }
        public string Url { get => _url; set => _url = value; }
        public int Price { get => _price; set => _price = value; }
        public string PictureUrl { get => _pictureUrl; set => _pictureUrl = value; }
        public DataBaseCategoryModel Category { get => _category; set => _category = value; }

    }

}