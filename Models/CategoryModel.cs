using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using System.Text.Json;

namespace NeuroWeb.Models
{

    public class DataBaseCategoryModel //класс для базы данных
    {
        public DataBaseCategoryModel()
        {

        }

        public DataBaseCategoryModel(int id, string name, string relationRef, bool isChildrenOnly)
        {
            _id = id;
            _name = name;
            _relationRef = relationRef;
            _isChildrenOnly = isChildrenOnly;
        }

        private int _id;
        private string _name;
        private string _relationRef;
        private bool _isChildrenOnly;

        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string RelationRef { get { return _relationRef; } set { _relationRef = value; } }
        public bool IsChildrenOnly { get { return _isChildrenOnly; } set { _isChildrenOnly = value; } }

    }


    public class CategoryModel //класс для действий с категориями и взаимодействием с базой данных
    {
        public CategoryModel() { }

        public CategoryModel(int id, string name, string relationRef, bool isChildrenOnly)
        {
            _id = id;
            _name = name;
            _relationRef = relationRef;
            _isChildrenOnly = isChildrenOnly;
        }

        private int _id;
        private string _name;
        private string _relationRef;
        private bool _isChildrenOnly;
        private string _pictureUrl;


        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string RelationRef { get { return _relationRef; } set { _relationRef = value; } }
        public bool IsChildrenOnly { get { return _isChildrenOnly; } set { _isChildrenOnly = value; } }
        public string PictureUrl { get { return _pictureUrl; } set { _pictureUrl = value; } }



        public async Task<List<CategoryModel>> GetMainCategoriesAsync()
        { 
            using (var db = new DataBase())
            {
                var products = db.ProductModels.ToList();
                var categories = db.CategoryModels.ToList();
                List<CategoryModel> categoryDTOList = new List<CategoryModel>();


                foreach (var category in categories)
                {
                    CategoryModel categoryDTO = new CategoryModel();

                    categoryDTO.Name = category.Name;
                    categoryDTO.PictureUrl = "https://basket-12.wbbasket.ru/vol1731/part173167/173167093/images/big/1.webp";

                    //categoryDTO.pictureUrl = products.Where(p => p.CategoryName == category.Name).FirstOrDefault().PictureUrl; //это верно

                    if (category.IsChildrenOnly)
                    {
                        categoryDTOList.Add(categoryDTO);
                    }
                }

                return categoryDTOList;
            }
        }
    }
}

//Сделать логику здесь и перенести ее из контроллера