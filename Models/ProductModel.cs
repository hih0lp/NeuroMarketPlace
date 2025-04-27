using NeuroWeb.Models;

namespace NeuroWeb.Models
{
    //public class DataBaseProductModel
    //{
    //    public DataBaseProductModel() { }
    //    public DataBaseProductModel(int id, string name, string brand, double reviewRating, string categoryName, string url, int price, string pictureUrl)
    //    {
    //        _id = id;
    //        _name = name;
    //        _brand = brand;
    //        _reviewRating = reviewRating;
    //        _categoryName = categoryName;
    //        _url = url;
    //        _price = price;
    //        _pictureUrl = pictureUrl;
    //    }

    //    private int _id;
    //    private string _name;
    //    private string _brand;
    //    private double _reviewRating;
    //    private string _categoryName;
    //    private string _url;
    //    private int _price;
    //    private string _pictureUrl;


    //    public int Id { get => _id; set => _id = value; }
    //    public string Name { get => _name; set => _name = value; }
    //    public string Brand { get => _brand; set => _brand = value; }
    //    public double ReviewRating { get => _reviewRating; set => _reviewRating = value; }
    //    public string CategoryName { get => _categoryName; set => _categoryName = value; }
    //    public string Url { get => _url; set => _url = value; }
    //    public int Price { get => _price; set => _price = value; }
    //    public string PictureUrl { get => _pictureUrl; set => _pictureUrl = value; }

    //}

    public class ProductModel
    {
        public ProductModel() { }

        public ProductModel(int id, string name, string brand, double reviewRating, string categoryName, string url, int price, string pictureUrl)//, string marketPlace
        {
            _id = id;
            _name = name;
            _brand = brand;
            _reviewRating = reviewRating;
            _categoryName = categoryName;
            _url = url;
            _price = price;
            _pictureUrl = pictureUrl;
            //_marketPlace = marketPlace;
        }

        private int _id;
        private string _name;
        private string _brand;
        private double _reviewRating;
        private string _categoryName;
        private string _url;
        private int _price;
        private string _pictureUrl;
        //private string _marketPlace;


        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Brand { get => _brand; set => _brand = value; }
        public double ReviewRating { get => _reviewRating; set => _reviewRating = value; }
        public string CategoryName { get => _categoryName; set => _categoryName = value; }
        public string Url { get => _url; set => _url = value; }
        public int Price { get => _price; set => _price = value; }
        public string PictureUrl { get => _pictureUrl; set => _pictureUrl = value; }
        //public string MarketPlace { get => _marketPlace; set => _marketPlace = value; }


    }
}