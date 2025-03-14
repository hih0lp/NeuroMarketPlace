using System.Text.Json.Serialization;
using System.Text.Json;
using NeuroWeb.Models;

namespace PetProjectC_NeuroWeb.JSONConverters.DataBaseConverters
{
    public class ProductsJSONConverter : JsonConverter<ProductDTOModel>
    {
        public override ProductDTOModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<ProductDTOModel> models = new List<ProductDTOModel>();

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

                    switch(propertyName)
                    {
                        case "id":
                            models.Add(new ProductDTOModel());
                            id = reader.GetInt32();
                            models.Last().id = id;
                            break;
                        case "name":
                            name = reader.GetString();
                            models.Last().name = name;
                            break;
                        case "brand":
                            brand = reader.GetString();
                            models.Last().brand = brand;
                            break;
                        case "reviewRating":
                            reviewRating = reader.GetDouble();
                            models.Last().reviewRating = reviewRating;
                            break;
                        case "categoryName":
                            categoryName = reader.GetString();
                            models.Last().categoryName = categoryName;
                            break;
                        case "url":
                            url = reader.GetString();
                            models.Last().url = url;
                            break;                          
                        case "price":
                            price = reader.GetInt32();
                            models.Last().price = price;
                            break;
                        case "picture":
                            pictureUrl = reader.GetString();
                            models.Last().pictureUrl = pictureUrl;
                            break;
                    }
                }
            }
        }


        public override void Write(Utf8JsonWriter writer, ProductDTOModel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
