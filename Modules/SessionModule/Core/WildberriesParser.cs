using PetProjectC_NeuroWeb.Modules.ProductModule.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Aspose.Html;
using Aspose.Html.Net;
using Microsoft.EntityFrameworkCore;


namespace PetProjectC_NeuroWeb.Modules.SessionModule.Core
{
    public class PageParserWildberries
    {

        public void ParsePage(string connectionString)
        {
            string _connectionString = connectionString;
            var filesInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "images", "productImages")).GetFiles();
            int filesLength = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "images", "productImages")).GetFiles().Length; //количество файлов в папке images
            int numberOfFile = 0;

            List<string> fileNames = new List<string>();

            foreach (var file in filesInfo)
            {
                fileNames.Add(file.Name);
                Console.WriteLine(file.Name);
                //Console.WriteLine(1);
            }

            //var replacableFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "images"));


            using var document = new HTMLDocument();

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\";

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(connectionString);


            var cookiesButton = driver.FindElement(By.XPath(
                "//div[@class='cookies']" +
                "/button[@class='cookies__btn btn-minor-md']")); //кнопка окей на кукисе

            cookiesButton.Click();

            while (true)
            {

                Thread.Sleep(5000); //delay для загрузки страницы




                IJavaScriptExecutor exec = (IJavaScriptExecutor)driver;



                int scrollHeight = 200;
                List<string> productsHrefsList = new List<string>();
                driver.Manage().Window.Size = new System.Drawing.Size(1400, 4000);
                exec.ExecuteScript("document.body.style.zoom='0.45'");

                while (scrollHeight < 2200) //скролл вниз
                {
                    exec.ExecuteScript($"window.scrollTo(0, 0)");//{scrollHeight}
                    exec.ExecuteScript($"window.scrollTo(0, document.body.scrollHeight - {scrollHeight})");//{scrollHeight}
                    scrollHeight += 200;
                    Thread.Sleep(1000); //скролл страницы
                    var productsHrefs = driver.FindElements(By.XPath(
                                "//a[@class='product-card__link j-card-link j-open-full-product-card']")).ToList().Distinct();//[@class='product-card__link j-card-link j-open-full-product-card']  

                    //"//div[@class='catalog-page']" +
                    //"/div[@class='catalog-page__main new-size']" +
                    //"/div[@class='catalog-page__content']" +
                    //"/div[@class='product-card-overflow']" +
                    //"/div[@class='product-card-list']" +
                    //"/article[@class='product-card j-card-item j-analitics-item   product-card--adv ']" +
                    //"/div[@class='product-card__wrapper']" +


                    foreach (var href in productsHrefs)
                    {
                        //if (!productsHrefsList.Contains(href.GetAttribute("href")))
                        if (!productsHrefsList.Contains(href.GetAttribute("href")))
                        {
                            productsHrefsList.Add(href.GetAttribute("href"));

                        }

                    }

                    foreach (var href in productsHrefs)
                    {
                        Console.WriteLine(href);
                    }


                }

                var thisPage = driver.FindElement(By.XPath(
                    "//span[@class='pagination-item pagination__item active']"
                    )); //элемент номера этой страницы
                string nextPageRef;

                try
                {

                    var nextPage = driver.FindElement(By.LinkText(Convert.ToString(Convert.ToInt32(thisPage.Text) + 1)));
                    nextPageRef = nextPage.GetAttribute("href"); //сохранение ссылки на следующую страницу
                }
                catch (Exception ex)
                {

                    nextPageRef = null;
                }



                try
                {


                    Thread.Sleep(3000);



                    int hrefs = 0;

                    //Console.WriteLine(1);


                    foreach (var rfrnce in productsHrefsList)
                    {

                        driver.Navigate().GoToUrl(rfrnce);
                        Thread.Sleep(6000);

                        IWebElement imageElem;


                        Thread.Sleep(3500);
                        var image = driver.FindElement(By.XPath("//div[@class='slide__content img-plug']/img")); //получение картинки




                        using var request = new RequestMessage(image.GetAttribute("src"));

                        using var response = document.Context.Network.Send(request);


                        using (var db = new DataBase())
                        {

                            string id = Guid.NewGuid().ToString();


                            var productcard = rfrnce;
                            var name = driver.FindElement(By.ClassName("product-page__title")).Text; //получение имени


                            Comment comment;
                            try
                            {
                                comment = new Comment(id, FindComment(driver, exec, id, productcard));//попытка создать коммент

                            }
                            catch (Exception ex)
                            {
                                comment = new Comment(id, "Нет отзыва");
                            }


                            if (numberOfFile < filesLength)
                            {
                                //Product newProduct = new Product(id, comment, name);

                                var products = db.Products.ToList();
                                var comments = db.Reviews.ToList();

                                var findingProduct = products.FirstOrDefault(u => u.Id == fileNames[numberOfFile].Split(".").First());
                                var findingComment = comments.FirstOrDefault(u => u.Id == fileNames[numberOfFile].Split(".").First());

                                Console.WriteLine(fileNames[numberOfFile].Split(".").First());
                                db.Products.Where(u => u.Id == findingProduct.Id)
                                            .ExecuteUpdate(s => s
                                                    .SetProperty(u => u.Name, u => name));
                                //.SetProperty(f => f.Comment, f => comment));


                                db.Reviews.Where(u => u.ProductId == findingProduct.Id)
                                            .ExecuteUpdate(s => s
                                                    .SetProperty(u => u.Review, u => comment.Review));


                                //findingProduct = newProduct;
                                File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "images", "productImages",fileNames[numberOfFile]), response.Content.ReadAsByteArray());
                                numberOfFile++;

                                db.SaveChanges();


                                if (hrefs >= productsHrefsList.Count)
                                {

                                    break;
                                }
                            }

                            else
                            {
                                Product product = new Product(id, comment, name);
                                //Console.WriteLine("Ready");

                                Thread.Sleep(500);


                                db.Products.Add(product);
                                db.Reviews.Add(comment);
                                db.SaveChanges();


                                File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "images", $"{id}.jpg"), response.Content.ReadAsByteArray());

                                //Console.WriteLine("Изображение записал");
                                hrefs++;


                                Console.WriteLine(productsHrefsList.Count);
                                Console.WriteLine(hrefs);


                                if (hrefs >= productsHrefsList.Count)
                                {

                                    break;
                                }
                            }
                        }
                    }


                    if (hrefs >= productsHrefsList.Count) //выход за границы списка => перелистывание на другую страницу
                    {

                        if (nextPageRef == null)
                        {
                            throw new Exception();
                        }

                        driver.Navigate().GoToUrl(nextPageRef);
                        Console.WriteLine("Страницу перевернул");
                        productsHrefsList = null;
                    }



                    else
                    {
                        throw new Exception();
                    }

                }


                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    driver.Quit();
                    break;
                }

            }

        }




        private string FindComment(IWebDriver driver, IJavaScriptExecutor exec, string id, string productCard)
        {

            Thread.Sleep(5000);

            //Console.WriteLine("Зашел на страницу");




            var allCommentsButton = driver.FindElement(By.XPath("//a[@class='product-review j-wba-card-item']"));

            allCommentsButton.Click();

            //Console.WriteLine("Зашел во все отзывы");

            Thread.Sleep(2000);




            int j = 0; //скролл переменная

            while (j != 2400)
            {
                Thread.Sleep(100);
                exec.ExecuteScript($"window.scrollTo(0, document.body.scrollHeight - {j})");
                j += 200;
            }

            var reviews = driver.FindElements(By.XPath("//p[@class='feedback__text j-feedback__text show']")); //получение текста отзыва

            List<string> reviewsAr = new List<string>(); //массив для сравнения размеров строк и вывола самого большого
            int minIndexOfBiggestString = 0;



            foreach (var review in reviews) //добавление всех строк в массив
            {
                reviewsAr.Add(review.Text);
            }

            //for (int i = 0; i < reviewsAr.Count; i++)
            //{
            //    for (int k = i; k < reviewsAr.Count; k++)
            //    {
            //        if (reviewsAr[i].Length > reviewsAr[k].Length)
            //        {
            //            minIndexOfBiggestString = k; //нахождение элемента массива с наибольшим количеством слов
            //        }
            //    }
            //}
            //Console.WriteLine("Спиздил отзывы");

            //Console.WriteLine(minIndexOfBiggestString);

            string returnReview;

            try
            {

                returnReview = reviewsAr[reviewsAr.IndexOf(reviewsAr.Max())]; //создание объекта 

            }
            catch (Exception ex)
            {

                returnReview = "Отзыва нет";

            }

            Console.WriteLine("КОММЕНТАРИЙ ГОТОВ");

            reviewsAr = null;

            //Comment rv = new Comment();

            return returnReview;

        }

    }


}
