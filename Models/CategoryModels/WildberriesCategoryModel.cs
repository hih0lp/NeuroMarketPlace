using NeuroWeb;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using PetProjectC_NeuroWeb.Models;
using System.Text.Json;

namespace PetProjectC_NeuroWeb.Models.CategoryModels
{
    public class WildberriesCategoryModel
    {
        public WildberriesCategoryModel() { }

        public WildberriesCategoryModel(int id, string name, string relationRef, bool isChildrenOnly, string pictureUrl)// string marketPlace
        {
            _id = id;
            _name = name;
            _relationRef = relationRef;
            _isChildrenOnly = isChildrenOnly;
            _pictureUrl = pictureUrl;
            //_marketPlace = marketPlace;
        }


        private int _id;
        private string _name = null!;
        private string _relationRef = null!;
        private bool _isChildrenOnly;
        private string? _pictureUrl;
        //private string _marketPlace;


        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string RelationRef { get { return _relationRef; } set { _relationRef = value; } }
        public bool IsChildrenOnly { get { return _isChildrenOnly; } set { _isChildrenOnly = value; } }
        public string? PictureUrl { get { return _pictureUrl; } set { _pictureUrl = value; } }
        //public string MarketPlace { get { return _marketPlace; } set { _marketPlace = value; } }
    }

    public class WildberriesDataActionModel : CategoryDataActionModel<WildberriesCategoryModel>  //класс для действий с категориями и взаимодействием с базой данных
    {
        public override async Task<List<WildberriesCategoryModel>> GetCategoriesAsync(string categoryName = "")
        {
            using (var db = new DataBase())
            {
                var products = db.ProductModels.ToList();
                var categoryList = db.CategoryModels.ToList();
                string categoryUrl;
                List<WildberriesCategoryModel> categoryDTOList = new List<WildberriesCategoryModel>();



                foreach (var category in categoryList)
                {
                    if (categoryName != string.Empty)
                    {
                        if (category.Name == categoryName)
                        {
                            categoryUrl = category.RelationRef;

                            categoryDTOList = categoryList.Where(x => x.RelationRef.Split('/').Count() == categoryUrl.Split('/').Count() + 1).Where(x => x.RelationRef.Contains(categoryUrl)).ToList();

                            Console.WriteLine("подкатегории is getting");

                            return categoryDTOList;
                        }
                    }
                    else
                    {
                        WildberriesCategoryModel categoryDTO = new WildberriesCategoryModel();
                        //categoryDTO.PictureUrl = products.Where(p => p.CategoryName == category.Name).FirstOrDefault().PictureUrl; //это верно
                        categoryDTO.Name = category.Name;
                        categoryDTO.PictureUrl = "https://basket-12.wbbasket.ru/vol1731/part173167/173167093/images/big/1.webp";
                        categoryDTO.IsChildrenOnly = category.IsChildrenOnly;

                        if (categoryDTO.IsChildrenOnly)
                        {
                            categoryDTOList.Add(categoryDTO);
                            Console.WriteLine(111111);
                        }
                    }
                }
                return categoryDTOList;
            }
        }
    }
}
//Протестить парсер на данной модели, в случае неудачи, вероятно, в случае неудачи раскомментировать