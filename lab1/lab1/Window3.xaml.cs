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
            this.ResizeMode = ResizeMode.CanResize;
            Grid mygrid = new Grid();
            mygrid.Height = this.Height;
            mygrid.Width = this.Width;
            this.Content=mygrid;
            Grid field = new Grid();
            field.Width = 0.4 * this.Width; 
            field.Height = 0.8 * this.Height;
            mygrid.Children.Add(field);
            field.VerticalAlignment = VerticalAlignment.Top;
            field.HorizontalAlignment = HorizontalAlignment.Center;
            field.Margin = new Thickness(0,20,0,0);
            int n=7;
            int m=4;
            RowDefinition[] rows = new RowDefinition[n];
            ColumnDefinition[] cols = new ColumnDefinition[m];
            for (int i = 0; i < n; i++)
            {
                rows[i] = new RowDefinition();
                field.RowDefinitions.Add(rows[i]);
            }
            for(int i=0;i < m; i++)
            {
                cols[i] = new ColumnDefinition();
                field.ColumnDefinitions.Add(cols[i]);
            }
            Button ToMain = new Button();
            ToMain.Content = "RETURN TO MAIN";
            ToMain.Click += ToMain_Click;
            void ToMain_Click(object sender, RoutedEventArgs e)
            {
                MainWindow mw = new MainWindow();
                Hide();
                mw.Show();
            }
            Grid.SetColumn(ToMain,1);
            Grid.SetColumnSpan(ToMain, 2);
            Grid.SetRow(ToMain, 6);
            field.Children.Add(ToMain);

            TextBox CalBox = new TextBox();
            CalBox.FontSize = 25;
            Grid.SetRow(CalBox, 0);
            Grid.SetColumn(CalBox, 0);
            Grid.SetColumnSpan(CalBox,3);
            field.Children.Add(CalBox);

            Button Numb0=new Button();
            Numb0.Content = "0";
            Grid.SetRow(Numb0, 5);
            Grid.SetColumn(Numb0, 1);
            field.Children.Add(Numb0);
            void Numb0_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "0";
            }
            Numb0.Click += Numb0_Click;

            Button Numb1 = new Button();
            Numb1.Content = "1";
            Grid.SetRow(Numb1, 4);
            Grid.SetColumn(Numb1, 0);
            field.Children.Add(Numb1);
            void Numb1_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "1";
            }
            Numb1.Click += Numb1_Click;

            Button Numb2 = new Button();
            Numb2.Content = "2";
            Grid.SetRow(Numb2, 4);
            Grid.SetColumn(Numb2, 1);
            field.Children.Add(Numb2);
            void Numb2_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "2";
            }
            Numb2.Click += Numb2_Click;

            Button Numb3 = new Button();
            Numb3.Content = "3";
            Grid.SetRow(Numb3, 4);
            Grid.SetColumn(Numb3, 2);
            field.Children.Add(Numb3);
            void Numb3_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "3";
            }
            Numb3.Click += Numb3_Click;

            Button Numb4 = new Button();
            Numb4.Content = "4";
            Grid.SetRow(Numb4, 3);
            Grid.SetColumn(Numb4, 0);
            field.Children.Add(Numb4);
            void Numb4_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "4";
            }
            Numb4.Click += Numb4_Click;

            Button Numb5 = new Button();
            Numb5.Content = "5";
            Grid.SetRow(Numb5, 3);
            Grid.SetColumn(Numb5, 1);
            field.Children.Add(Numb5);
            void Numb5_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "5";
            }
            Numb5.Click += Numb5_Click;

            Button Numb6 = new Button();
            Numb6.Content = "6";
            Grid.SetRow(Numb6, 3);
            Grid.SetColumn(Numb6, 2);
            field.Children.Add(Numb6);
            void Numb6_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "6";
            }
            Numb6.Click += Numb6_Click;

            Button Numb7 = new Button();
            Numb7.Content = "7";
            Grid.SetRow(Numb7, 2);
            Grid.SetColumn(Numb7, 0);
            field.Children.Add(Numb7);
            void Numb7_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "7";
            }
            Numb7.Click += Numb7_Click;

            Button Numb8 = new Button();
            Numb8.Content = "8";
            Grid.SetRow(Numb8, 2);
            Grid.SetColumn(Numb8, 1);
            field.Children.Add(Numb8);
            void Numb8_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "8";
            }
            Numb8.Click += Numb8_Click;

            Button Numb9 = new Button();
            Numb9.Content = "9";
            Grid.SetRow(Numb9, 2);
            Grid.SetColumn(Numb9, 2);
            field.Children.Add(Numb9);
            void Numb9_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Text = CalBox.Text + "9";
            }
            Numb9.Click += Numb9_Click;

            Button DelButton = new Button();
            DelButton.Content = "<=";
            Grid.SetRow(DelButton, 0);
            Grid.SetColumn(DelButton, 3);
            field.Children.Add(DelButton);
            void DelButton_Click(object sender, RoutedEventArgs e)
            {
                if (CalBox.Text.Length > 0)
                {
                    CalBox.Text = CalBox.Text.Substring(0, CalBox.Text.Length - 1);
                }
            }
            DelButton.Click += DelButton_Click;

            Button DecButton = new Button();
            DecButton.Content = ",";
            Grid.SetRow(DecButton, 5);
            Grid.SetColumn(DecButton, 2);
            field.Children.Add(DecButton);
            void DecButton_Click(object sender, RoutedEventArgs e)
            {
                if (CalBox.Text.Length > 0)
                {
                    if (Char.IsDigit(CalBox.Text[CalBox.Text.Length - 1]))
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
            DecButton.Click += DecButton_Click;

            Button NegButton = new Button();
            NegButton.Content = "+/-";
            Grid.SetRow(NegButton, 5);
            Grid.SetColumn(NegButton, 0);
            field.Children.Add(NegButton);
            void NegButton_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    CalBox.Text = (-1 * double.Parse(CalBox.Text)).ToString();
                }
                catch
                {
                }

            }
            NegButton.Click += NegButton_Click;

            Button PlusButton = new Button();
            PlusButton.Content = "+";
            Grid.SetRow(PlusButton, 2);
            Grid.SetColumn(PlusButton, 3);
            field.Children.Add(PlusButton);         
            void PlusButton_Click(object sender, RoutedEventArgs e)
            {
                if (!CalBox.Text.Substring(1).Contains('-') && !CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*') && CalBox.Text.Length > 0)
                {
                    CalBox.Text = CalBox.Text + "+";
                }
                else if (CalBox.Text.Length == 0)
                {
                    CalBox.Text = "0+";
                }
            }
            PlusButton.Click += PlusButton_Click;

            Button MinusButton = new Button();
            MinusButton.Content = "-";
            Grid.SetRow(MinusButton, 3);
            Grid.SetColumn(MinusButton, 3);
            field.Children.Add(MinusButton);
            void MinusButton_Click(object sender, RoutedEventArgs e)
            {
                if (CalBox.Text.Length == 0)
                {
                    CalBox.Text = "-";
                }
                else
                {
                    if (!CalBox.Text.Substring(1).Contains('-') && ((!CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*')) || CalBox.Text[CalBox.Text.Length - 1] == '*' || CalBox.Text[CalBox.Text.Length - 1] == '/'))
                    {
                        CalBox.Text = CalBox.Text + "-";
                    }
                }

            }

            MinusButton.Click += MinusButton_Click;

            Button ClearButton = new Button();
            ClearButton.Content = "CLEAR";
            Grid.SetRow(ClearButton, 1);
            Grid.SetColumn(ClearButton, 2);
            field.Children.Add(ClearButton);
            void ClearButton_Click(object sender, RoutedEventArgs e)
            {
                CalBox.Clear();
            }
            ClearButton.Click += ClearButton_Click;

            Button DivideButton = new Button();
            DivideButton.Content = "/";
            Grid.SetRow(DivideButton, 4);
            Grid.SetColumn(DivideButton, 3);
            field.Children.Add(DivideButton);
            void DivideButton_Click(object sender, RoutedEventArgs e)
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
            DivideButton.Click += DivideButton_Click;

            Button MultiplyButton = new Button();
            MultiplyButton.Content = "*";
            Grid.SetRow(MultiplyButton, 5);
            Grid.SetColumn(MultiplyButton, 3);
            field.Children.Add(MultiplyButton);
            void MultiplyButton_Click(object sender, RoutedEventArgs e)
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
            MultiplyButton.Click += MultiplyButton_Click;

            Button EqualButton = new Button();
            EqualButton.Content = "=";
            Grid.SetRow(EqualButton, 1);
            Grid.SetColumn(EqualButton, 3);
            field.Children.Add(EqualButton);
            void EqualButton_Click(object sender, RoutedEventArgs e)
            {
                if (CalBox.Text.Substring(1).Contains('-') || CalBox.Text.Contains('+') || CalBox.Text.Contains('/') || CalBox.Text.Contains('*'))
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

                    b = CalBox.Text.Substring(t + 1);
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

            EqualButton.Click += EqualButton_Click;

            Button SqrtButton = new Button();
            SqrtButton.Content = "sqrt(x)";
            Grid.SetRow(SqrtButton, 1);
            Grid.SetColumn(SqrtButton, 0);
            field.Children.Add(SqrtButton);
            void SqrtButton_Click(object sender, RoutedEventArgs e)
            {
                if (!CalBox.Text.Contains('-') && !CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*') && CalBox.Text.Length > 0)
                {
                    CalBox.Text = Math.Sqrt(double.Parse(CalBox.Text)).ToString();
                }
            }
            SqrtButton.Click += SqrtButton_Click;

            Button RevButton = new Button();
            RevButton.Content = "1/x";
            Grid.SetRow(RevButton, 1);
            Grid.SetColumn(RevButton, 1);
            field.Children.Add(RevButton);
            void RevButton_Click(object sender, RoutedEventArgs e)
            {
                if (!CalBox.Text.Substring(1).Contains('-') && !CalBox.Text.Contains('+') && !CalBox.Text.Contains('/') && !CalBox.Text.Contains('*') && CalBox.Text.Length > 0)
                {
                    CalBox.Text = (1 / double.Parse(CalBox.Text)).ToString();
                }
            }
            RevButton.Click += RevButton_Click;
        }
    }
}
