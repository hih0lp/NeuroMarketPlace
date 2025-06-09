using NeuroWeb;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.ClientConverters;
using PetProjectC_NeuroWeb.JSONConverters.WildberriesJSONConverter.DataBaseConverters;
using PetProjectC_NeuroWeb.Models;
using PetProjectC_NeuroWeb.Models.Products;
using System.Text.Json;

namespace PetProjectC_NeuroWeb.Models.CategoryModels
{
    public class WildberriesCategoryModel
    {
        public WildberriesCategoryModel() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string RelationRef { get; set; }
        public bool IsChildrenOnly { get; set; } 
        public string? PictureUrl { get; set; }
        public List<ProductModel>? Products { get; set; }
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
                List<WildberriesCategoryModel> gettingCategoryList = new List<WildberriesCategoryModel>();



                foreach (var category in categoryList)
                {
                    if (categoryName != string.Empty)
                    {
                        if (category.Name == categoryName)
                        {

                            categoryUrl = category.RelationRef;
                            //Console.WriteLine(category.RelationRef);

                            //Получений подкатегорий путем просмотра количества слэшей и сравнивания присылаемых значений
                            gettingCategoryList = categoryList.Where(x => x.RelationRef.Split('/').Count() == categoryUrl.Split('/').Count() + 1).Where(x => x.RelationRef.Contains(categoryUrl)).ToList();

                            //Присваивание картинки из продуктов (ДОДЕЛАТЬ ААААА)
                            foreach (var categoryDTO in gettingCategoryList)
                            {
                                categoryDTO.PictureUrl = "https://basket-12.wbbasket.ru/vol1731/part173167/173167093/images/big/1.webp";
                            }

                            return gettingCategoryList;
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
                            gettingCategoryList.Add(categoryDTO);
                            //Console.WriteLine(111111);
                        }
                    }
                }
                return gettingCategoryList;
            }
        }
    }
}
//Протестить парсер на данной модели, в случае неудачи, вероятно, в случае неудачи раскомментировать