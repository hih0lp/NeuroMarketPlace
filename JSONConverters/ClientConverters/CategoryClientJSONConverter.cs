using NeuroWeb.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PetProjectC_NeuroWeb.JSONConverters.ClientConverters
{
    public class CategoryClientJSONConverter : JsonConverter<List<CategoryClientDTOModel>>
    {
        public override List<CategoryClientDTOModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }

           

        public override void Write(Utf8JsonWriter writer, List<CategoryClientDTOModel> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("categories");

            writer.WriteStartArray();

            foreach (var category in value)
            {
                writer.WriteStartObject();
                writer.WriteString("name", category.name);
                writer.WriteString("pictureUrl", category.pictureUrl);
                writer.WriteEndObject();
            } 
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
