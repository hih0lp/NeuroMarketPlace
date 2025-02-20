namespace NeuroWeb.Models
{
    public class CategoryDTOModel
    {
        public CategoryDTOModel() { }
        public int id;
        public string name;
        public string relationRef;
        public bool isChildrenOnly;
        
    }




    public class DataBaseCategoryModel
    {
        public DataBaseCategoryModel()
        {

        }

        public DataBaseCategoryModel(int id, string name, string relationRef, bool isChildrenOnly, List<DataBaseProductModel>? products)
        {
            _id = id;
            _name = name;
            _relationRef = relationRef;
            _isChildrenOnly = isChildrenOnly;
            _products = products;
        }

        private int _id;
        private string _name;
        private string _relationRef;
        private bool _isChildrenOnly;
        private List<DataBaseProductModel> _products;


        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string RelationRef { get { return _relationRef; } set { _relationRef = value; } }
        public bool IsChildrenOnly { get { return _isChildrenOnly; } set { _isChildrenOnly = value; } }
        public List<DataBaseProductModel> Products { get { return _products; } set { _products = value; } }
    }
}
