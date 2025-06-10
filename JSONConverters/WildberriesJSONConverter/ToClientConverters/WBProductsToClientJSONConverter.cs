using PetProjectC_NeuroWeb.Models.CategoryModels;
using PetProjectC_NeuroWeb.Models.Products;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ToClientConverters
{
    public class WBProductsToClientJSONConverter : JsonConverter<List<ProductModel>>
    {
        public override List<ProductModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }

        public override void Write(Utf8JsonWriter writer, List<ProductModel> productsList, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("products");
            writer.WriteStartArray();

            foreach (var product in productsList)
            {
                writer.WriteStartObject();
                writer.WriteString("name", product.Name);
                writer.WriteString("pictureurl", product.PictureUrl);
                writer.WriteString("url", product.Url);
                writer.WriteNumber("reviewrating", product.ReviewRating);
                writer.WriteNumber("price", product.Price);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}