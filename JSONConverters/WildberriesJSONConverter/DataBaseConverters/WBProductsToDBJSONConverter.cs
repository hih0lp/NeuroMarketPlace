using System.Text.Json.Serialization;
using System.Text.Json;
using NeuroWeb.Models;

namespace PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters
{
    public class WBProductsToDBJSONConverter : JsonConverter<List<ProductModel>>
    {
        public override List<ProductModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<ProductModel> models = new List<ProductModel>();

            int id = 0;
            string name = "Undefined";
            string brand = "Undefined";
            double reviewRating = 0;
            string categoryName = "Undefined";
            string url = "Undefined";
            int price = 0;
            string pictureUrl = "Undefined";

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "id":
                            models.Add(new ProductModel());
                            id = reader.GetInt32();
                            models.Last().Id = id;
                            break;
                        case "name":
                            name = reader.GetString();
                            models.Last().Name = name;
                            break;
                        case "brand":
                            brand = reader.GetString();
                            models.Last().Brand = brand;
                            break;
                        case "reviewRating":
                            reviewRating = reader.GetDouble();
                            models.Last().ReviewRating = reviewRating;
                            break;
                        case "categoryName":
                            categoryName = reader.GetString();
                            models.Last().CategoryName = categoryName;
                            break;
                        case "url":
                            url = reader.GetString();
                            models.Last().Url = url;
                            break;
                        case "price":
                            price = reader.GetInt32();
                            models.Last().Price = price;
                            break;
                        case "picture":
                            pictureUrl = reader.GetString();
                            models.Last().PictureUrl = pictureUrl;
                            break;
                    }
                }
            }
            return models;
        }


        public override void Write(Utf8JsonWriter writer, List<ProductModel> value, JsonSerializerOptions options)
        { }
    }
}
