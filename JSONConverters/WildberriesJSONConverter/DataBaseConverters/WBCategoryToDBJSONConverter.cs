using System.Text.Json.Serialization;
using System.Text.Json;
using NeuroWeb.Models;

namespace PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters
{
    public class WBCategoryToDBJSONConverter : JsonConverter<List<WildberriesCategoryModel>>
    {
        public override List<WildberriesCategoryModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<WildberriesCategoryModel> models = new List<WildberriesCategoryModel>();
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


                    switch (propertyName)
                    {
                        case "id":
                            models.Add(new WildberriesCategoryModel());
                            id = reader.GetInt32();
                            models.Last().Id = id;
                            break;

                        case "name":
                            name = reader.GetString();
                            models.Last().Name = name;
                            break;

                        case "url":
                            relationRef = reader.GetString();
                            models.Last().RelationRef = relationRef;
                            break;

                        case "childrenOnly":
                            isChildrenOnly = reader.GetBoolean();
                            models.Last().IsChildrenOnly = isChildrenOnly;
                            break;

                    }
                }
            }

            return models;
        }

        public override void Write(Utf8JsonWriter writer, List<WildberriesCategoryModel> value, JsonSerializerOptions options)
        { }


    }
}
