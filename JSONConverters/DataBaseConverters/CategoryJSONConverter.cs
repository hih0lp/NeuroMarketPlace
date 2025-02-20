using System.Text.Json.Serialization;
using System.Text.Json;
using NeuroWeb.Models;

namespace NeuroWeb.JSONConverters.DataBaseConverters
{
    public class CategoryJSONConverter : JsonConverter<List<CategoryDTOModel>>
    {
        public override List<CategoryDTOModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<CategoryDTOModel> models = new List<CategoryDTOModel>();
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
                            models.Add(new CategoryDTOModel());
                            id = reader.GetInt32();
                            models.Last().id = id;
                            break;

                        case "name":
                            name = reader.GetString();
                            models.Last().name = name;
                            break;

                        case "url":
                            relationRef = reader.GetString();
                            models.Last().relationRef = relationRef;
                            break;

                        case "childrenOnly":
                            isChildrenOnly = reader.GetBoolean();
                            models.Last().isChildrenOnly = isChildrenOnly;
                            break;

                    }
                }
            }

            return models;
        }

        public override void Write(Utf8JsonWriter writer, List<CategoryDTOModel> value, JsonSerializerOptions options)
        { }


    }
}
