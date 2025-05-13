namespace PetProjectC_NeuroWeb.Models
{
    public abstract class CategoryDataActionModel<T>
    {
        public abstract Task<List<T>> GetCategoriesAsync(string categoryName = "");
    }
}
