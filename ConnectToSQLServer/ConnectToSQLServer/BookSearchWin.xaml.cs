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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ConnectToSQLServer
{
    /// <summary>
    /// Логика взаимодействия для LibrarianWindow.xaml
    /// </summary>
    public partial class BookSearchWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public BookSearchWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void InfoBtnR_Click(object sender, RoutedEventArgs e)
        {
            ReaderInfWin readerInf = new ReaderInfWin();
            readerInf.ExitButtonM.Visibility = Visibility.Hidden;
            readerInf.ExitButtonR.Visibility = Visibility.Visible;
            readerInf.Show();
        }
        private void InfoBtnB_Click(object sender, RoutedEventArgs e)
        {
            BookInfWin bookInf = new BookInfWin();
            bookInf.ExitButtonM.Visibility = Visibility.Hidden;
            bookInf.ExitButtonR.Visibility = Visibility.Visible;
            bookInf.Show();
        }

    }
}
