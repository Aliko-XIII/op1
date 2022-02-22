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
    public partial class ProtectionModeWindow : Window
    {
        public ProtectionModeWindow()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader("parol.txt");
            ParolLabe.Content = sr.ReadToEnd();
            parol = sr.ReadToEnd();
            sr.Close();
            if (File.Exists("p1.txt"))
            {
                StreamReader sr1 = new StreamReader("p1.txt");
                string p1= sr1.ReadToEnd();
                sr1.Close();
                int n0;
                int n1;
                int index = p1.IndexOf('|');
                n0 = int.Parse(p1.Substring(0, index));
                n1 = int.Parse(p1.Substring(index + 1));
                P1Field.Content = Convert.ToDouble(n1) / n0;
            }
            if (File.Exists("p2.txt"))
            {
                StreamReader sr2 = new StreamReader("p2.txt");
                string p2 = sr2.ReadToEnd();
                sr2.Close();
                int n0;
                int n2;
                int index = p2.IndexOf('|');
                n0 = int.Parse(p2.Substring(0, index));
                n2 = int.Parse(p2.Substring(index + 1));
                P2Field.Content = Convert.ToDouble(n2) / n0;
            }
        }
        
        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void InputField_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }
        static double ColDisp(List<string> collection)
        {
            double sum = 0;
            double sum2 = 0;
            foreach (string el in collection)
            {
                sum += double.Parse(el);
                sum2 += Math.Pow(double.Parse(el), 2);
            }
            return sum2 / collection.Count - Math.Pow(sum / collection.Count, 2);

        }
        static bool Odnor(int degree, List<string> etalon, List<string> current)
        {
            int odnor_count = 0;
            int neodonor_count = 0;
            StreamReader sr = new StreamReader("fisher.txt");
            string table = sr.ReadToEnd();
            sr.Close();
            int t_index = table.IndexOf("|" + (degree).ToString() + "|") + 2 + (degree).ToString().Length;
            string ft = "";
            while (table[t_index] != '|' && t_index<table.Length)
            {
                ft+= table[t_index];
                t_index++;
            }
            double smax;
            double smin;
            double fp;
            for (int i=0;i<etalon.Count; i++)
            {
                for(int j = 0; j < current.Count; j++)
                {
                    smax = Math.Max(double.Parse(etalon[i]), double.Parse(current[j]));
                    smin = Math.Min(double.Parse(etalon[i]), double.Parse(current[j]));
                    fp = smax / smin;
                    if (fp > double.Parse(ft))
                    {
                        neodonor_count++;
                    }
                    else
                    {
                        odnor_count++;
                    }
                }
            }
            if (neodonor_count < odnor_count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        string parol;
        int number = 0;
        int i = 0;
        Stopwatch stopwatch = new Stopwatch();
        List<List<string>> timelapse = new List<List<string>>();
        List<string> attempt = new List<string>();
        List<string> matspod = new List<string>();
        List<string> disp = new List<string>();
        List<List<string>> etalon= new List<List<string>>();
        List<string> ematspod= new List<string>();
        List<string> edisp= new List<string>();
        List<string> dispxl = new List<string>();
        List<string> dispy = new List<string>();
        static List<string> MatSpod(List<string> lapses)
        {
            List<string> matspod = new List<string>();
            List<string> cur;
            double sum;
            for (int i = 0; i < lapses.Count; i++)
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
                matspod.Add((sum / cur.Count).ToString());
            }
            return matspod;
        }

        string data;
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
                    sr = new StreamReader(parol + "_data.txt");
                    data = sr.ReadToEnd();
                    sr.Close();
                    string m = "";
                    string d = "";
                    string t = "";
                    List<string> cur_lapse = new List<string>();
                    for (int j = 1; j < data.Length - 1; j++)
                    {
                        if (data[j - 1] == 'm')
                        {
                            m = "";
                            while (data[j] != 'd')
                            {
                                m += data[j];
                                j++;
                            }
                            ematspod.Add(m);
                        }
                        if (data[j - 1] == 'd')
                        {
                            d = "";
                            while (data[j] != 't')
                            {
                                d += data[j];
                                j++;
                            }
                            edisp.Add(d);
                        }
                        if (data[j - 1] == 't')
                        {
                            t = "";
                            cur_lapse = new List<string>();
                            while (data[j + 1] != '\n')
                            {
                                t += data[j];
                                j++;
                                if (data[j] == ' ' || data[j + 1] == '\n')
                                {
                                    if(t!=" ")
                                    {
                                        cur_lapse.Add(t);
                                    }
                                    
                                    t = "";
                                }
                            }
                            etalon.Add(cur_lapse);
                        }
                    }
                    for (int j = 0; j < timelapse.Count; j++)
                    {
                        double sum = 0;
                        double sum2 = 0;
                        foreach (string el in timelapse[j])
                        {
                            sum += double.Parse(el);
                            sum2 += Math.Pow(double.Parse(el), 2);
                        }
                        matspod.Add((sum/timelapse[j].Count).ToString());
                        disp.Add((sum2 / timelapse[j].Count - Math.Pow(sum / timelapse[j].Count, 2)).ToString());
                    }
                    if (Odnor(parol.Length - 1, edisp, disp))
                    {
                        DispField.Content = "однорідні";
                    }
                    else
                    {
                        DispField.Content = "неоднорідні";
                    }
                    InputField.IsEnabled = false;
                    InputField.Text = "STOP";
                    double sxl;
                    for (int j = 0; j < etalon.Count; j++)
                    {
                        sxl = 0;
                        for (int k = 0; k < etalon[j].Count; k++)
                        {
                            sxl += Math.Pow(double.Parse(etalon[j][k]) - double.Parse(ematspod[j]), 2);
                        }
                        dispxl.Add((sxl / (parol.Length - 1)).ToString());
                    }
                    double sy;
                    for (int j = 0; j < timelapse.Count; j++)
                    {
                        sy = 0;
                        for (int k = 0; k < timelapse[j].Count; k++)
                        {
                            sy += Math.Pow(double.Parse(timelapse[j][k]) - double.Parse(matspod[j]), 2);
                        }
                        dispy.Add((sy / (parol.Length - 1)).ToString());
                    }
                    StreamReader reader = new StreamReader("student.txt");
                    string st_table = reader.ReadToEnd();
                    reader.Close();
                    int st_index = st_table.IndexOf("|" + (parol.Length - 1).ToString() + "|") + 2 + (parol.Length - 1).ToString().Length;
                    string st_cur = "";
                    while (st_table[st_index] != '|' && st_index < st_table.Length)
                    {
                        st_cur += st_table[st_index];
                        st_index++;
                    }
                    double s;
                    double tp;
                    int wrong = 0;
                    for (int j = 0; j < dispxl.Count; j++)
                    {
                        for(int k = 0; k < dispy.Count; k++)
                        {
                            s = Math.Sqrt((double.Parse(dispxl[j]) + double.Parse(dispy[k])) * (parol.Length - 1) / (2 * parol.Length - 1));
                            tp = Math.Abs(double.Parse(ematspod[j]) - double.Parse(matspod[k]))/s/Math.Sqrt(2.0/parol.Length);
                            if (tp > double.Parse(st_cur))
                            {
                                wrong++;
                            }
                        }
                    }

                    StatisticsBlock.Content =(1-Convert.ToDouble(wrong)/(etalon.Count*timelapse.Count)).ToString();
                    if (double.Parse(StatisticsBlock.Content.ToString()) > 0.5)
                    {
                        StreamWriter sw = File.AppendText(parol + "_data.txt");
                        
                        for(int j = 0; j < timelapse.Count; j++)
                        {
                            sw.Write("m");
                            sw.Write(matspod[j]);
                            sw.Write("d");
                            sw.Write(disp[j]);
                            sw.Write("t");
                            for(int k = 0; k < timelapse[j].Count; k++)
                            {
                                sw.Write(timelapse[j][k]+" ");
                            }
                            sw.WriteLine();
                        }
                        sw.Close();
                    }
                    if (isAuthorBox.IsChecked == true)
                    {
                        if (!File.Exists("p1.txt"))
                        {
                            FileStream fs = File.Create("p1.txt");
                            fs.Close();
                            StreamWriter sw1 = new StreamWriter("p1.txt");
                            if (double.Parse(StatisticsBlock.Content.ToString()) > 0.5)
                            {
                                sw1.Write("1|0");
                            }
                            else
                            {
                                sw1.Write("1|1");
                            }
                            sw1.Close();
                        }
                        else
                        {
                            StreamReader sr1 = new StreamReader("p1.txt");
                            string p1 = sr1.ReadToEnd();
                            sr1.Close();
                            int n0;
                            int n1;
                            int index = p1.IndexOf('|');
                            n0 = int.Parse(p1.Substring(0, index));
                            n1 = int.Parse(p1.Substring(index+1));
                            P1Field.Content = Convert.ToDouble(n1) / n0;
                            StreamWriter sw1 = new StreamWriter("p1.txt");
                            if (double.Parse(StatisticsBlock.Content.ToString()) > 0.5)
                            {
                                sw1.Write($"{n0 + 1}|{n1}");
                            }
                            else
                            {
                                sw1.Write($"{n0 + 1}|{n1 + 1}");
                            }
                            sw1.Close();
                        }
                    }
                    else
                    {
                        if (!File.Exists("p2.txt"))
                        {
                            FileStream fs = File.Create("p2.txt");
                            fs.Close();
                            StreamWriter sw2 = new StreamWriter("p2.txt");
                            if (double.Parse(StatisticsBlock.Content.ToString()) > 0.5)
                            {
                                sw2.Write("1|1");
                            }
                            else
                            {
                                sw2.Write("1|0");
                            }
                            sw2.Close();
                        }
                        else
                        {
                            StreamReader sr2 = new StreamReader("p2.txt");
                            string p2 = sr2.ReadToEnd();
                            sr2.Close();
                            int n0;
                            int n2;
                            int index = p2.IndexOf('|');
                            n0 = int.Parse(p2.Substring(0, index));
                            n2 = int.Parse(p2.Substring(index+1));
                            P2Field.Content = Convert.ToDouble(n2) / n0;
                            StreamWriter sw2 = new StreamWriter("p2.txt");
                            if (double.Parse(StatisticsBlock.Content.ToString()) > 0.5)
                            {
                                sw2.Write($"{n0 + 1}|{n2 + 1}");
                            }
                            else
                            {
                                sw2.Write($"{n0 + 1}|{n2}");
                            }
                            sw2.Close();
                        }
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

        private void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

