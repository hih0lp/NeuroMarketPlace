namespace NeuroWeb.Models
{
    public record class CategoryDTOModel(int id, string name, string relationRef, bool isChildrenOnly);

    public record class DataBaseCategoryModel(int id, string name, string relationRef, bool isChildrenOnly, List<DataBaseProductModel>? products);
}
