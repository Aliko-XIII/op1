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
    

    public partial class RequestInfWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public RequestInfWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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

        private void GetRequests()
        {
            string sqlQ = "SELECT Readers.Surname as [Читач], " +
                "Books.NameBook as [Книга], " +
                "FORMAT (Requests.RequestDate, 'dd/MM/yyyy ') as [Дата]" +
                "From Requests inner join " +
                "Books on Books.IDBook=Requests.IDBookR inner join " +
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
