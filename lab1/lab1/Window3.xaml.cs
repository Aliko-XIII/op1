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

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void CalBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Numb0_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "0";
        }

        private void Numb1_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "1";
        }

        private void Numb2_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "2";
        }

        private void Numb3_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "3";
        }

        private void Numb4_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "4";
        }

        private void Numb5_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "5";
        }

        private void Numb6_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "6";
        }

        private void Numb7_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "7";
        }

        private void Numb8_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "8";
        }

        private void Numb9_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Text = CalBox.Text + "9";
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalBox.Text.Length > 0)
            {
                CalBox.Text = CalBox.Text.Substring(0, CalBox.Text.Length - 1);
            }
        }

        private void DecButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalBox.Text.Length > 0)
            {
                if(Char.IsDigit(CalBox.Text[CalBox.Text.Length - 1]))
                {
                    CalBox.Text = CalBox.Text + ",";
                }
                else
                {
                    CalBox.Text = CalBox.Text + "0,";
                }
            }
            else
            {
                CalBox.Text = "0,";
            }
        }

        private void NegButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CalBox.Text =(-1*double.Parse(CalBox.Text)).ToString();   
            }
            catch
            {
            }
            
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CalBox.Text.Substring(1).Contains('-')&&!CalBox.Text.Contains('+')&& !CalBox.Text.Contains('/')&& !CalBox.Text.Contains('*') && CalBox.Text.Length>0)
            {
                CalBox.Text = CalBox.Text + "+";
            }
            else if(CalBox.Text.Length == 0)
            {
                CalBox.Text="0+";
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CalBox.Clear();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalBox.Text.Length == 0)
            {
                CalBox.Text = "-";
            }
            else
            {
                if (!CalBox.Text.Substring(1).Contains('-') && ((!CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*'))||CalBox.Text[CalBox.Text.Length-1]=='*'|| CalBox.Text[CalBox.Text.Length - 1] == '/'))
                {
                    CalBox.Text = CalBox.Text + "-";
                }
            } 
            
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalBox.Text.Length == 0)
            {
                CalBox.Text = "0/";
            }
            else if (!CalBox.Text.Substring(1).Contains('-') && !CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*') && CalBox.Text.Length > 0)
            {
                CalBox.Text = CalBox.Text + "/";
            }
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalBox.Text.Length == 0)
            {
                CalBox.Text = "0*";
            }
            else if (!CalBox.Text.Substring(1).Contains('-') && !CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*') && CalBox.Text.Length > 0)
            {
                CalBox.Text = CalBox.Text + "*";
            }
        }

        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            if(!CalBox.Text.Contains('-') && !CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*') && CalBox.Text.Length > 0)
            {
                CalBox.Text = Math.Sqrt(double.Parse(CalBox.Text)).ToString();
            }
        }

        private void RevButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CalBox.Text.Substring(1).Contains('-') && !CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*') && CalBox.Text.Length > 0)
            {
                CalBox.Text = (1 / double.Parse(CalBox.Text)).ToString();
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if(CalBox.Text.Substring(1).Contains('-') || CalBox.Text.Contains('+') || CalBox.Text.Contains('/') || CalBox.Text.Contains('*'))
            {
                string a = CalBox.Text[0].ToString();
                string b;
                char operation;

                int t = 1;
                while (Char.IsDigit(CalBox.Text[t]))
                {
                    a += CalBox.Text[t];
                    t++;
                }

                operation = CalBox.Text[t];

                b = CalBox.Text.Substring(t+1);
                switch (operation)
                {
                    case '+':
                        CalBox.Text = (double.Parse(a) + double.Parse(b)).ToString();
                        break;
                    case '-':
                        CalBox.Text = (double.Parse(a) - double.Parse(b)).ToString();
                        break;
                    case '*':
                        CalBox.Text = (double.Parse(a) * double.Parse(b)).ToString();
                        break;
                    case '/':
                        CalBox.Text = (double.Parse(a) / double.Parse(b)).ToString();
                        break;
                }

            }
        }
    }
}
