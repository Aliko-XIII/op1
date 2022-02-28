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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
            this.Background = Brushes.Purple;
            Grid mygrid = new Grid();
            mygrid.Height = this.Height;
            mygrid.Width = this.Width;
            this.Content = mygrid;
            TextBlock text=new TextBlock();
            text.Text = "Лабораторна робота №1\nПідготував Тектов Аліко\nстудент 1 курсу ПЗКС група КП - 13\nлютий 2022 року";
            text.Height = this.Height/3;
            text.Width = this.Width / 2;
            text.FontSize = 20;
            text.Foreground = Brushes.White;
            text.FontWeight = FontWeights.Bold;
            mygrid.Children.Add(text);
            Button ToMain = new Button();
            ToMain.Content = "RETURN TO MAIN";
            ToMain.HorizontalAlignment = HorizontalAlignment.Center;
            ToMain.VerticalAlignment = VerticalAlignment.Bottom;
            ToMain.Margin = new Thickness(0, 0, 0, 50);
            ToMain.Click += ToMain_Click;
            void ToMain_Click(object sender, RoutedEventArgs e)
            {
                MainWindow mw = new MainWindow();
                Hide();
                mw.Show();
            }
            ToMain.Width = 184;
            ToMain.Height = 53;
            mygrid.Children.Add(ToMain);
        }

    }
}
