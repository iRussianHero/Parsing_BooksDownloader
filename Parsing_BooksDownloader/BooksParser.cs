using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing_BooksDownloader
{
    class BooksParser
    {
        List<Book> books;

        public List<Book> GetBooks (string site)
        {
            books = new List<Book>();
            string name = "";
            string url = "";
            int index = site.IndexOf("_blank");
            while (index>=0)
            {
                site = site.Remove(0, index + 6);
                index = site.IndexOf("href");
                if (index >= 0)
                {
                    //  Парсинг URL книги
                    var xSite = site.Remove(0, index+8);
                    var xIndex = site.IndexOf(">")-12;
                    url = xSite.Remove(xIndex);

                    //  Парсинг Name книги
                    var ySite = site.Remove(0, site.IndexOf(">")+1);
                    site = site.Remove(0, index + 16);
                    index = site.IndexOf("</a");
                    name = ySite.Remove(index);

                    books.Add(new Book() { Name = name, Url = "https://tululu.org/txt.php?id=" + url });

                    //  Переход к следующему результату поиска
                    index = site.IndexOf("_blank");
                }
            }
            return books;
        }
    }
}
