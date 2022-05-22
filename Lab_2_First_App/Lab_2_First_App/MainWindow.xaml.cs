using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;

namespace Lab_2_First_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DispatcherTimer dT;
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List <Ellipse>();
        static PointCollection pC = new PointCollection();
        static List<int[]> Population=new List<int[]>();
        static List<int> GreedWay = new List<int>() { 0 };
        static List<int> arr = new List<int> {0};
        public MainWindow()
        {
            dT = new DispatcherTimer();
            InitializeComponent();
            InitPoints();
            InitPolygon();
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);  
            GenButton.IsChecked = true;
            gen = true;
            InitFirstGen();
            SelectPopulation();
        }
        //INTERFACE
        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75 * MainWin.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * MainWin.Height - 3 * Radius));
                pC.Add(p);
            }
            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();

                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                el.Fill = Brushes.LightBlue;
                EllipseArray.Add(el);
            }

        }
        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
        }
        private void PlotPoints()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }
        private void VelCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
        }
        private void PlotWay(int[] BestWayIndex)
        {
            PointCollection Points = new PointCollection();

            for (int i = 0; i < BestWayIndex.Length; i++)
                Points.Add(pC[BestWayIndex[i]]);
            
            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }
        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (GenButton.IsChecked==true)
            {
                if (dT.IsEnabled)
                {
                    dT.Stop();
                    NumElemCB.IsEnabled = true;
                    GenButton.IsEnabled = !GenButton.IsEnabled;
                    GreedButton.IsEnabled = !GreedButton.IsEnabled;
                }
                else
                {
                    NumElemCB.IsEnabled = false;
                    dT.Start();
                    GenButton.IsEnabled = !GenButton.IsEnabled;
                    GreedButton.IsEnabled = !GreedButton.IsEnabled;
                }
            }
            else
            {
                MyCanvas.Children.Clear();
                PlotPoints();
                if (dT.IsEnabled)
                {
                    dT.Stop();
                    NumElemCB.IsEnabled = true;
                    GenButton.IsEnabled = !GenButton.IsEnabled;
                    GreedButton.IsEnabled = !GreedButton.IsEnabled;
                }
                else
                {
                    NumElemCB.IsEnabled = false;
                    dT.Start();
                    GenButton.IsEnabled = !GenButton.IsEnabled;
                    GreedButton.IsEnabled = !GreedButton.IsEnabled;
                }
            }

        }

        //Fitness function(min len->max fitness)
        private double Fitness(int[] way)
        {
            double len = 0;
            for(int i = 0; i < way.Length-1; i++)
            {
                len += Math.Sqrt(Math.Pow((pC[way[i+1]].X- pC[way[i]].X),2)+Math.Pow((pC[way[i + 1]].Y - pC[way[i]].Y), 2));
            }
            len += Math.Sqrt(Math.Pow((pC[way[way.Length - 1]].X - pC[0].X), 2) + Math.Pow((pC[way[way.Length - 1]].Y - pC[way[0]].Y), 2));
            return 1.0/len;
        }

        //Sort by fitness and remove worst
        private void SelectPopulation()
        {
            Population = (Population.OrderByDescending(individual=>Fitness(individual))).ToList();
            Population.RemoveRange(PointCount-1, Population.Count-PointCount);
        }

        //Randomly generates 2*PointCount ways(chromosomes)
        private void InitFirstGen()
        {
            Population = new List<int[]>();
            Random rnd = new Random();
            int[] def_chromosome = new int[PointCount];
            for (int i = 0; i < PointCount; i++)
                def_chromosome[i] = i;
            
            for (int s = 0; s < 2*PointCount; s++)
            {
                int[] cur_chromosome=new int[PointCount];
                def_chromosome.CopyTo(cur_chromosome,0);
                for(int j = 0;j < PointCount; j++)
                {
                    int i1;
                    int i2;
                    int tmp;
                    i1 = rnd.Next(PointCount);
                    i2 = rnd.Next(PointCount);
                    tmp = cur_chromosome[i1];
                    cur_chromosome[i1] = cur_chromosome[i2];
                    cur_chromosome[i2] = tmp;
                }
                Population.Add(cur_chromosome);

            }
            


        }
        
        //Occurs before the start of the algorithm 
        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
            
            if (gen)
            {
                InitFirstGen();
                SelectPopulation();

            }
            else if (greed)
            {
                GreedWay.Clear();
                GreedWay.Add(0);
                
            }
            
        }

        //Get Length of the next way
        private double GetWayLen(int p1, int p2)
        {
            double len = Math.Sqrt(Math.Pow((pC[p2].X - pC[p1].X), 2) + Math.Pow((pC[p2].Y - pC[p1].Y), 2));
            return len;
        }

        private void OneStep(object sender, EventArgs e)
        {            
            MyCanvas.Children.Clear();
            PlotPoints();
            if (gen)
            {
                if (Population[0].Count() != PointCount)
                {
                    InitFirstGen();
                }
                SelectPopulation();
                PlotWay(Population[0]);
                Hybridization();
            }
            else if(greed)
            {
                if(GreedWay.Count < PointCount)
                {
                    int next = -1;
                    for (int i = 0; i < PointCount; i++)
                    {
                        if (!GreedWay.Contains(i))
                        {
                            if (next >= 0)
                            {
                                if (GetWayLen(GreedWay[GreedWay.Count - 1], i) < GetWayLen(GreedWay[GreedWay.Count - 1], next))
                                {
                                    next = i;
                                }
                            }
                            else
                            {
                                next = i;
                            }
                        }
                    }
                    GreedWay.Add(next);
                }
                PlotWay(GreedWay.ToArray());
            }
            

        }
        
        private void Hybridization()
        {
            Random rnd = new Random();
            List<int> individuals = new List<int>();
            for (int i = 0; i < Population.Count; i++)
            {
                individuals.Add(i);
            }

            while (individuals.Count >= 2)
            {
                int[] first_parent=new int[PointCount];
                int[] second_parent = new int[PointCount];
                int id1= individuals[rnd.Next(0, individuals.Count)];
                individuals.Remove(id1);
                int id2=individuals[rnd.Next(0, individuals.Count)];
                Population[id1].CopyTo(first_parent, 0);
                Population[id2].CopyTo(second_parent, 0);
                int crossover = rnd.Next(1, PointCount-1);
                List<int> first_child = new List<int>();
                List<int> second_child = new List<int>();
                
                for(int i=0; i <= crossover; i++)
                {
                    first_child.Add(first_parent[i]);
                    second_child.Add(second_parent[i]);
                }
                for(int i = crossover; i < PointCount; i++)
                {
                    if (!first_child.Contains(second_parent[i]))
                    {
                        first_child.Add(second_parent[i]);
                    }
                    if (!second_child.Contains(first_parent[i]))
                    {
                        second_child.Add(first_parent[i]);
                    }
                }
                if (first_child.Count < PointCount)
                {
                    for(int i = 0; i < crossover; i++)
                    {
                        if (!first_child.Contains(second_parent[i]))
                        {
                            first_child.Add(second_parent[i]);
                        }
                    }
                }
                if(second_child.Count < PointCount)
                {
                    for (int i = 0; i < crossover; i++)
                    {
                        if (!second_child.Contains(first_parent[i]))
                        {
                            second_child.Add(first_parent[i]);
                        }
                    }
                }
                double first_mut = rnd.NextDouble();
                double second_mut = rnd.NextDouble();
                if (first_mut < 0.7)
                {
                    int i1 = rnd.Next(0,PointCount);
                    int i2 = rnd.Next(0,PointCount);
                    if (i1 < i2)
                    {
                        while (i1 < i2)
                        {
                            int tmp = first_child[i1];
                            first_child[i1] = first_child[i2];
                            first_child[i2] = tmp;
                            i1++;
                            i2--;
                        }
                    }
                    else if (i2 > i1)
                    {
                        while (i1 > i2)
                        {
                            int tmp = first_child[i1];
                            first_child[i1] = first_child[i2];
                            first_child[i2] = tmp;
                            i1--;
                            i2++;
                        }
                    }
                }
                if (second_mut < 0.7)
                {
                    int i1 = rnd.Next(0,PointCount);
                    int i2 = rnd.Next(0,PointCount);
                    if (i1 < i2)
                    {
                        while (i1 < i2)
                        {
                            int tmp = second_child[i1];
                            second_child[i1] = second_child[i2];
                            second_child[i2] = tmp;
                            i1++;
                            i2--;
                        }
                    }
                    else if (i1>i2)
                    {
                        while (i1 > i2)
                        {
                            int tmp = second_child[i1];
                            second_child[i1] = second_child[i2];
                            second_child[i2] = tmp;
                            i1--;
                            i2++;
                        }
                    }
                }

                bool check1 =true;
                bool check2=true;
                foreach(int[] chromosome in Population)
                {
                    if (Enumerable.SequenceEqual(first_child.ToArray(),chromosome)) { check1=false; }
                    if(Enumerable.SequenceEqual(second_child.ToArray(), chromosome)) { check2=false; }
                    if (!check1 && !check2)
                    {
                        break;
                    }
                }
                

                if (check1)
                {
                    Population.Add(first_child.ToArray());
                }
                if (check2)
                {
                    Population.Add(second_child.ToArray());
                }
            }
        }
        static bool greed;
        static bool gen;
        private void GreedButton_Checked(object sender, RoutedEventArgs e)
        {
            greed = true;
            gen = false;
        }
        private void GenButton_Checked(object sender, RoutedEventArgs e)
        {
            greed = false;
            gen = true;
        }
    }
}