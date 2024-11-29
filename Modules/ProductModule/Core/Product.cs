namespace PetProjectC_NeuroWeb.Modules.ProductModule.Core
{
    public class Product
    {
        public Product(string id, string description)
        {
            _id = id;
            //_image = image;
            _description = description;
        }

        private string _id;
        //private List<string> _image;
        private string _description;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
