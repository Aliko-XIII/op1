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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace pr1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            StudyModeBtn.IsEnabled = false;
            ProtectionModeBtn.IsEnabled = false;
            if (!File.Exists("parol.txt"))
            {
                FileStream parol = File.Create("parol.txt");
                parol.Close();
            }

        }
        public string GetParol()
        {
            return (string)ParolBox.Text;
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void StudyModeBtn_Click(object sender, RoutedEventArgs e)
        { 
            StudyModeWindow studyModeWindow = new StudyModeWindow();
            studyModeWindow.Show();
        }
        private void ProtectionModeBtn_Click(object sender,

        RoutedEventArgs e)

        {
            ProtectionModeWindow protectionModeWindow = new ProtectionModeWindow();

            protectionModeWindow.Show();
        }

        private void ParolBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParolBox.Text.Length > 3)
            {
                StudyModeBtn.IsEnabled = true;
                ProtectionModeBtn.IsEnabled = true;
            }
            else
            {
                StudyModeBtn.IsEnabled = false;
                ProtectionModeBtn.IsEnabled = false;
            }
            
            StreamWriter sw = new StreamWriter("parol.txt");
            sw.Write(ParolBox.Text);
            sw.Close();
        }
    }
}
