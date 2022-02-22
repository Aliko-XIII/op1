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
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace pr1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class StudyModeWindow : Window
    {
        
       
        
        public StudyModeWindow()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader("parol.txt");
            ParolLabe.Content=sr.ReadToEnd();
            sr.Close();
        }
        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
        }
        private void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
             

        }
        
        string parol;
        int number = 0;
        int i = 0;
        Stopwatch stopwatch = new Stopwatch();
        List<string> disp = new List<string>();
        List<List<string>> timelapse = new List<List<string>>();
        List<string> attempt = new List<string>();
        List<string> matspod=new List<string>();
        List <string> sv=new List<string>();
        List<string> student = new List<string>();
        static List<string> MatSpod(List<string> lapses)
        {
            List<string> matspod = new List<string>();
            List<string> cur;
            double sum;
            for(int i = 0; i < lapses.Count; i++)
            {
                sum = 0;
                cur = new List<string>();
                for (int j = 0; j < lapses.Count; j++)
                {
                    cur.Add(lapses[j]);
                }
                cur.RemoveAt(i);
                foreach (string el in cur)
                {
                    sum += double.Parse(el);
                }
                matspod.Add((sum/cur.Count).ToString());
            }
            return matspod;
        }
        static List <string> Disp(List<string> lapses, List<string> matspod)
        {
            List<string> disp = new List<string>();
            List<string> cur;
            double sum;
            for(int i = 0;i < lapses.Count; i++)
            {
                sum = 0;
                cur = new List<string>();
                foreach (string s in lapses)
                {
                    cur.Add(s);
                }
                cur.RemoveAt(i);
                foreach (string el in cur)
                {
                    sum += Math.Pow(double.Parse(el) - double.Parse(matspod[i]), 2);
                }
                disp.Add((sum/(cur.Count-1)).ToString());
            }
            return disp;
        }
        static List<string> SerVid(List<string> disp)
        {
            List<string> sv = new List<string>();
            foreach (string el in disp)
            {
                sv.Add(Math.Sqrt(double.Parse(el)).ToString());
            }
            return sv;
        }
        static List<string> Student(List<string> lapses, List<string> matspod, List<string> sv)
        {
            List<string> student = new List<string>();
            for(int i = 0; i < lapses.Count; i++)
            {
                student.Add((Math.Abs((double.Parse(lapses[i])-double.Parse(matspod[i]))/(double.Parse(sv[i])))).ToString());
            }
            return student;
        }
        private void InputField_KeyDown_1(object sender, KeyEventArgs e)
        {
            
        }

        private void InputField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SymbCount.Content = InputField.Text.Length;
            TimesCount.Content = number;
            int times = int.Parse(((ComboBoxItem)CountProtection.SelectedItem).Content.ToString());
            StreamReader sr = new StreamReader("parol.txt");
            parol = sr.ReadToEnd();
            sr.Close();

            if (times > number)
            {
                if (i == 0)
                {
                    if (InputField.Text.Length > 0 && InputField.Text != parol)
                    {
                        ErrLabel.Content = "ПОМИЛКА";
                        number = 0;
                        InputField.Clear();
                    }
                    InputField.Clear();
                    stopwatch.Start();
                    i++;
                }
                else if (i == parol.Length - 1)
                {
                    stopwatch.Stop();
                    attempt.Add(stopwatch.Elapsed.TotalSeconds.ToString());
                    timelapse.Add(attempt);
                    attempt = new List<string>();
                    number++;
                    i = 0;
                }
                else
                {
                    attempt.Add(stopwatch.Elapsed.TotalSeconds.ToString());
                    stopwatch.Restart();
                    i++;
                }
            }
            else
            {
                if (InputField.Text == parol)
                {
                    bool test = true;
                    for (int j = 0; j < timelapse.Count(); j++)
                    {
                        matspod = MatSpod(timelapse[j]);
                        disp = Disp(timelapse[j], matspod);
                        sv = SerVid(disp);
                        student = Student(timelapse[j], MatSpod(timelapse[j]), sv);
                        StreamReader reader = new StreamReader("student.txt");
                        string st_table = reader.ReadToEnd();
                        reader.Close();
                        int st_index = st_table.IndexOf("|" + (timelapse[j].Count - 2).ToString() + "|") + 2 + (timelapse[j].Count - 2).ToString().Length;
                        string st_cur = "";
                        while (st_table[st_index] != '|'&& st_index<st_table.Length)
                        {
                            st_cur += st_table[st_index];
                            st_index++;
                        }
                        foreach (string el in student)
                        {
                            if (double.Parse(el) > double.Parse(st_cur))
                            {
                                test = false;
                            }
                        }
                    }
                    if (test)
                    {
                        if (!File.Exists(parol+"_data.txt"))
                        {
                            FileStream data = File.Create(parol+"_data.txt");
                            data.Close();
                        }
                        StreamWriter sw = File.AppendText(parol+"_data.txt");
                        for (int j = 0; j < timelapse.Count(); j++)
                        {

                            double sum = 0;
                            double sum2 = 0;
                            foreach (string el in timelapse[j])
                            {
                                sum += double.Parse(el);
                                sum2 += Math.Pow(double.Parse(el), 2);
                            }
                            sw.Write("m");
                            sw.Write(sum / timelapse[j].Count);
                            sw.Write("d");
                            sw.Write(sum2 / timelapse[j].Count - Math.Pow(sum / timelapse[j].Count, 2));
                            sw.Write("t");
                            for (int k = 0; k < timelapse[j].Count(); k++)
                            {
                                sw.Write(timelapse[j][k] + " ");
                            }
                            sw.WriteLine();
                        }
                        sw.Close();
                        InputField.Text = "НАВЧАННЯ ЗАВЕРШЕНО";
                        InputField.IsEnabled = false;
                    }
                    else
                    {
                        stLabe.Content = "TestError";
                        InputField.Clear();
                        number = 0;
                        e.Handled = true;
                    }

                }
                else
                {
                    ErrLabel.Content = "ПОМИЛКА";
                    number = 0;
                    InputField.Clear();
                    e.Handled = true;
                }
            }
        }
    }
        
    }

