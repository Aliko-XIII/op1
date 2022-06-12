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
        DataTable BookHallTable;

        public BookWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            InfoBlock.Visibility = Visibility.Hidden;
            InitHallCB();

        }
        private void ChangeData(string SQLQuery)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            IDBox.Clear(); 
            NameBox.Clear();
            AuthorBox.Clear();
            HallCB.SelectedIndex = -1;
            YearBox.Clear();
            PublisherBox.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorBox.Text != String.Empty &&
                IDBox.Text != String.Empty &&
                HallCB.SelectedIndex > -1 &&
                NameBox.Text != String.Empty &&
                PublisherBox.Text != String.Empty)
            {
                string sqlQ;
                if (!InTable(IDBox.Text))
                {
                    sqlQ =
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
                $" set @hallname = '{(string)HallCB.SelectedItem}';" +
                "set @hall = (select IDHall from Halls where NameHall = @hallname);" +
                " declare @bookname varchar(30);" +
                $" set @bookname='{NameBox.Text}';" +
                " declare @year datetime;" +
                $" set @year='01.01.{YearBox.Text}';" +
                " declare @publisher varchar(30);" +
                $"set @publisher='{PublisherBox.Text}';" +
                "insert into dbo.Books (IDBook, NameBook, Publisher, YearBook) values (@idbook,@bookname, @publisher, @year);" +
                "if not exists (select * from BookHall where (IDBookH=@idbook and IDHallH=@hall))" +
                " insert into dbo.BookHall(IDBookH, IDHallH, NumBook) values(@idbook, @hall, 1);\n" +
                "else " +
                $"update BookHall set NumBook= NumBook+{(int)BookNumSlider.Value} where (IDBookH=@idbook and IDHallH=@hall);" +
                "if not exists (select * from BookAuthor where (IDAuthorA=@idauthor and IDBookA=@idbook))" +
                " insert into dbo.BookAuthor(IDBookA, IDAuthorA) values(@idbook, @idauthor);";
                }
                else
                {
                    sqlQ =
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
                $" set @hallname = '{(string)HallCB.SelectedItem}';" +
                "set @hall = (select IDHall from Halls where NameHall = @hallname);" +
                " declare @bookname varchar(30);" +
                $" set @bookname='{NameBox.Text}';" +
                " declare @year datetime;" +
                $" set @year='01.01.{YearBox.Text}';" +
                " declare @publisher varchar(30);" +
                $"set @publisher='{PublisherBox.Text}';" +
                "if not exists (select * from BookHall where (IDBookH=@idbook and IDHallH=@hall))" +
                $" insert into dbo.BookHall(IDBookH, IDHallH, NumBook) values(@idbook, @hall, {(int)BookNumSlider.Value});\n" +
                "else " +
                $"update BookHall set NumBook= NumBook+{(int)BookNumSlider.Value} where (IDBookH=@idbook and IDHallH=@hall);";
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
                MessageBox.Show("Ви не заповнили усі поля!");
            }
            
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != String.Empty)
            {
                string sqlQ=null;
                MessageBoxResult res = MessageBox.Show("Ви хочете видалити усі книги з цим шифром з бібліотеки-ТАК\n Лише визначену кількість-НІ", "?", MessageBoxButton.YesNoCancel);
                sqlQ = $"declare @idhall int; " +
                        $"set @idhall=(select IDHall from Halls where NameHall='{(string)HallCB.SelectedItem}'); ";

                if (res == MessageBoxResult.Yes) 
                {
                        sqlQ+=$"Delete from BookHall ";
                }
                else if(res==MessageBoxResult.No)
                {
                    sqlQ +=$"Update BookHall set NumBook=NumBook-{BookNumSlider.Value} ";
                }
                sqlQ+=$" where IDBookH = { IDBox.Text } and IDHallH = @idhall";
                try
                {
                    ChangeData(sqlQ);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            else
            {
                MessageBox.Show("Введіть шифр книги");
            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != String.Empty)
            {
                string sqlQ = String.Empty;
                if (NameBox.Text != String.Empty)
                {
                    sqlQ += $"Update Books set NameBook='{NameBox.Text}' where IDBook={IDBox.Text};";
                }
                if (PublisherBox.Text != String.Empty)
                {
                    sqlQ += $"Update Books set Publisher='{PublisherBox.Text}' where IDBook={IDBox.Text};";
                }
                if (YearBox.Text!=String.Empty)
                {
                    sqlQ += $"Update Books set Year='{YearBox.Text}-01-01' where IDBook={IDBox.Text};";
                }
                if (AuthorBox.Text != String.Empty)
                {
                    sqlQ += $"if exists(select * from Authors where NameAuthor = '{AuthorBox.Text}')" +
                $"set @idauthor = (select IDAuthor from Authors where NameAuthor = '{AuthorBox.Text}')" +
                " else" +
                $" insert into dbo.Authors(NameAuthor) values('{AuthorBox.Text}')" +
                $"if not exists (select * from BookAuthor where (IDAuthorA=@idauthor and IDBookA={IDBox.Text}))" +
                $" insert into dbo.BookAuthor(IDBookA, IDAuthorA) values({IDBox.Text}, @idauthor);";
                }
                if (BookNumSlider.Value > 0)
                {
                    sqlQ = $"declare @idhall int; " +
                        $"set @idhall=(select IDHall from Halls where NameHall='{(string)HallCB.SelectedItem}'); " +
                        $"Update BookHall set NumBook=NumBook-{BookNumSlider.Value} where IDBookH = { IDBox.Text } and IDHallH = @idhall;";

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
                MessageBox.Show("Введіть шифр книги");
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Visible;
        }

        private void InfoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoBlock.Visibility = Visibility.Hidden;
        }

        private bool InTable(string id)
        {
            string SQLQuery = "Select IDBook from Books";
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

        private void BookNumSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BookNumLabel.Content=BookNumSlider.Value;
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            BookInfWin bookInf = new BookInfWin();
            bookInf.ExitButtonM.Visibility = Visibility.Hidden;
            bookInf.ExitButtonR.Visibility = Visibility.Visible;
            bookInf.Show();
        }
    }
}
