using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Parsing_BooksDownloader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FolderBrowserDialog folder;
        List<Book> books;

        public MainWindow()
        {
            InitializeComponent();
            folder = new FolderBrowserDialog();
        }

        async void DownloadAll()
        {
            WebClient client = new WebClient();

            if( folder.SelectedPath.Length == 0 || folder.SelectedPath == null)
            {
                if(folder.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
            }

            PB.Value = 0;
            PB.Maximum = books.Count;

            for (int i = 0; i < books.Count; i++)
            {
                string path = folder.SelectedPath + "\\";

                LB1.Content = books[i].Name;

                await Task.Run(()=> client.DownloadFile(new Uri(books[i].Url), path + $"{ books[i].Name }" + ".txt"));
                await Task.Run(() => Dispatcher.Invoke(new Action(() => { PB.Value += 1; })));
            }
            LB1.Content = "Готово";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LB.Items.Clear();
            LB2.Items.Clear();
            SiteToString siteToString = new SiteToString();
            string site = siteToString.GetSite("https://tululu.org/search/?q=" + TB.Text);
            BooksParser parser = new BooksParser();
            books = parser.GetBooks(site);

            foreach (Book x in books)
            {
                LB.Items.Add(x.Name);
            }
            foreach (Book x in books)
            {
                LB2.Items.Add(x.Url);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (LB.SelectedIndex < 0)
                return;

            PB.Value = 0;
            PB.Maximum = 1;

            if (folder.SelectedPath.Length == 0 || folder.SelectedPath == null)
            {
                if (folder.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
            }

            WebClient client = new WebClient();
            string path = folder.SelectedPath + "\\" + books[LB.SelectedIndex].Name + ".txt";

            client.DownloadFileAsync(new Uri(books[LB.SelectedIndex].Url), path);

            PB.Value += 1;
            LB1.Content = "Готово";
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DownloadAll();
        }
    }
}
