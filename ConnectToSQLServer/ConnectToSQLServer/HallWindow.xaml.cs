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
    /// Логика взаимодействия для HallWindow.xaml
    /// </summary>
    public partial class HallWindow : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public HallWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            InfoBlock.Visibility = Visibility.Hidden;
        }

        private void ChangeData(string SQLQuery)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != String.Empty)
            {
                string sqlQ = null;
                sqlQ = $"Delete from Halls where IDHall={IDBox.Text}; ";
                try
                {
                    ChangeData(sqlQ);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Введіть шифр зали");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text!=String.Empty &&
                CapacityBox.Text!=String.Empty)
            {
                string sqlQ =
                $"Insert into Halls(NameHall, Capacity) values ('{NameBox.Text}',{CapacityBox.Text});";
                try
                {
                    ChangeData(sqlQ);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ви не заповнили усі поля!");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != String.Empty)
            {
                string sqlQ = String.Empty;
                if(NameBox.Text != String.Empty)
                {
                    sqlQ += $"Update Halls set NameHall='{NameBox.Text}' where IDHall={IDBox.Text}; ";

                }
                if (CapacityBox.Text != String.Empty)
                {
                    sqlQ += $"Update Halls set Capacity={CapacityBox.Text} where IDHall={IDBox.Text}; ";
                }
                try
                {
                    ChangeData(sqlQ);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Введіть шифр зали");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            IDBox.Clear();
            NameBox.Clear();
            CapacityBox.Clear();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Visible;
        }

        private void InfoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Hidden;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            HallInfWin hallInf=new HallInfWin();
            hallInf.ExitButtonM.Visibility = Visibility.Hidden;
            hallInf.ExitButtonR.Visibility = Visibility.Visible;
            hallInf.Show();
        }
    }
}
