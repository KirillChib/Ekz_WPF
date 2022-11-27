using System.Linq;
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
            {
                return;
            }

            _board.RefreshBackGround();

            Button btn = (Button)sender;

            var point = new Point();
            point = _board.ButtonIsClick(btn);

            if (_board.IsChek == false)
            {
                if (_board.Fields[(int)point.X, (int)point.Y].FigurBase == null)
                {
                    _board.IsChek = false;

                    return;
                }
                else
                {
                    btn.Background = Brushes.Aqua;
                    _board.IsChek = true;
                    _board.CurrentPoint = point;

                    _board.listRules = _board.Fields[(int)point.X, (int)point.Y].FigurBase.Rules(_board.Fields, _board.Buttons, point)
                       .Where(c => (c.X < _board._size && c.Y < _board._size) && (c.X >= 0 && c.Y >= 0))
                       .Select(c => c).ToList();

                    foreach (var item in _board.listRules)
                    {
                        _board.Buttons[(int)item.X, (int)item.Y].BorderThickness = new Thickness(5);
                        _board.Buttons[(int)item.X, (int)item.Y].BorderBrush = Brushes.Yellow;
                    }
                }
            }
            else if (_board.IsChek == true)
            {
                foreach (var item in _board.listRules)
                {
                    if (item == point)
                    {
                        _board.Fields[(int)item.X, (int)item.Y].FigurBase = _board.Fields[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].FigurBase;
                        _board.Fields[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].FigurBase = null;

                        _board.Buttons[(int)item.X, (int)item.Y].Content = _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content;
                        _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content = null;

                        _board.IsChek = false;
                    }
                }
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _board.StartNewGame();
        }
    }
}