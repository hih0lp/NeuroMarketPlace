namespace PetProjectC_NeuroWeb.Modules.ProductModule.Core
{
    //public class Product
    //{
    //    public Product(string id, string description)
    //    {
    //        _id = id;
    //        //_image = image;
    //        _description = description;
    //    }

    //    private string _id;
    //    //private List<string> _image;
    //    private string _description;

    //    public string Id
    //    {
    //        get { return _id; }
    //        set { _id = value; }
    //    }

    //}

    public class Product
    {

        public Product() { }
        public Product(string id, Comment? comment, string name) //byte[] binImage 
        {
            _comment = comment;
            _id = id;
            //_binImage = binImage;
            _name = name;//можно комментить
        }
        private string _id;
        private Comment? _comment;
        //private byte[] _binImage;
        private string? _name;//можно комментить

        public string Id { get => _id; set => _id = value; }
        public Comment Comment { get => _comment; set => _comment = value; } //_comments = _reviews
                                                                             //public byte[] binImage { get => _binImage; set => _binImage = value; }
        public string? Name { get => _name; set => _name = value; }

    }
}
