using System.Text.Json;
using System.Text.Json.Serialization;

namespace WBParser.Category
{
    public class CategoryConverter : JsonConverter<List<Category>>
    {
        public override List<Category>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<Category>? categories = new List<Category>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        case "id":
                            categories.Add(new Category(reader.GetInt32()));
                            break;
                        case "name":
                            categories.Last().Name = reader.GetString();
                            break;
                        case "url":
                            categories.Last().Url = reader.GetString();
                            break;
                        case "shardkey":
                            categories.Last().ShardKey = reader.GetString();
                            break;
                        case "childrenonly":
                            categories.Last().ChildrenOnly = reader.GetBoolean();
                            break;
                    }
                }
            }
            return categories;
        }

        public override void Write(Utf8JsonWriter writer, List<Category> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var category in value)
            {
                writer.WriteNumber("id", category.Id);
                writer.WriteString("name", category.Name);
                writer.WriteString("url", category.Url);
                writer.WriteBoolean("childrenOnly", category.ChildrenOnly);
            }

            writer.WriteEndArray();
        }
    }
}
