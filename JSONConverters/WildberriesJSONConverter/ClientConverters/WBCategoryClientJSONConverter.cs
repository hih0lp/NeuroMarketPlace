using NeuroWeb.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters
{
    public class WBCategoryClientJSONConverter : JsonConverter<List<CategoryModel>>
    {
        public override List<CategoryModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }



        public override void Write(Utf8JsonWriter writer, List<CategoryModel> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("categories");

            writer.WriteStartArray();

            foreach (var category in value)
            {
                writer.WriteStartObject();
                writer.WriteString("name", category.Name);
                writer.WriteString("pictureUrl", category.PictureUrl);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
