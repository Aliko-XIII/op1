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
            StreamReader sr = new StreamReader("student_data.txt");
            string init_text = sr.ReadToEnd();
            sr.Close();
            resbox.Text = init_text;
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw=new MainWindow();
            Hide();
            mw.Show();
        }


        private void Read_Click(object sender, RoutedEventArgs e)
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (IDbox.GetLineLength(0)==0)
            {
                IDbox.Text = "NO DATA";
            }
            else
            {
                StreamReader sr = new StreamReader("student_data.txt");
                string data=sr.ReadToEnd();
                sr.Close();
                string id="1";
                int del_index = 0;
                bool check = false;
                for (int i = 1; i < data.Length-1; i++)
                {
                    id = "";
                    if(data[i] == ':'&& data[i - 1] == 'D')
                    {
                        check=true;
                    }
                    while (check)
                    {
                        
                        i++;
                        id += data[i];
                        if (data[i+1] == ';')
                        {
                            check = false;
                        }
                    }
                    if (id == IDbox.Text)
                    {
                        del_index = i - id.Length-2;
                        break;
                    }
                }
                int t = del_index;
                while (data[t] != '\n')
                {
                    t++;
                }
                data=data.Remove(del_index,t-del_index+1);
                resbox.Text = data;
                StreamWriter sw = new StreamWriter("student_data.txt");
                sw.WriteLine(data);
                sw.Close();
            }
        }

        private void IDbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            IDbox.Clear();
            name1box.Clear();
            name2box.Clear();
            nameFbox.Clear();
            phonebox.Clear();
            datebox.Clear();
        }
    }
}
