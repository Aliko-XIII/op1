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
    /// Логика взаимодействия для BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public BookWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }
        private void ChangeData(string SQLQuery)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            IDBox.Clear(); 
            NameBox.Clear();
            AuthorBox.Clear();
            HallBox.Clear();
            YearBox.Clear();
            PublisherBox.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlQ =
                " declare @authorname varchar(30);" +
                $" set @authorname = '{AuthorBox.Text}';" +
                " declare @idbook int;" +
                $"set @idbook = {IDBox.Text};" +
                "declare @idauthor int;" +
                "if exists(select * from Authors where NameAuthor = @authorname)" +
                "set @idauthor = (select IDAuthor from Authors where NameAuthor = @authorname)" +
                " else" +
                " insert into dbo.Authors(NameAuthor) values(@authorname)" +
                " set @idauthor = (select IDAuthor from Authors where NameAuthor = @authorname)" +
                " declare @hallname varchar(30);" +
                " declare @hall int;" +
                $" set @hallname = '{HallBox.Text}';" +
                "set @hall = (select IDHall from Halls where NameHall = @hallname);" +
                " declare @bookname varchar(30);"+
                $" set @bookname='{NameBox.Text}';"+
                " declare @year datetime;"+
                $" set @year='01.01.{YearBox.Text}';"+
                " declare @publisher varchar(30);"+
                $"set @publisher='{PublisherBox.Text}';"+
                "insert into dbo.Books (IDBook, NameBook, Publisher, YearBook) values (@idbook,@bookname, @publisher, @year);" +
                " insert into dbo.BookHall(IDBookH, IDHallH, NumBook) values(@idbook, @hall, 1)" +
                " insert into dbo.BookAuthor(IDBookA, IDAuthorA) values(@idbook, @idauthor)";
            try
            {
                ChangeData(sqlQ);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
