namespace WBParser
{
    public class Category
    {
        public Category(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string ShardKey { get; set; } = null!;
    }
}
