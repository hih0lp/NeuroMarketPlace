namespace NeuroWeb.Models
{
    public record class ProductDTOModel(int id, string name, string brand, int reviewRating, string categoryName);

    public record class DataBaseProductModel(int id, string name, string brand, int reviewRating, string categoryName, DataBaseCategoryModel? category);

}
