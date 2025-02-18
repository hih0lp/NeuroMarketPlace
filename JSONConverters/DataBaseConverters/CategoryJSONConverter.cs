using System.Text.Json.Serialization;
using System.Text.Json;
using NeuroWeb.Models;

namespace WBParser.JSONConverters.DataBaseConverters
{
    public class CategoryJSONConverter : JsonConverter<CategoryModel>
    {
        public override CategoryModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var id = 0;
            var name = "Undefined";
            var relationRef = "Undefined";
            var isChildrenOnly = true;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();


                    switch (propertyName.ToLower())
                    {
                        case "id":
                            id = reader.GetInt32();
                            break;

                        case "name":
                            name = reader.GetString();
                            break;

                        case "url":
                            relationRef = reader.GetString();
                            break;

                        case "childrenOnly":
                            isChildrenOnly = reader.GetBoolean();
                            break;

                        default:
                            break;
                    }
                }
            }

            return new CategoryModel(id, name, relationRef, isChildrenOnly);
        }

        public override void Write(Utf8JsonWriter writer, CategoryModel value, JsonSerializerOptions options)
        { }


    }
}
