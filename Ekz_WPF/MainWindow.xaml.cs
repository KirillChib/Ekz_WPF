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
            InitFieldsForDeletedFigure();
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

        private void InitFieldsForDeletedFigure()
        {
            for (int i = 0; i < _board._size * 2; i++)
            {
                var btn = new Button();
                btn.Width = 50;
                btn.Height = 50;
                btn.IsEnabled = false;
                btn.Content = null;

                _board.DeletedWhiteFigures.Add(btn);

                if (i >= _board._size)
                {
                    Grid.SetColumn(btn, 0);
                    WhiteFigures.Children.Add(btn);
                }
                else
                {
                    Grid.SetColumn(btn, 1);
                    WhiteFigures1.Children.Add(btn);
                }
            }

            for (int i = 0; i < _board._size * 2; i++)
            {
                var btn = new Button();
                btn.Width = 50;
                btn.Height = 50;
                btn.IsEnabled = false;
                btn.Content = null;

                _board.DeletedBlackFigures.Add(btn);

                if (i >= _board._size)
                {
                    Grid.SetColumn(btn, 0);
                    BlackFigure.Children.Add(btn);
                }
                else
                {
                    Grid.SetColumn(btn, 1);
                    BlackFigure1.Children.Add(btn);
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

            if (_board.Player == ColorOfFigure.White) //если ход игрока за белыми фигурами
            {
                if (_board.IsChek == false)
                {
                    if ((_board.Fields[(int)point.X, (int)point.Y].FigurBase == null) ||
                        (_board.Fields[(int)point.X, (int)point.Y].FigurBase != null
                        && _board.Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure == ColorOfFigure.Black))
                    {
                        return;
                    }
                    else
                    {
                        btn.Background = Brushes.Purple;
                        _board.CurrentPoint = point;

                        _board.listRules = _board.Fields[(int)point.X, (int)point.Y].FigurBase.Rules(_board.Fields, _board.Buttons, point)
                           .Where(c => (c.X < _board._size && c.Y < _board._size) && (c.X >= 0 && c.Y >= 0))
                           .Select(c => new Point(c.X, c.Y)).ToList();

                        _board.ShowRulesOfFigure(_board.listRules, ColorOfFigure.White);
                        _board.IsChek = true;
                    }
                }
                else if (_board.IsChek == true)
                {
                    if (_board.whiteKing.IsChess == true)
                    {
                        //var tmpDictionary = new Dictionary<FigurBase, List<Point>>();
                        //var tmpCurrentPoint = _board.CurrentPoint;
                        var tmpFields = new Board();
                        tmpFields = _board;
                        //var tmpFigure = tmpFields.Fields[(int)tmpCurrentPoint.X, (int)tmpCurrentPoint.Y].FigurBase;

                        if (tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase != null && tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure != ColorOfFigure.White)
                        {
                            tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase = tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase;
                            tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase = null;
                            tmpFields.AddRulesInDictionary(tmpFields.ForCheckChessOrMate);

                            if (tmpFields.whiteKing.CheckChess(tmpFields.ForCheckChessOrMate, ColorOfFigure.White))
                            {
                                MessageBox.Show("Белым шах");
                                return;
                            }
                            else
                            {
                                //tmpFields[(int)point.X, (int)point.Y].FigurBase = null;

                                tmpFields.DoActionFigures(_board.listRules, point, ColorOfFigure.White);
                                tmpFields.whiteKing.IsChess = false;

                                _board = tmpFields;
                                _board.Player = ColorOfFigure.Black;
                                _board.Buttons[(int)point.X, (int)point.Y].Content = _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content;
                                _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content = null;

                                if (_board.Player == ColorOfFigure.Black)
                                {
                                    tbWhite.Visibility = Visibility.Collapsed;
                                    tbBlack.Visibility = Visibility.Visible;
                                }

                                return;
                            }
                        }
                        else if (tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase == null)
                        {
                            tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase = tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase;
                            tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase = null;
                            tmpFields.AddRulesInDictionary(tmpFields.ForCheckChessOrMate);

                            if (tmpFields.whiteKing.CheckChess(tmpFields.ForCheckChessOrMate, ColorOfFigure.White))
                            {
                                MessageBox.Show("Белым шах");
                                tmpFields.IsChek = false;
                                return;
                            }
                            else
                            {
                                //tmpFields[(int)point.X, (int)point.Y].FigurBase = null;
                                tmpFields.DoActionFigures(_board.listRules, point, ColorOfFigure.White);
                                tmpFields.blackKing.IsChess = false;

                                _board = tmpFields;
                                _board.Player = ColorOfFigure.Black;
                                _board.Buttons[(int)point.X, (int)point.Y].Content = _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content;
                                _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content = null;

                                if (_board.Player == ColorOfFigure.Black)
                                {
                                    tbWhite.Visibility = Visibility.Collapsed;
                                    tbBlack.Visibility = Visibility.Visible;
                                }

                                return;
                            }
                        }
                    }

                    _board.DoActionFigures(_board.listRules, point, ColorOfFigure.White);

                    if (_board.blackKing.CheckChess(_board.ForCheckChessOrMate, ColorOfFigure.Black))
                    {
                        MessageBox.Show("Черным шах");
                        _board.blackKing.IsChess = true;
                    }
                    else
                        _board.blackKing.IsChess = false;

                    if (_board.Player == ColorOfFigure.Black)
                    {
                        tbBlack.Visibility = Visibility.Visible;
                        tbWhite.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else if (_board.Player == ColorOfFigure.Black) // если ход  игрока за черными фигурами
            {
                if (_board.IsChek == false)
                {
                    if ((_board.Fields[(int)point.X, (int)point.Y].FigurBase == null) ||
                        (_board.Fields[(int)point.X, (int)point.Y].FigurBase != null && _board.Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure == ColorOfFigure.White))
                    {
                        return;
                    }
                    else
                    {
                        btn.Background = Brushes.Purple;
                        _board.CurrentPoint = point;

                        _board.listRules = _board.Fields[(int)point.X, (int)point.Y].FigurBase.Rules(_board.Fields, _board.Buttons, point)
                           .Where(c => (c.X < _board._size && c.Y < _board._size) && (c.X >= 0 && c.Y >= 0))
                           .Select(c => new Point(c.X, c.Y)).ToList();

                        _board.ShowRulesOfFigure(_board.listRules, ColorOfFigure.Black);

                        _board.IsChek = true;
                    }
                }
                else if (_board.IsChek == true)
                {
                    if (_board.blackKing.IsChess == true)
                    {
                        //var tmpDictionary = new Dictionary<FigurBase, List<Point>>();
                        //var tmpCurrentPoint = _board.CurrentPoint;
                        var tmpFields = new Board();
                        tmpFields = _board;
                        //var tmpFigure = tmpFields[(int)tmpCurrentPoint.X, (int)tmpCurrentPoint.Y].FigurBase;

                        if (tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase != null && tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure != ColorOfFigure.Black)
                        {
                            tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase = tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase;
                            tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase = null;
                            tmpFields.AddRulesInDictionary(tmpFields.ForCheckChessOrMate);

                            if (tmpFields.blackKing.CheckChess(tmpFields.ForCheckChessOrMate, ColorOfFigure.Black))
                            {
                                MessageBox.Show("Черным шах");
                                _board.IsChek = false;
                                return;
                            }
                            else
                            {
                                //tmpFields[(int)point.X, (int)point.Y].FigurBase = null;
                                tmpFields.DoActionFigures(_board.listRules, point, ColorOfFigure.Black);
                                tmpFields.blackKing.IsChess = false;

                                _board = tmpFields;
                                _board.Player = ColorOfFigure.White;
                                _board.Buttons[(int)point.X, (int)point.Y].Content = _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content;
                                _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content = null;

                                if (_board.Player == ColorOfFigure.White)
                                {
                                    tbWhite.Visibility = Visibility.Visible;
                                    tbBlack.Visibility = Visibility.Collapsed;
                                }

                                return;
                            }
                        }
                        else if (tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase == null)
                        {
                            tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase = tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase;
                            tmpFields.Fields[(int)tmpFields.CurrentPoint.X, (int)tmpFields.CurrentPoint.Y].FigurBase = null;
                            tmpFields.AddRulesInDictionary(tmpFields.ForCheckChessOrMate);

                            if (tmpFields.blackKing.CheckChess(tmpFields.ForCheckChessOrMate, ColorOfFigure.Black))
                            {
                                MessageBox.Show("Черным шах");
                                _board.IsChek = false;
                                return;
                            }
                            else
                            {
                                //tmpFields.Fields[(int)point.X, (int)point.Y].FigurBase = null;

                                tmpFields.DoActionFigures(_board.listRules, point, ColorOfFigure.Black);
                                tmpFields.blackKing.IsChess = false;

                                _board = tmpFields;
                                _board.Player = ColorOfFigure.White;
                                _board.Buttons[(int)point.X, (int)point.Y].Content = _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content;
                                _board.Buttons[(int)_board.CurrentPoint.X, (int)_board.CurrentPoint.Y].Content = null;

                                if (_board.Player == ColorOfFigure.White)
                                {
                                    tbWhite.Visibility = Visibility.Visible;
                                    tbBlack.Visibility = Visibility.Collapsed;
                                }

                                return;
                            }
                        }
                    }
                    _board.DoActionFigures(_board.listRules, point, ColorOfFigure.Black);

                    if (_board.whiteKing.CheckChess(_board.ForCheckChessOrMate, ColorOfFigure.White))
                    {
                        MessageBox.Show("Белым шах");
                        _board.whiteKing.IsChess = true;
                    }
                    else
                        _board.whiteKing.IsChess = false;

                    if (_board.Player == ColorOfFigure.White)
                    {
                        tbWhite.Visibility = Visibility.Visible;
                        tbBlack.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _board.StartNewGame();

            tbBlack.Visibility = Visibility.Collapsed;
            tbWhite.Visibility = Visibility.Visible;
        }
    }
}