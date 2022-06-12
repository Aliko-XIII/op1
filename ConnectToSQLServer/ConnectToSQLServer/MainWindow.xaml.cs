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

        private void BookInf_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var bookInf = new BookInfWin();
            bookInf.ExitButtonM.Visibility = Visibility.Visible;
            bookInf.ExitButtonR.Visibility = Visibility.Hidden;
            bookInf.Show();
        }

        private void ReaderInf_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var readerInf = new ReaderInfWin();
            readerInf.ExitButtonM.Visibility = Visibility.Visible;
            readerInf.ExitButtonR.Visibility = Visibility.Hidden;
            readerInf.Show();
        }

        private void HallInf_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var hallInf = new HallInfWin();
            hallInf.ExitButtonM.Visibility = Visibility.Visible;
            hallInf.ExitButtonR.Visibility = Visibility.Hidden;
            hallInf.Show();
        }

        private void ReqInfBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var requestInf = new RequestInfWin();
            requestInf.ExitButtonM.Visibility = Visibility.Visible;
            requestInf.ExitButtonR.Visibility = Visibility.Hidden;
            requestInf.Show();
        }

        private void BookSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            BookSearchWin bookSearch = new BookSearchWin();
            Hide();
            bookSearch.Show();
        }


    }
}