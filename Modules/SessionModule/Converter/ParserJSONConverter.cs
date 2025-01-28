using System.Text.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using PetProjectC_NeuroWeb.Modules.SessionModule.DTO;

namespace PetProjectC_NeuroWeb.Modules.SessionModule.Converter
{
    public class ParserJSONConverter : JsonConverter<PicturesDTO>
    {
        public override PicturesDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }


        public override void Write(Utf8JsonWriter writer, PicturesDTO value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            for (int i = 0; i < value.names.Length; i++)
            {
                writer.WriteString("name", value.names[i]);
                writer.WriteString("review", value.reviews[i]);
            }

            writer.WriteEndArray();
        }
    }
}
