//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using CsvHelper;
//using HtmlAgilityPack;
//using System.IO;
//using System.Globalization;
//using ScrapySharp.Network;
//using ScrapySharp.Extensions;
//using ScrapySharp.Html;
//using ScrapySharp.Html.Forms;

//namespace WebSrapingApp
//{
//    class ProgramOld
//    {
//        static ScrapingBrowser _browser = new ScrapingBrowser();

//        static void Main(string[] args)
//        {
//            try
//            {
//                Console.WriteLine("Please enter a search term:");
//                var MySearchTerm = "Amy Grant";
//                //var MySearchTerm = Console.ReadLine();
//                //https://search.azlyrics.com/search.php?q=mercy+chinwo
//                var mainPageLinks = GetMainPageLinks("https://search.azlyrics.com");
//                var lstGigs = GetPageDetails(mainPageLinks, MySearchTerm);
//            }
//            catch (Exception ex)
//            {

//                Console.WriteLine(ex.Message);
//                Console.WriteLine(ex.StackTrace);
//            }


//            /*static*/
//            List<string> GetMainPageLinks(string url)
//            {
//                var homePageLinks = new List<string>();

//                try
//                {
//                    var html = GetHtml(url);
//                    var links = html.CssSelect("a");

//                    foreach (var link in links)
//                    {
//                        if (link.Attributes["href"].Value.Contains(".html"))
//                        {
//                            homePageLinks.Add(link.Attributes["href"].Value);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {

//                    Console.WriteLine(ex.Message);
//                    Console.WriteLine(ex.StackTrace);
//                }

//                return homePageLinks;
//            }


//            /*static*/
//            HtmlNode GetHtml(string url)
//            {
//                try
//                {
//                    WebPage webpage = _browser.NavigateToPage(new Uri(url));
//                    return webpage.Html;
//                }
//                catch (Exception ex)
//                {

//                    Console.WriteLine(ex.Message);
//                    Console.WriteLine(ex.StackTrace);
//                }

//                return null;
//            }

//            HtmlNode GetHtmlLinks(string url)
//            {
//                try
//                {
//                    //WebPage webpage = _browser.NavigateToPage(new Uri(url));
//                    //return webpage.Html;
//                    var doc = new HtmlDocument().LoadHtml(html: url);
//                    var divs = doc.DocumentNode.Descendants("div");
//                    var temp = divs.Select(x => x.GetDataAttributes());


//                    //IEnumerable<HyperLink> links = FindLinks(By by);
//                }
//                catch (Exception ex)
//                {

//                    Console.WriteLine(ex.Message);
//                    Console.WriteLine(ex.StackTrace);
//                }

//                return null;
//            }




//            /*static*/
//            List<PageDetails> GetPageDetails(List<string> urls, string searchTerm)
//            {
//                var lstPageDetails = new List<PageDetails>();
//                try
//                {
//                    foreach (var url in urls)
//                    {
//                        //var htmlNode = GetHtml(url);
//                        var htmlNode = GetHtmlLinks(url);
//                        var pageDetails = new PageDetails()
//                        {
//                            Title = htmlNode.OwnerDocument.DocumentNode.SelectSingleNode("//html/head/title").InnerText,
//                            Description = htmlNode.OwnerDocument.DocumentNode
//                          .SelectSingleNode("//html/body/section/section/section/section").InnerText
//                          .Replace("\n        \n            QR Code Link to This Post\n            \n        \n", ""),
//                            Url = url
//                        };


//                        //var pageDetails = new PageDetails();
//                        //pageDetails.Title = htmlNode.OwnerDocument.DocumentNode
//                        //  .SelectSingleNode("//html/head/title").InnerText;

//                        //var description = htmlNode.OwnerDocument.DocumentNode
//                        //  .SelectSingleNode("//html/body/section/section/section/section").InnerText;
//                        //pageDetails.Description = description
//                        //  .Replace("\n        \n            QR Code Link to This Post\n            \n        \n", "");

//                        //pageDetails.Url = url;

//                        var searchTermInTitle = pageDetails.Title.ToLower().Contains(searchTerm.ToLower());
//                        var searchTermInDescription = pageDetails.Description.ToLower().Contains(searchTerm.ToLower());

//                        if (searchTermInTitle || searchTermInDescription)
//                        {
//                            lstPageDetails.Add(pageDetails);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {

//                    Console.WriteLine(ex.Message);
//                    Console.WriteLine(ex.StackTrace);
//                }


//                return lstPageDetails;
//            }



//        }




















//        //static void Main(string[] args)
//        //{
//        //    HtmlWeb web = new HtmlWeb();
//        //    HtmlDocument doc = web.Load("https://search.azlyrics.com/search.php?q=mercy+chinwo");
//        //    var panels = doc.DocumentNode.SelectNodes("//div[@class='panel-heading");

//        //    foreach(var item in panels)
//        //    {
//        //        var HeaderNames = doc.DocumentNode.SelectNodes("//table[@class='table table-condensed']");
//        //        HeaderNames = HeaderNames.d
//        //    }

//        //    //var HeaderNames = doc.DocumentNode.SelectNodes("//span[@class='toctext']");



//        //    var titles = new List<Row>();
//        //    foreach (var item in HeaderNames)
//        //    {
//        //        titles.Add(new Row { Title = item.InnerText });
//        //    }

//        //    var folder = AppDomain.CurrentDomain.BaseDirectory + "/files";
//        //    if (!Directory.Exists(folder))
//        //        Directory.CreateDirectory(folder);

//        //    using (var writer = new StreamWriter($"{folder}/example.csv"))
//        //    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
//        //    {
//        //        csv.WriteRecords(titles);
//        //    }
//        //}
//    }

//    public class PageDetails
//    {
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public string Url { get; set; }
//    }
//}
