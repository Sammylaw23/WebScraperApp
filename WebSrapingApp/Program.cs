using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebSrapingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
                string baseUrl = "";
            try
            {
                //string baseUrl = "https://www.azlyrics.com/";
                string url = "https://search.azlyrics.com/";
                //string searchTerm = "Amy Grant";
                //string searchSong = "A Mighty Fortress";
                string searchTerm = "Mercy Chinwo";
                string searchSong = "Excess Love";

                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);


                var btnMenus = doc.DocumentNode.Descendants("a")
                .Where(node => node.GetAttributeValue("class", "").Equals("btn btn-menu")).ToList();

                foreach (var menu in btnMenus)
                {
                    try
                    {
                        var menuPage = menu.GetAttributeValue("href", "");
                        var httpClient2 = new HttpClient();
                        var singlePage = await httpClient2.GetStringAsync("https:" + menuPage);
                        doc.LoadHtml(singlePage);
                        var tempList = doc.DocumentNode.Descendants("a").ToList();
                        string searchTermLink = tempList.Where(x => x.OuterHtml.ToUpper()
                        .Contains(searchTerm.ToUpper())).Select(x => x.GetAttributeValue("href", "")).FirstOrDefault();
                        if (searchTermLink is null) continue;
                        var foundPage = await httpClient.GetStringAsync(baseUrl + searchTermLink);
                        doc.LoadHtml(foundPage);
                        var songList = doc.DocumentNode.Descendants("div")
                            .Where(node => node.GetAttributeValue("class", "")
                            .Equals("listalbum-item")).ToList();
                        if (songList.Count > 0)
                        {
                        }
                        else continue;


                        //string songUrl = songList.Where(x => x.InnerText.Contains(searchSong))
                        //    .Select(x=>x.Descendants("a").ToList().Select(node => node.GetAttributeValue("href", ""))).FirstOrDefault();

                        var songItem = songList.Where(x => x.InnerText.Contains(searchSong)).FirstOrDefault();
                        if (songItem is null) continue;
                        string songUrl = songItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                        var songPage = await httpClient.GetStringAsync(baseUrl + songUrl);
                        doc.LoadHtml(songPage);
                        var song = doc.DocumentNode.Descendants("div")
                            .Where(node => node.GetAttributeValue("class", "").Equals("lyricsh")).FirstOrDefault();



                        //var song = doc.DocumentNode.Descendants("div")
                        //    .Where(node => node.GetAttributeValue("class", "").Equals(" ")).FirstOrDefault().InnerHtml;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                    

                }
            }
            catch (Exception ex) when (baseUrl == string.Empty)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
