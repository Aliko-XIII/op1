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
    /// Логика взаимодействия для ReaderWindow.xaml
    /// </summary>
    public partial class ReaderWindow : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public ReaderWindow()
        {

            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            InfoBlock.Visibility = Visibility.Hidden;
            InitHallCB();
        }

        private void InitHallCB()
        {
            var data = new DataSet();
            connection = new SqlConnection(connectionString);
            connection.Open();
            adapter = new SqlDataAdapter("Select NameHall From Halls;", connectionString);
            adapter.Fill(data);
            List<string> halls = new List<string>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                halls.Add(row["NameHall"].ToString());
            }
            connection.Close();
            HallCB.ItemsSource = halls;

        }

        private void ChangeData(string SQLQuery)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddButton1_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != string.Empty &&
                NameBox.Text != string.Empty &&
                SurnameBox.Text != string.Empty &&
                AdressBox.Text != string.Empty &&
                PassportBox.Text != string.Empty &&
                BirthBox.Text != string.Empty &&
                EducationBox.Text != string.Empty &&
                PhoneBox.Text != string.Empty &&
                RegistrationBox.Text != string.Empty &&
                HallCB.SelectedIndex > -1 &&
                DegreeCB.SelectedIndex > -1)
            {
                if (!InTable(IDBox.Text))
                {
                    string sqlQ = "declare @id int;" +
                        "declare @name varchar(30);" +
                        "declare @surname varchar(30);" +
                        "declare @adress varchar(50);" +
                        "declare @passport int;" +
                        "declare @birthdate datetime;" +
                        "declare @education varchar(30);" +
                        "declare @registration datetime;" +
                        "declare @phone nchar(12);" +
                        " declare @degree varchar(30);" +
                        "declare @hall varchar(30);" +
                        "declare @idhall int;" +
                        $"set @id = {IDBox.Text};" +
                        $"set @name = '{NameBox.Text}';" +
                        $"set @surname = '{SurnameBox.Text}';" +
                        $"set @adress = '{AdressBox.Text}';" +
                        $"set @passport = {PassportBox.Text};" +
                        $"set @birthdate = '{BirthBox.Text}';" +
                        $"set @education = '{EducationBox.Text}';" +
                        $"set @registration = '{RegistrationBox.Text}';" +
                        $"set @degree = '{DegreeCB.SelectedValue}';" +
                        $"set @phone = '{PhoneBox.Text}';" +
                        $"set @hall = '{HallCB.SelectedItem.ToString()}';" +
                        "insert into Readers(IDReader, Name, Surname, Adress, Passport, BirthDate, Education, Degree, Registration, Phone)" +
                        " values(@id, @name, @surname, @adress, @passport, @birthdate, @education, @degree, @registration, @phone);" +
                        "set @idhall = (select IDHall from Halls where NameHall = @hall);" +
                        "insert into ReaderHall(IDReader, IDHall) values(@id, @idhall); ";
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
                    MessageBox.Show("Такий номер читацького квитка вже існує.\nМоже ви хотіли оновити дані?");
                }
            }
            else
            {
                MessageBox.Show("Ви не заповнили усі поля");
            }
            
        }

        private void ClearButton1_Click(object sender, RoutedEventArgs e)
        {
            IDBox.Clear();
            NameBox.Clear();
            SurnameBox.Clear();
            PassportBox.Clear();
            PhoneBox.Clear();
            BirthBox.Clear();
            AdressBox.Clear();
            HallCB.SelectedIndex= -1;
            EducationBox.Clear();
            RegistrationBox.Clear();
            DegreeCB.SelectedIndex = -1;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Visible;
        }

        private void InfoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Hidden;
        }

        private void UpdateButton1_Click(object sender, RoutedEventArgs e)
        {
            
            if (IDBox.Text != String.Empty)
            {
                string sqlQ=String.Empty;
                if (NameBox.Text != String.Empty)
                {
                    sqlQ += $"Update Readers set Name='{NameBox.Text}' where IDReader={IDBox.Text};";
                }
                if (SurnameBox.Text != String.Empty)
                {
                    sqlQ += $"Update Readers set Surname='{SurnameBox.Text}' where IDReader={IDBox.Text};";
                }
                if(PassportBox.Text != String.Empty)
                {
                    sqlQ += $"Update Readers set Passport={PassportBox.Text} where IDReader={IDBox.Text};";
                }
                if(PhoneBox.Text != String.Empty)
                {
                    sqlQ += $"Update Readers set Phone='{PhoneBox.Text}' where IDReader={IDBox.Text};";
                }
                if(BirthBox.Text!= String.Empty)
                {
                    sqlQ += $"Update Readers set BirthDate='{BirthBox.Text}' where IDReader={IDBox.Text};";
                }
                if(AdressBox.Text!= String.Empty)
                {
                    sqlQ += $"Update Readers set Adress='{AdressBox.Text}' where IDReader={IDBox.Text};";

                }
                if(HallCB.SelectedIndex>-1)
                {
                    sqlQ += $" declare @idhall int;" +
                        $" set @idhall=(select IDHall from Halls where NameHall='{HallCB.SelectedItem}');" +
                        $" Update ReaderHall set IDHall=@idhall where IDReader={IDBox.Text};";

                }
                if (EducationBox.Text != String.Empty)
                {
                    sqlQ += $" Update Readers set Education='{EducationBox.Text}' where IDReader={IDBox.Text};";

                }
                if(RegistrationBox.Text != String.Empty)
                {
                    sqlQ += $" Update Readers set Registration='{RegistrationBox.Text}' where IDReader={IDBox.Text};";
                }
                if (DegreeCB.SelectedIndex > -1)
                {
                    MessageBox.Show((string)DegreeCB.SelectedValue);
                    sqlQ += $" Update Readers set Degree='{DegreeCB.SelectedValue}' where IDReader={IDBox.Text};";
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
                MessageBox.Show("Введіть номер читацького квитка");
            }
        }

        private void DelButton1_Click(object sender, RoutedEventArgs e)
        {
            if(IDBox.Text!= String.Empty)
            {
                string sqlQ = $"Delete from Readers where IDReader={IDBox.Text};";
                ChangeData(sqlQ);
            }
            else
            {
                MessageBox.Show("Введіть номер читацького квитка");
            }
        }
        
        private bool InTable(string id)
        {
            string SQLQuery = "Select IDReader from Readers";
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string> id_list = new List<string>();
            while (reader.Read())
            {
                id_list.Add(Convert.ToString(reader.GetValue(0)));
            }
            connection.Close();
            return id_list.Contains(id);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            ReaderInfWin readerInf = new ReaderInfWin();
            readerInf.ExitButtonM.Visibility = Visibility.Hidden;
            readerInf.ExitButtonR.Visibility = Visibility.Visible;
            readerInf.Show();
        }
    }
}
