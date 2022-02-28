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
using System.IO;

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            if (!File.Exists("student_data.txt"))
            {
                FileStream data = File.Create("student_data.txt");
                data.Close();
            }

            StreamReader sr= new StreamReader("student_data.txt");
            string init_text = sr.ReadToEnd();
            sr.Close();
            
            this.Title = "Window1";
            this.ResizeMode = ResizeMode.NoResize;
            this.Background = Brushes.PaleGoldenrod;
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.Width = this.Width*0.9;
            grid.Height = this.Height*0.8;
            this.Content = grid;
            

            RowDefinition[] rows = new RowDefinition[4];
            ColumnDefinition[] cols = new ColumnDefinition[4];
            for (int i = 0; i < 4; i++)
            {
                rows[i] = new RowDefinition();
                cols[i] = new ColumnDefinition();
                grid.RowDefinitions.Add(rows[i]);
                grid.ColumnDefinitions.Add(cols[i]);
            }

            Label L1 = new Label();
            L1.Content = "ID залікової книжки";
            grid.Children.Add(L1);
            L1.VerticalAlignment = VerticalAlignment.Top;
            L1.FontSize = 13;

            Label L2 = new Label();
            L2.Content = "Прізвище";
            grid.Children.Add(L2);
            Grid.SetColumn(L2, 1);
            L2.VerticalAlignment = VerticalAlignment.Top;
            L2.FontSize = 13;

            Label L3 = new Label();
            L3.Content = "Ім'я";
            grid.Children.Add(L3);
            Grid.SetColumn(L3, 2);
            L3.VerticalAlignment = VerticalAlignment.Top;
            L3.FontSize = 13;

            Label L4 = new Label();
            L4.Content = "По-батькові";
            grid.Children.Add(L4);
            Grid.SetColumn(L4, 3);
            L4.VerticalAlignment = VerticalAlignment.Top;
            L4.FontSize = 13;

            Label L21 = new Label();
            L21.Content = "Моб. номер";
            grid.Children.Add(L21);
            Grid.SetRow(L21,1);
            L21.VerticalAlignment = VerticalAlignment.Top;
            L21.FontSize = 13;

            Label L22 = new Label();
            L22.Content = "Дата народження";
            grid.Children.Add(L22);
            Grid.SetRow(L22, 1);
            Grid.SetColumn(L22,1);
            L22.VerticalAlignment = VerticalAlignment.Top;
            L22.FontSize = 13;

            TextBox IDbox = new TextBox();
            grid.Children.Add(IDbox);
            Grid.SetRow(IDbox,0);
            Grid.SetColumn(IDbox,0);
            IDbox.HorizontalAlignment=HorizontalAlignment.Left;
            IDbox.Height = 20;
            IDbox.Width = 150;

            TextBox name2box = new TextBox();
            grid.Children.Add(name2box);
            Grid.SetRow(name2box, 0);
            Grid.SetColumn(name2box, 1);
            name2box.HorizontalAlignment = HorizontalAlignment.Left;
            name2box.Height = 20;
            name2box.Width = 150;

            TextBox name1box = new TextBox();
            grid.Children.Add(name1box);
            Grid.SetRow(name1box, 0);
            Grid.SetColumn(name1box, 2);
            name1box.HorizontalAlignment = HorizontalAlignment.Left;
            name1box.Height = 20;
            name1box.Width = 150;

            TextBox nameFbox = new TextBox();
            grid.Children.Add(nameFbox);
            Grid.SetRow(nameFbox, 0);
            Grid.SetColumn(nameFbox, 3);
            nameFbox.HorizontalAlignment = HorizontalAlignment.Left;
            nameFbox.Height = 20;
            nameFbox.Width = 150;

            TextBox phonebox = new TextBox();
            grid.Children.Add(phonebox);
            Grid.SetRow(phonebox, 1);
            Grid.SetColumn(phonebox, 0);
            phonebox.HorizontalAlignment = HorizontalAlignment.Left;
            phonebox.Height = 20;
            phonebox.Width = 150;

            TextBox datebox = new TextBox();
            grid.Children.Add(datebox);
            Grid.SetRow(datebox, 1);
            Grid.SetColumn(datebox, 1);
            datebox.HorizontalAlignment = HorizontalAlignment.Left;
            datebox.Height = 20;
            datebox.Width = 150;

            TextBox resbox = new TextBox();
            resbox.TextWrapping = TextWrapping.Wrap;
            grid.Children.Add(resbox);
            Grid.SetRow(resbox, 2);
            Grid.SetColumn(resbox, 0);
            Grid.SetRowSpan(resbox, 2);
            Grid.SetColumnSpan(resbox,2);
            resbox.Text = init_text;

            Button Read = new Button();
            Read.Content = "ЗЧИТАТИ";
            grid.Children.Add(Read);
            Grid.SetRow(Read, 1);
            Grid.SetColumn(Read, 2);
            Read.Margin = new Thickness(20,20,20,20);

            Read.Click += Read_Click;
            void Read_Click(object sender, RoutedEventArgs e)
            {
                if (IDbox.GetLineLength(0) != 0)
                {
                    StreamWriter sw = File.AppendText("student_data.txt");
                    string student = $"ID:{IDbox.Text};First name:{name1box.Text}; Second name:{name2box.Text}; Patronymic:{nameFbox.Text}; Phone number:{phonebox.Text}; Date:{datebox.Text};";
                    sw.WriteLine(student);
                    sw.Close();
                    StreamReader sr = new StreamReader("student_data.txt");
                    string init_text1 = sr.ReadToEnd();
                    sr.Close();
                    resbox.Text = init_text1;
                }

            }

            Button Delete = new Button();
            Delete.Content = "ВИДАЛИТИ";
            grid.Children.Add(Delete);
            Grid.SetRow(Delete, 1);
            Grid.SetColumn(Delete, 3);
            Delete.Margin = new Thickness(20, 20, 20, 20);
            Delete.Click += Delete_Click;
            void Delete_Click(object sender, RoutedEventArgs e)
            {
                if (IDbox.GetLineLength(0) == 0)
                {
                    IDbox.Text = "NO DATA";
                }
                else
                {
                    StreamReader sr = new StreamReader("student_data.txt");
                    string data = sr.ReadToEnd();
                    sr.Close();
                    string id = "1";
                    int del_index = 0;
                    bool check = false;
                    for (int i = 1; i < data.Length - 1; i++)
                    {
                        id = "";
                        if (data[i] == ':' && data[i - 1] == 'D')
                        {
                            check = true;
                        }
                        while (check)
                        {

                            i++;
                            id += data[i];
                            if (data[i + 1] == ';')
                            {
                                check = false;
                            }
                        }
                        if (id == IDbox.Text)
                        {
                            del_index = i - id.Length - 2;
                            break;
                        }
                    }
                    int t = del_index;
                    while (data[t] != '\n')
                    {
                        t++;
                    }
                    data = data.Remove(del_index, t - del_index + 1);
                    resbox.Text = data;
                    StreamWriter sw = new StreamWriter("student_data.txt");
                    sw.WriteLine(data);
                    sw.Close();
                }
            }

            Button Clear = new Button();
            Clear.Content = "Clear all";
            grid.Children.Add(Clear);
            Grid.SetRow(Clear, 2);
            Grid.SetColumn(Clear, 2);
            Clear.Margin = new Thickness(20, 20, 20, 20);
            Clear.Click += Clear_Click;
            void Clear_Click(object sender, RoutedEventArgs e)
            {
                IDbox.Clear();
                name1box.Clear();
                name2box.Clear();
                nameFbox.Clear();
                phonebox.Clear();
                datebox.Clear();
            }

            Button ToMain = new Button();
            ToMain.Content = "RETURN TO MAIN";
            Grid.SetRow(ToMain, 3);
            Grid.SetColumn(ToMain, 3);
            ToMain.Margin = new Thickness(20, 20, 20, 20);
            ToMain.Click += ToMain_Click;
            void ToMain_Click(object sender, RoutedEventArgs e)
            {
                MainWindow mw = new MainWindow();
                Hide();
                mw.Show();
            }

            grid.Children.Add(ToMain);
        }
    }
}
