using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Controls;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    {
        string connectionString = null;        
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            GetBooksData();
            GetReadersData();
            GetHallsData();
            GetRequests();
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

        private void GetReadersData()
        {
            string sqlQ = "Select Readers.IDReader as [Номер читацького квитка]," +
                "Readers.Surname as [Прізвище]," +
                "Readers.Adress as[Адреса]," +
                "Readers.Passport as[Номер паспорта]," +
                "Readers.Phone as [Номер телефону]," +
                "Readers.BirthDate as [Дата народження]," +
                "Readers.Education as[Освіта]," +
                "Readers.Degree as[Наукова ступінь]," +
                "Readers.Registration as [Дата реєстрації], " +
                "Halls.NameHall as [Читальна зала] "+
                "from Readers inner join "+
                "ReaderHall on Readers.IDReader=ReaderHall.IDReader inner join "+
                "Halls on ReaderHall.IDHall=Halls.IDHall;";
            try
            {
                GetAndShowData(sqlQ, ReadersDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetHallsData()
        {
            string sqlQ = "SELECT IDHall as [№], " +
                       "NameHall as [Назва], " +  
                       "Capacity as [Місткість]"+
                    "FROM Halls;";
            try
            {
                GetAndShowData(sqlQ, HallsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetRequests()
        {
            string sqlQ = "SELECT Readers.Surname as [Читач], "+
                "Books.NameBook as [Книга], " +
                "FORMAT (Requests.RequestDate, 'dd/MM/yyyy ') as [Дата]" +
                "From Requests inner join "+
                "Books on Books.IDBook=Requests.IDBookR inner join "+
                "Readers on Readers.IDReader=Requests.IDReaderR";
            try
            {
                GetAndShowData(sqlQ, RequestsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            BookWindow bw = new BookWindow();
            Hide();
            bw.Show();
        }

        private void ReaderButton_Click(object sender, RoutedEventArgs e)
        {
            ReaderWindow readw = new ReaderWindow();
            Hide();
            readw.Show();
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            RequestWindow reqw = new RequestWindow();
            Hide();
            reqw.Show();
        }

        private void HallButton_Click(object sender, RoutedEventArgs e)
        {
            HallWindow hw=new HallWindow();
            Hide();
            hw.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}