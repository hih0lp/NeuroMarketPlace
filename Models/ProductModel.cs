namespace NeuroWeb.Models
{
    public record class ProductDTOModel(int id, string name, string brand, int reviewRating, string categoryName);

    public class DataBaseProductModel
    {
        public int Id { get; set; }
        public string name;
        public string brand;
        public int reviewRating;
        public string categoryName;
        public DataBaseCategoryModel? Category { get; set; }

    }

}
