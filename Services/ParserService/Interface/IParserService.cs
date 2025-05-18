namespace PetProjectC_NeuroWeb.Services.ParserService.Interface
{
    public interface IParserService
    {
        public Task AddCategoryInDB();//param = object state
        public Task AddProductsInDB();//param = object state
    }
}
