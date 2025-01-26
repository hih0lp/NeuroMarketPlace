namespace PetProjectC_NeuroWeb.Modules.ProductModule.Core
{
    public class Comment
    {
        public Comment() { }
        public Comment(string productId, string review)
        {
            _productId = productId;
            _review = review;
        }

        private string id = Guid.NewGuid().ToString();
        private string _productId;
        private string _review;


        //public string Id { get => _id; set => _id = value; }
        public string Id { get => id; set => id = value; }
        public string ProductId { get => _productId; set => _productId = value; }
        public string Review { get => _review; set => _review = value; }
        public Product Product { get; set; } = null!;
    }


}
