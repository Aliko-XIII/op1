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
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        string hour;
        string minute;
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        private void ChangeData(string SQLQuery)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }

        public RequestWindow()
        {
            InitializeComponent();
            //HourSlider.Value = HourSlider.Minimum;
            //MinuteSlider.Value = MinuteSlider.Minimum;
            minute = MinuteSlider.Minimum.ToString();
            hour = HourSlider.Minimum.ToString();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            InfoBlock.Visibility = Visibility.Hidden;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (ReqDate.Text != String.Empty)
            {
                string day = ReqDate.Text.Substring(0, 2);
                string month= ReqDate.Text.Substring(3, 2);
                string year=ReqDate.Text.Substring(6, 4);
                string sqlQ = String.Empty;
                if (BookBox.Text != String.Empty)
                {
                    sqlQ += $"Update Requests set IDBookR={BookBox.Text} where RequestDate='{year}-{month}-{day} {TimeLabel.Content}';";
                }
                if (ReaderBox.Text != String.Empty)
                {
                    sqlQ += $"Update Requests set IDReaderR={ReaderBox.Text} where RequestDate='{year}-{month}-{day} {TimeLabel.Content}';";
                }
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
                MessageBox.Show("Введіть дату");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReqDate.Text != String.Empty &&
                BookBox.Text != String.Empty &&
                ReaderBox.Text != String.Empty)
            {
                string day = ReqDate.Text.Substring(0, 2);
                string month = ReqDate.Text.Substring(3, 2);
                string year = ReqDate.Text.Substring(6, 4);

                string sqlQ = "Insert into Requests(IDReaderR, IDBookR, RequestDate)" +
                    $"values ({ReaderBox.Text},{BookBox.Text}, '{year}-{month}-{day} {TimeLabel.Content}')";

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

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReqDate.Text != String.Empty)
            {
                string day = ReqDate.Text.Substring(0, 2);
                string month = ReqDate.Text.Substring(3, 2);
                string year = ReqDate.Text.Substring(6, 4);
                string sqlQ = $"Delete from Requests where RequestDate='{year}-{month}-{day} {TimeLabel.Content}';";
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
                MessageBox.Show("Введіть дату");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            HourSlider.Value = HourSlider.Minimum;
            MinuteSlider.Value = MinuteSlider.Minimum;
            BookBox.Text = String.Empty;
            ReaderBox.Text = String.Empty;
            ReqDate.Text = String.Empty;

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Visible;
        }

        private void InfoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Hidden;
        }
        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            RequestInfWin requestInf = new RequestInfWin();
            requestInf.ExitButtonM.Visibility = Visibility.Hidden;
            requestInf.ExitButtonR.Visibility = Visibility.Visible;
            requestInf.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void HourSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            hour = HourSlider.Value< 10 ? ("0" + HourSlider.Value.ToString()) : HourSlider.Value.ToString();
            
            TimeLabel.Content =$"{hour}:{minute}";
        }

        private void MinuteSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            minute = MinuteSlider.Value < 10 ? ("0" + MinuteSlider.Value.ToString()) : MinuteSlider.Value.ToString();
            TimeLabel.Content = $"{hour}:{minute}";
        }
    }
}
