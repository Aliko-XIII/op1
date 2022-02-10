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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public static Button[,] cells;
        public Window2()
        {
            InitializeComponent();
            field.IsEnabled=true;
            cells = new Button[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    cells[i,j] = new Button();
                    cells[i,j].FontSize = 40;
                    cells[i, j].Click += Button_Click;
                    cells[i, j].Content = string.Empty;
                    field.Children.Add(cells[i, j]);                    
                    Grid.SetRow(cells[i, j], i);
                    Grid.SetColumn(cells[i, j], j);
                }
            }
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
            
        }
        static public bool turn = true;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool CheckWin(string player,Button btn)
                    {
                        bool ver = true;
                        bool hor = true;
                        for (int i = 0; i < 5; i++)
                        {
                            if (cells[Grid.GetRow(btn), i].Content != player)
                            {
                                hor = false;
                            }
                            if (cells[i, Grid.GetColumn(btn)].Content != player)
                            {
                                ver = false;
                            }
                        }
                        bool m_diag = false;
                        bool s_diag = false;

                        if (Grid.GetRow(btn) == Grid.GetColumn(btn) || 4 - Grid.GetRow(btn) == Grid.GetColumn(btn))
                        {
                            m_diag = true;
                            s_diag = true;
                            for (int i = 0; i < 5; i++)
                            {
                                if (cells[i, i].Content != player)
                                {
                                    m_diag = false;
                                }
                                if (cells[4 - i, i].Content != player)
                                {
                                    s_diag = false;
                                }
                            }
                        }
                        if(hor || ver || m_diag || s_diag)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
            if (Comp_enemy.IsChecked == false)
            {
                Button btn = sender as Button;
                if (btn.Content == string.Empty)
                {
                    string player;
                    if (turn)
                    {
                        player = "X";
                        btn.Background = Brushes.Blue;
                    }
                    else
                    {
                        player = "O";
                        btn.Background = Brushes.Red;
                    }
                    btn.Content = player;
                    if (CheckWin(player, btn))
                    {
                        if (player == "X")
                        {
                            ResLabel.Foreground = Brushes.Blue;
                        }
                        else
                        {
                            ResLabel.Foreground = Brushes.Red;
                        }
                        ResLabel.Content = player + " WON";

                        field.IsEnabled = false;
                    }
                }
                turn = !turn;
            }
            else
            {
                turn = true;
                Button btn = sender as Button;
                if (btn.Content == string.Empty)
                {
                    btn.Content = "X";
                    btn.Background = Brushes.Blue;
                }
                if(CheckWin("X", btn))
                {
                    ResLabel.Content = "X WON";
                    field.IsEnabled = false;
                }
                
                byte[,,] combo = new byte[5, 5, 4]; 
                for(int i = 0; i < 5; i++)
                {
                    for(int j=0;j<5; j++)
                    {
                        for(int k=0;k<3; k++){combo[i, j, k] = 0;}
                    }
                }
                
                void CheckCombo(byte[,,] combo, string a, string b)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (cells[i, j].Content == a)
                            {
                                
                                for (int k = 0; k < 5; k++)
                                {
                                    if (cells[i, k].Content == a)
                                    {
                                        combo[i, j, 0]++;
                                    }
                                    if (cells[k, j].Content == a)
                                    {
                                        combo[i, j, 1]++;
                                    }
                                    if (i == j)
                                    {
                                        if (cells[k, k].Content == a)
                                        {
                                            combo[i, j, 2]++;
                                        }

                                    }
                                    if (4 - i == j)
                                    {
                                        if (cells[k, 4 - k].Content == a)
                                        {
                                            combo[i, j, 3]++;
                                        }
                                    }
                                }
                                for(int k = 0; k < 5; k++)
                                {
                                    if (cells[i, k].Content == b)
                                    {
                                        combo[i, j, 0]=0;
                                    }
                                    if (cells[k, j].Content ==b)
                                    {
                                        combo[i, j, 1]=0;
                                    }
                                    if (i == j)
                                    {
                                        if (cells[k, k].Content == b)
                                        {
                                            combo[i, j, 2]=0;
                                        }

                                    }
                                    if (4 - i == j)
                                    {
                                        if (cells[k, 4 - k].Content == b)
                                        {
                                            combo[i, j, 3]=0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                CheckCombo(combo, "X", "O");
                bool stop = false;
                
                void OTurn()
                {
                    for (int t = 4; t > 0; t--)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                if (combo[i, j, 0] == t)
                                {
                                    for (int l = 0; l < 5; l++)
                                    {
                                        if (cells[i, l].Content == string.Empty)
                                        {
                                            cells[i, l].Content = "O";
                                            cells[i, l].Background = Brushes.Red;
                                            if (CheckWin("O", cells[i, l]))
                                            {
                                                ResLabel.Content = "O WON";
                                                field.IsEnabled = false;
                                            }
                                            stop = true;
                                            break;
                                        }
                                    }
                                }
                                else if (combo[i, j, 1] == t)
                                {
                                    for (int l = 0; l < 5; l++)
                                    {
                                        if (cells[l, j].Content == string.Empty)
                                        {
                                            cells[l, j].Content = "O";
                                            cells[l, j].Background = Brushes.Red;
                                            stop = true;
                                            if (CheckWin("O", cells[l, j]))
                                            {
                                                ResLabel.Content = "O WON";
                                                field.IsEnabled = false;
                                            }
                                            break;
                                        }
                                    }
                                }
                                else if (combo[i, j, 2] == t)
                                {
                                    for (int l = 0; l < 5; l++)
                                    {
                                        if (cells[l, l].Content == string.Empty)
                                        {
                                            cells[l, l].Content = "O";
                                            cells[l, l].Background = Brushes.Red;
                                            stop = true;
                                            if (CheckWin("O", cells[l, l]))
                                            {
                                                ResLabel.Content = "O WON";
                                                field.IsEnabled = false;
                                            }
                                            break;
                                        }
                                    }
                                }
                                else if (combo[i, j, 3] == t)
                                {
                                    for (int l = 0; l < 5; l++)
                                    {
                                        if (cells[l, 4 - l].Content == string.Empty)
                                        {
                                            cells[l, 4 - l].Content = "O";
                                            cells[l, 4 - l].Background = Brushes.Red;
                                            stop = true;
                                            if (CheckWin("O", cells[l, 4 - l]))
                                            {
                                                ResLabel.Content = "O WON";
                                                field.IsEnabled = false;
                                            }
                                            break;
                                        }
                                    }
                                }

                                if (stop) { break; }
                            }
                            if (stop) { break; }
                        }
                        if (stop) { break; }
                    }
                }
                OTurn();
                CheckCombo(combo, "O", "X");
                OTurn();
                if (!stop)
                {
                    ResLabel.Content = "DRAW";
                    field.IsEnabled = false;
                }

            }

            bool draw = true;
            for(int i = 0; i < 5; i++)
            {
                if (cells[i, 0].Content == string.Empty|| cells[0, i].Content == string.Empty|| cells[i, 4].Content == string.Empty|| cells[4, i].Content == string.Empty)
                {
                    draw = false;
                }
                
            }
            if (draw)
            {
                ResLabel.Content = "DRAW";
                field.IsEnabled = false;
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            field.IsEnabled = true;
            cells = new Button[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j] = new Button();
                    cells[i, j].FontSize = 40;
                    cells[i, j].Click += Button_Click;
                    cells[i, j].Content = string.Empty;
                    field.Children.Add(cells[i, j]);
                    Grid.SetRow(cells[i, j], i);
                    Grid.SetColumn(cells[i, j], j);
                }
            }
            ResLabel.Content = "Game in progress";
            ResLabel.Foreground = Brushes.Black;
        }
    }
}
