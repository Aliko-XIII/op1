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



namespace PR3
{
    /// <summary>
    /// Логика взаимодействия для Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable dT;
        int LenTable;
        int index;

        public Administration()
        {
            InitializeComponent();            
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            UpdateDataTable();
            RealAdminPasswd.Password = ""; RealAdminPasswd.IsEnabled = false;
            NewAdminPasswd.Password = ""; NewAdminPasswd.IsEnabled = false;
            NewAdminPasswd2.Password = ""; NewAdminPasswd2.IsEnabled = false;
            PrevButton.IsEnabled = false; NextButton.IsEnabled = false;
            UpdatePasswdButton.IsEnabled = false;
            AddUserButton.IsEnabled = false;
            CorrectStatusBtn.IsEnabled = false;
            CorrectRestrictionBtn.IsEnabled = false;
            dT.Clear();
            dataGrid.ItemsSource = dT.DefaultView;
            AdminPasswd.Password = "";
            UsersLogins.ItemsSource = "";
        }

        private void UpdatePasswd_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            String strQ;
            String RealPass = RealAdminPasswd.Password.ToString();
            String NewPass = NewAdminPasswd.Password.ToString();
            String NewPass2 = NewAdminPasswd2.Password.ToString();
            if ((RealPass == AdminPasswd.Password.ToString()) 
            && (NewPass != String.Empty)
            && (NewPass == NewPass2))

            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    strQ = "UPDATE MainTable SET Password ='" + NewPass + "'WHERE Login = 'ADMIN'; ";
                    command = new SqlCommand(strQ, connection);
                    command.ExecuteNonQuery();
                }
            }
            connection.Close();
            UpdateDataTable();
        }

        void UpdateDataTable()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                adapter = new SqlDataAdapter("SELECT Name, Surname, Login, Status, Restriction FROM MainTable", connection);
                dT = new DataTable("Користувачі системи");
                adapter.Fill(dT);
                dataGrid.ItemsSource = dT.DefaultView;
                LenTable = dT.Rows.Count;
            }
            connection.Close();
        }
        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                UserNameSelected.Content = dT.Rows[index][0].ToString();
                UserSurnameSelected.Content = dT.Rows[index][1].ToString();
                UserLoginSelected.Content = dT.Rows[index][2].ToString();
                UserStatusSelected.Content = dT.Rows[index][3].ToString();
                ChangeActive.IsChecked = Boolean.Parse(dT.Rows[index][3].ToString().ToLower());
                UserRestrictionSelected.Content = dT.Rows[index][4].ToString();
                ChangeRestriction.IsChecked = Boolean.Parse(dT.Rows[index][4].ToString().ToLower());

            }
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (index < LenTable - 1)
            {
                index++;
                UserNameSelected.Content = dT.Rows[index][0].ToString();
                UserSurnameSelected.Content = dT.Rows[index][1].ToString();
                UserLoginSelected.Content = dT.Rows[index][2].ToString();
                UserStatusSelected.Content = dT.Rows[index][3].ToString();
                ChangeActive.IsChecked = Boolean.Parse(dT.Rows[index][3].ToString().ToLower());
                UserRestrictionSelected.Content = dT.Rows[index][4].ToString();
                ChangeRestriction.IsChecked = Boolean.Parse(dT.Rows[index][4].ToString().ToLower());

            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            String strQ;
            String UserLogin = AddingUserLogin.Text;
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    strQ = "INSERT INTO MainTable (Name, Surname, Login, Status,Restriction) values('', '', '" + UserLogin + "', 1, 0); ";
                    command = new SqlCommand(strQ, connection);
                    command.ExecuteNonQuery();
                }
                UpdateDataTable();
                SetUserList();
            }
            catch
            {
                MessageBox.Show("Користувача не додано! Можливо такий в базі вже є!");
            }
            connection.Close();
            UpdateDataTable();

        }

        private void CorrectStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            String strQ;
            bool UserStatus = (bool)ChangeActive.IsChecked;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Status ='" + UserStatus + "' WHERE Login='" +
                UsersLogins.SelectedValue.ToString() + "';";
                command = new SqlCommand(strQ, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            UpdateDataTable();

        }

        private void CorrectRestrictionBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            String strQ;
            bool UserRestriction = (bool)ChangeRestriction.IsChecked;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Restriction ='" + UserRestriction + "' WHERE Login = '" + UsersLogins.SelectedValue.ToString() + "'; ";
                command = new SqlCommand(strQ, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            UpdateDataTable();
        }

        private void ExitFromSystem_Click(object sender, RoutedEventArgs e)
        {
            RealAdminPasswd.Password = ""; RealAdminPasswd.IsEnabled = false;
            NewAdminPasswd.Password = ""; NewAdminPasswd.IsEnabled = false;
            NewAdminPasswd2.Password = ""; NewAdminPasswd2.IsEnabled = false;
            PrevButton.IsEnabled = false; NextButton.IsEnabled = false;
            UpdatePasswdButton.IsEnabled = false;
            AddUserButton.IsEnabled = false;
            CorrectStatusBtn.IsEnabled = false;
            CorrectRestrictionBtn.IsEnabled = false;
            dT.Clear();
            dataGrid.ItemsSource = dT.DefaultView;
            AdminPasswd.Password = "";
            UsersLogins.ItemsSource = "";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        void SetUserList()
        {
            var data = new DataSet();
            connection = new SqlConnection(connectionString);
            connection.Open();
            adapter = new SqlDataAdapter("Select Login From MainTable;", connectionString);
            adapter.Fill(data);
            List<string> logins = new List<string>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                logins.Add(row["Login"].ToString());
            }
            connection.Close();
            UsersLogins.ItemsSource = logins;
        }
        private void AuthoBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSet data = new DataSet();
            adapter = new SqlDataAdapter("Select Password From MainTable WHERE Login='ADMIN';", connectionString);
            adapter.Fill(data);
            string password= data.Tables[0].Rows[0]["Password"].ToString();
            if (AdminPasswd.Password == password)
            {
                UpdateDataTable();
                RealAdminPasswd.IsEnabled = true;
                NewAdminPasswd.IsEnabled = true;
                NewAdminPasswd2.IsEnabled = true;
                PrevButton.IsEnabled = true; NextButton.IsEnabled = true;
                UpdatePasswdButton.IsEnabled = true;
                AddUserButton.IsEnabled = true;
                CorrectStatusBtn.IsEnabled = true;
                CorrectRestrictionBtn.IsEnabled = true;
                SetUserList();
                index =0;
                UserNameSelected.Content = dT.Rows[index][0].ToString();
                UserSurnameSelected.Content = dT.Rows[index][1].ToString();
                UserLoginSelected.Content = dT.Rows[index][2].ToString();
                UserStatusSelected.Content = dT.Rows[index][3].ToString();
                UserRestrictionSelected.Content = dT.Rows[index][4].ToString();
                ChangeActive.IsChecked = Boolean.Parse(dT.Rows[index][3].ToString().ToLower());
                ChangeRestriction.IsChecked = Boolean.Parse(dT.Rows[index][4].ToString().ToLower());
            }
            else
            {
                AdminPasswd.Clear();
                MessageBox.Show("Невірний пароль адміністратора!");
            }


        }

        private void UsersLogins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i =0; i < dT.Rows.Count; i++)
            {
                if ((string)dT.Rows[i][2] == (string)UsersLogins.SelectedItem)
                {
                    index = i;
                }
            }
            UserNameSelected.Content = dT.Rows[index][0].ToString();
            UserSurnameSelected.Content = dT.Rows[index][1].ToString();
            UserLoginSelected.Content = dT.Rows[index][2].ToString();
            UserStatusSelected.Content = dT.Rows[index][3].ToString();
            ChangeActive.IsChecked = Boolean.Parse(dT.Rows[index][3].ToString().ToLower());
            UserRestrictionSelected.Content = dT.Rows[index][4].ToString();
            ChangeRestriction.IsChecked = Boolean.Parse(dT.Rows[index][4].ToString().ToLower());
        }
    }
}
