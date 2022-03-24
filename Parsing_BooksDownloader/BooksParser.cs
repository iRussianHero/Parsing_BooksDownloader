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
                    var xSite = site.Remove(0, index+8);
                    var xIndex = site.IndexOf("/")-3;
                    url = xSite.Remove(xIndex);

                    site = site.Remove(0, index + 16);
                    index = site.IndexOf("</a");
                    name = site.Remove(index);

                    books.Add(new Book() { Name = name, Url = url });

                    index = site.IndexOf("_blank");
                }
            }
            return books;
        }
    }
}
