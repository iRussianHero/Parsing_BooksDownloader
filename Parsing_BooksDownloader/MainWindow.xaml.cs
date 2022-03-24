using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Parsing_BooksDownloader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //FolderBrowserDialog folder;
        List<Book> books;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LB.Items.Clear();
            SiteToString siteToString = new SiteToString();
            string site = siteToString.GetSite("https://tululu.org/search/?q=" + TB.Text);
            BooksParser parser = new BooksParser();
            books = parser.GetBooks(site);

            foreach (Book x in books)
            {
                LB.Items.Add(x.Url);
            }
        }

        //https://tululu.org/search/?q=
    }
}
