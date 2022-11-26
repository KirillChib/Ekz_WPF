using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ekz_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board _board = new Board();

        public MainWindow()
        {
            InitializeComponent();

            InitGrid();
        }

        private void InitGrid()
        {
            for (int i = 0; i < _board._size; i++)
            {
                for (int j = 0; j < _board._size; j++)
                {
                    Button btn = new Button();

                    btn.Background = (i + j) % 2 != 0 ? Brushes.IndianRed : Brushes.Bisque;

                    btn.Click += Btn_Click;

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    gridBoard.Children.Add(btn);

                    _board.Buttons[i, j] = btn;
                }
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            Button btn = (Button)sender;

            var point = new Point();
            point = _board.ButtonIsClick(btn);

            if (_board.Fields[(int)point.X, (int)point.Y].FigurBase == null)
                return;
            else
            {
                _board.RefreshBackGround();

                btn.Background = Brushes.Aqua;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _board.StartNewGame();
        }
    }
}