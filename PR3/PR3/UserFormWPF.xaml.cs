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
    /// Логика взаимодействия для UserFormWPF.xaml
    /// </summary>
    public partial class UserFormWPF : Window
    {
        string login;
        string passwd;
        SqlConnection connection=null;
        string connectionString=null;
        SqlDataAdapter adapter;
        DataTable dT;
        int count;
        SqlCommand command;
        public UserFormWPF()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            NewNameField.IsEnabled = false;
            NewSurnameField.IsEnabled = false;
            NewPasswdField.IsEnabled = false;
            NewPasswdField2.IsEnabled = false;
            UpdateDataBtn.IsEnabled = false;
        }

        private void AutorBtn_Click(object sender, RoutedEventArgs e)
        {
            login = loginField.Text;
            passwd = passwdField.Password;
            connection = new SqlConnection(connectionString);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                String strQ = "SELECT * FROM MainTable WHERE Login='" + login + "';";
                adapter = new SqlDataAdapter(strQ, connection);
                dT = new DataTable("Користувачі системи");
                adapter.Fill(dT);
                if (dT.Rows.Count == 0)
                {
                    MessageBoxResult result = MessageBox.Show("Такого користувача на знайдено\nХочете зареєструвати користувача?", "Помилка входу", MessageBoxButton.YesNo);
                    if(result == MessageBoxResult.No)
                    {
                        System.Windows.Application.Current.Shutdown();
                    }
                }

                else
                {
                    bool Status = Convert.ToBoolean(dT.Rows[0][4]);
                    if (!Status)
                        MessageBox.Show("Користувач заблокований Адміністратором системи!");
                    else
                    {
                        if ((bool)dT.Rows[0][5])
                        {
                            if ((dT.Rows[0][2].ToString() == login) &&

                        (dT.Rows[0][3].ToString() == passwd))

                            {
                                NewNameField.Text = dT.Rows[0][0].ToString();
                                NewSurnameField.Text = dT.Rows[0][1].ToString();
                                NewPasswdField.Password = "";
                                NewPasswdField2.Password = "";
                                NewNameField.IsEnabled = true;
                                NewSurnameField.IsEnabled = true;
                                NewPasswdField.IsEnabled = true;
                                NewPasswdField2.IsEnabled = true;
                                UpdateDataBtn.IsEnabled = true;

                            }
                            else
                            {
                                count++;

                                String s = "Невірно введений пароль! " +

                                "Помилкове введення No" + count.ToString();
                                MessageBox.Show(s);
                                if (count == 3)
                                    System.Windows.Application.Current.Shutdown();
                            }
                        }
                        else
                        {
                            if (dT.Rows[0][2].ToString() == login)
                            {
                                NewNameField.Text = dT.Rows[0][0].ToString();
                                NewSurnameField.Text = dT.Rows[0][1].ToString();
                                NewPasswdField.Password = "";
                                NewPasswdField2.Password = "";
                                NewNameField.IsEnabled = true;
                                NewSurnameField.IsEnabled = true;
                                NewPasswdField.IsEnabled = true;
                                NewPasswdField2.IsEnabled = true;
                                UpdateDataBtn.IsEnabled = true;

                            }


                        }
                    }

                }
            }
            connection.Close();
        }
        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            String nameReg = NameField.Text;
            String surnameReg = SurnameField.Text;
            String loginReg = loginRegField.Text;
            String passwdReg = passwdRegField.Password;
            String passwdReg2 = passwdRegField2.Password;
            String strQ;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    if ((passwdReg == passwdReg2) && (loginReg != "") &&
                    (passwdReg != "") && RestrictionFuncLogin(loginReg) && RestrictionFunc(passwdReg))
                    {
                        strQ = "INSERT INTO MainTable ";
                        strQ += "VALUES ('" + nameReg + "', '" + surnameReg +
                        "', '" + loginReg + "', '" + passwdReg + "','True', 'False'); ";
                        command = new SqlCommand(strQ, connection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        //MessageBox.Show($"Pass:{passwdReg}; login:{loginReg}; {RestrictionFuncLogin(loginReg)}");
                        MessageBox.Show("Обліковий запис не створено. Спробуйте ще раз!");

                    }
                }
                catch
                {
                    MessageBox.Show("Користувач з таким логіном вже існує у системі!");
                }
            }
            connection.Close();
        }

        private void CloseBtnSystem_Copy_Click(object sender, RoutedEventArgs e)
        {
            NewNameField.Text = ""; NewNameField.IsEnabled = false;
            NewSurnameField.Text = ""; NewSurnameField.IsEnabled = false;
            NewPasswdField.Password = ""; NewPasswdField.IsEnabled = false;
            NewPasswdField2.Password = ""; NewPasswdField2.IsEnabled = false;
            UpdateDataBtn.IsEnabled = false;
            passwdField.Password = "";
        }

        Boolean RestrictionFuncLogin(String Log)
        {
            foreach (char c in "\\,./<>?!@\"':;{}[]|#$%^&*()=+")
            {
                if (Log.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        Boolean RestrictionFunc(String Pass)
        {
            Byte Count1, Count2, Count3;
            Byte LenPass = (Byte)Pass.Length;
            Count1 = Count2 = Count3 = 0;
            for (Byte i = 0; i < LenPass; i++)
            {
                if ((Convert.ToInt32(Pass[i]) >= 65) &&

                (Convert.ToInt32(Pass[i]) <= 65 + 25))

                    Count1++;
                if ((Convert.ToInt32(Pass[i]) >= 97) &&

                (Convert.ToInt32(Pass[i]) <= 97 + 25))

                    Count2++;
                if ((Pass[i] == '+') || (Pass[i] == '-') || (Pass[i] == '*') ||

                (Pass[i] == '/'))

                    Count3++;
            }
            return (Count1 * Count2 * Count3 != 0);
        }

        private void UpdateDataBtn_Click(object sender, RoutedEventArgs e)
        {
            
            connection = new SqlConnection(connectionString);
            connection.Open();
            String newname = NewNameField.Text;
            String newsurname = NewSurnameField.Text;
            String newpasswd = NewPasswdField.Password;
            String newpasswd2 = NewPasswdField2.Password;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                String strQ;
                if (Convert.ToBoolean(dT.Rows[0][5]) == true)
                {
                    if (newpasswd == newpasswd2)
                    {
                        if ((newpasswd != String.Empty))
                        {
                            Boolean flag = RestrictionFunc(newpasswd);
                            if (flag == true)
                            {
                                strQ = "UPDATE MainTable SET Name='" + newname + "', ";
                                strQ += "Surname='" + newsurname + "', ";
                                strQ += "Password='" + newpasswd + "' ";
                                strQ += "WHERE Login='" + login + "';";

                                command = new SqlCommand(strQ, connection);
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show("У паролі немає літер верхнього та нижнього регістрів, а також арифметичних операцій! Спробуйте знову!");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Пароль не може бути пустим");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введені паролі не однакові ");
                    }
                }
                else
                {
                    if (newpasswd != String.Empty)
                    {
                        MessageBox.Show("Обмеження на пароль не встановлено");
                    }
                    else
                    {
                        strQ = "UPDATE MainTable SET Name='" + newname + "', ";
                        strQ += "Surname='" + newsurname + "', ";
                        strQ += "WHERE Login='" + login + "';";
                    }

                }

            }
                
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
    }
