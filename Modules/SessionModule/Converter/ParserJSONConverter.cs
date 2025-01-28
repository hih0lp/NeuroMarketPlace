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

            using (var db = new DataBase())
            {
                var productsList = db.Products.ToList();

                foreach (var product in productsList)
                {
                    writer.WriteString("name", product.Name);
                    writer.WriteString("review", product.Comment.Review);
                }
            }

            writer.WriteEndArray();
        }
    }
}
