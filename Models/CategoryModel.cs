namespace NeuroWeb.Models
{
    public record class CategoryModel(int id, string name, string relationRef, bool isChildrenOnly);
}
