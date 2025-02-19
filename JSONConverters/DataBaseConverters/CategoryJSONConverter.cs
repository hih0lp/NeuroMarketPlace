using System.Text.Json.Serialization;
using System.Text.Json;
using NeuroWeb.Models;

namespace NeuroWeb.JSONConverters.DataBaseConverters
{
    public class CategoryJSONConverter : JsonConverter<CategoryDTOModel>
    {
        public override CategoryDTOModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var id = 0;
            var name = "Undefined";
            var relationRef = "Undefined";
            var isChildrenOnly = true;
            var categoryName = "Undefined";

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

                        case "categoryName":
                            categoryName = reader.GetString();
                            break;

                        default:
                            break;
                    }
                }
            }

            return new CategoryDTOModel(id, name, relationRef, isChildrenOnly);
        }

        public override void Write(Utf8JsonWriter writer, CategoryDTOModel value, JsonSerializerOptions options)
        { }


    }
}
