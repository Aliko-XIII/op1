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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace ConnectToSQLServer
{
    /// <summary>
    /// Логика взаимодействия для BookInfWin.xaml
    /// </summary>
    public partial class BookInfWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public BookInfWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetBooksData();
        }
        
        private void GetAndShowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();
        }
        private void GetBooksData()
        {
            string sqlQ = "SELECT Books.IDBook as [Шифр книги]," +
                "Books.NameBook as [Назва]," +
                "STRING_AGG(Authors.NameAuthor, ', ') as [Автори]," +
                "YEAR(Books.YearBook) as [Рік публікації]," +
                "Books.Publisher as [Видавник]," +
                "SUM(BookHall.NumBook) as [Кількість екземплярів] " +
                "from Books inner join " +
                "BookAuthor on Books.IDBook = BookAuthor.IDBookA inner join " +
                "Authors on BookAuthor.IDAuthorA = Authors.IDAuthor inner join " +
                "BookHall on Books.IDBook=BookHall.IDBookH inner join " +
                "Halls on IDHallH=IDHall " +
                "group by IDBook, NameBook, Publisher, YearBook;";
            try
            {
                GetAndShowData(sqlQ, BooksDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void ExitButtonM_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void ExitButtonR_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
