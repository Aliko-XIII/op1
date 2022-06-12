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
    /// Логика взаимодействия для ReaderInfWin.xaml
    /// </summary>
    public partial class ReaderInfWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public ReaderInfWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetReadersData();
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
        private void GetReadersData()
        {
            string sqlQ = "Select Readers.IDReader as [Квиток], " +
                "Readers.Name as [Ім'я]," +
                "Readers.Surname as [Прізвище]," +
                "Readers.Adress as[Адреса]," +
                "Readers.Passport as[Паспорт]," +
                "Readers.Phone as [Телефон]," +
                "FORMAT (Readers.BirthDate, 'dd/MM/yyyy')  as [Дата народження]," +
                "Readers.Education as[Освіта]," +
                "Readers.Degree as[Наукова ступінь]," +
                "FORMAT (Readers.Registration, 'dd/MM/yyyy ') as [Дата реєстрації], " +
                "Halls.NameHall as [Читальна зала] " +
                "from Readers inner join " +
                "ReaderHall on Readers.IDReader=ReaderHall.IDReader inner join " +
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
