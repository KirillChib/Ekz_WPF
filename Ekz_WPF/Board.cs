using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ekz_WPF
{
    internal class Board
    {
        public readonly int _size = 8;
        public List<Point> listRules = new List<Point>();
        public Point CurrentPoint { get; set; }
        private Dictionary<FigurBase, List<Point>> forCheckChessOrMate;
        private List<Button> deletedWhiteFigures;
        private List<Button> deletedBlackFigures;
        public bool IsChek { get; set; }
        private ColorOfFigure player = ColorOfFigure.White;
        private Field[,] _fields;
        private Button[,] _buttons;
        private LoadImages images;

        public Button[,] Buttons { get => _buttons; set => _buttons = value; }
        internal Field[,] Fields { get => _fields; set => _fields = value; }
        internal ColorOfFigure Player { get => player; set => player = value; }
        internal Dictionary<FigurBase, List<Point>> ForCheckChessOrMate { get => forCheckChessOrMate; set => forCheckChessOrMate = value; }
        public List<Button> DeletedWhiteFigures { get => deletedWhiteFigures; }
        public List<Button> DeletedBlackFigures { get => deletedBlackFigures; }

        public Board()
        {
            images = new LoadImages();
            _fields = new Field[_size, _size];
            _buttons = new Button[_size, _size];
            forCheckChessOrMate = new Dictionary<FigurBase, List<Point>>();
            deletedWhiteFigures = new List<Button>();
            deletedBlackFigures = new List<Button>();

            IsChek = false;

            CreatedBoard();
        }

        private void CreatedBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _fields[i, j] = new Field();
                }
            }
        }

        public void StartNewGame()
        {
            _fields[0, 0].FigurBase = new Tura(images.BlackTura, ColorOfFigure.Black);

            _buttons[0, 0].Content = images.BlackTura;

            _fields[0, 1].FigurBase = new Horse(images.BlackHorse, ColorOfFigure.Black);
            _buttons[0, 1].Content = images.BlackHorse;

            _fields[0, 2].FigurBase = new Bishop(images.BlackBishop, ColorOfFigure.Black);
            _buttons[0, 2].Content = images.BlackBishop;

            _fields[0, 3].FigurBase = new Queen(images.BlackQueen, ColorOfFigure.Black);
            _buttons[0, 3].Content = images.BlackQueen;

            _fields[0, 4].FigurBase = new King(images.BlackKing, ColorOfFigure.Black) { CurrentField = new Point(0, 4) };
            _buttons[0, 4].Content = images.BlackKing;

            _fields[0, 5].FigurBase = new Bishop(images.BlackBishop1, ColorOfFigure.Black);
            _buttons[0, 5].Content = images.BlackBishop1;

            _fields[0, 6].FigurBase = new Horse(images.BlackHorse1, ColorOfFigure.Black);
            _buttons[0, 6].Content = images.BlackHorse1;

            _fields[0, 7].FigurBase = new Tura(images.BlackTura1, ColorOfFigure.Black);
            _buttons[0, 7].Content = images.BlackTura1;

            _fields[1, 0].FigurBase = new Pawn(images.BlackPawn, ColorOfFigure.Black);
            _buttons[1, 0].Content = images.BlackPawn;

            _fields[1, 1].FigurBase = new Pawn(images.BlackPawn1, ColorOfFigure.Black);
            _buttons[1, 1].Content = images.BlackPawn1;

            _fields[1, 2].FigurBase = new Pawn(images.BlackPawn2, ColorOfFigure.Black);
            _buttons[1, 2].Content = images.BlackPawn2;

            _fields[1, 3].FigurBase = new Pawn(images.BlackPawn3, ColorOfFigure.Black);
            _buttons[1, 3].Content = images.BlackPawn3;

            _fields[1, 4].FigurBase = new Pawn(images.BlackPawn4, ColorOfFigure.Black);
            _buttons[1, 4].Content = images.BlackPawn4;

            _fields[1, 5].FigurBase = new Pawn(images.BlackPawn5, ColorOfFigure.Black);
            _buttons[1, 5].Content = images.BlackPawn5;

            _fields[1, 6].FigurBase = new Pawn(images.BlackPawn6, ColorOfFigure.Black);
            _buttons[1, 6].Content = images.BlackPawn6;

            _fields[1, 7].FigurBase = new Pawn(images.BlackPawn7, ColorOfFigure.Black);
            _buttons[1, 7].Content = images.BlackPawn7;

            _fields[7, 0].FigurBase = new Tura(images.WhiteTura, ColorOfFigure.White);
            _buttons[7, 0].Content = images.WhiteTura;

            _fields[7, 1].FigurBase = new Horse(images.WhiteHorse, ColorOfFigure.White);
            _buttons[7, 1].Content = images.WhiteHorse;

            _fields[7, 2].FigurBase = new Bishop(images.WhiteBishop, ColorOfFigure.White);
            _buttons[7, 2].Content = images.WhiteBishop;

            _fields[7, 3].FigurBase = new Queen(images.WhiteQueen, ColorOfFigure.White);
            _buttons[7, 3].Content = images.WhiteQueen;

            _fields[7, 4].FigurBase = new King(images.WhiteKing, ColorOfFigure.White) { CurrentField = new Point(7, 4) };
            _buttons[7, 4].Content = images.WhiteKing;

            _fields[7, 5].FigurBase = new Bishop(images.WhiteBishop1, ColorOfFigure.White);
            _buttons[7, 5].Content = images.WhiteBishop1;

            _fields[7, 6].FigurBase = new Horse(images.WhiteHorse1, ColorOfFigure.White);
            _buttons[7, 6].Content = images.WhiteHorse1;

            _fields[7, 7].FigurBase = new Tura(images.WhiteTura1, ColorOfFigure.White);
            _buttons[7, 7].Content = images.WhiteTura1;

            _fields[6, 0].FigurBase = new Pawn(images.WhitePawn, ColorOfFigure.White);
            _buttons[6, 0].Content = images.WhitePawn;

            _fields[6, 1].FigurBase = new Pawn(images.WhitePawn1, ColorOfFigure.White);
            _buttons[6, 1].Content = images.WhitePawn1;

            _fields[6, 2].FigurBase = new Pawn(images.WhitePawn2, ColorOfFigure.White);
            _buttons[6, 2].Content = images.WhitePawn2;

            _fields[6, 3].FigurBase = new Pawn(images.WhitePawn3, ColorOfFigure.White);
            _buttons[6, 3].Content = images.WhitePawn3;

            _fields[6, 4].FigurBase = new Pawn(images.WhitePawn4, ColorOfFigure.White);
            _buttons[6, 4].Content = images.WhitePawn4;

            _fields[6, 5].FigurBase = new Pawn(images.WhitePawn5, ColorOfFigure.White);
            _buttons[6, 5].Content = images.WhitePawn5;

            _fields[6, 6].FigurBase = new Pawn(images.WhitePawn6, ColorOfFigure.White);
            _buttons[6, 6].Content = images.WhitePawn6;

            _fields[6, 7].FigurBase = new Pawn(images.WhitePawn7, ColorOfFigure.White);
            _buttons[6, 7].Content = images.WhitePawn7;
        }

        public void RefreshBackGround()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Buttons[i, j].BorderThickness = new Thickness(0, 0, 0, 0);
                    Buttons[i, j].Background = (i + j) % 2 != 0 ? Brushes.IndianRed : Brushes.Bisque;
                }
            }
        }

        public Point ButtonIsClick(Button button)
        {
            var point = new Point();

            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                {
                    if (Buttons[i, j] == button)
                    {
                        point.X = i;
                        point.Y = j;
                    }
                }

            return point;
        }

        public void ShowRulesOfFigure(List<Point> rules, ColorOfFigure color)
        {
            if (color == ColorOfFigure.White)
            {
                foreach (var item in rules)
                {
                    if ((Fields[(int)item.X, (int)item.Y].FigurBase != null) &&
                        (Fields[(int)item.X, (int)item.Y].FigurBase.ColorFigure == ColorOfFigure.White))
                    {
                        continue;
                    }

                    Buttons[(int)item.X, (int)item.Y].BorderThickness = new Thickness(3);
                    Buttons[(int)item.X, (int)item.Y].BorderBrush = Brushes.Purple;
                }
            }
            else if (color == ColorOfFigure.Black)
            {
                foreach (var item in rules)
                {
                    if ((Fields[(int)item.X, (int)item.Y].FigurBase != null) &&
                        (Fields[(int)item.X, (int)item.Y].FigurBase.ColorFigure == ColorOfFigure.Black))
                    {
                        continue;
                    }

                    Buttons[(int)item.X, (int)item.Y].BorderThickness = new Thickness(3);
                    Buttons[(int)item.X, (int)item.Y].BorderBrush = Brushes.Purple;
                }
            }
        }

        public void DoActionFigures(List<Point> rules, Point point, ColorOfFigure color)
        {
            if (color == ColorOfFigure.White)
            {
                foreach (var item in rules)
                {
                    if (item == point)
                    {
                        if (Fields[(int)point.X, (int)point.Y].FigurBase is King king)
                        {
                            king.CurrentField = point;
                        }

                        if ((Fields[(int)point.X, (int)point.Y].FigurBase != null && Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure == ColorOfFigure.Black) ||
                            Fields[(int)point.X, (int)point.Y].FigurBase == null)
                        {
                            Fields[(int)item.X, (int)item.Y].FigurBase = Fields[(int)CurrentPoint.X, (int)CurrentPoint.Y].FigurBase;
                            Fields[(int)CurrentPoint.X, (int)CurrentPoint.Y].FigurBase = null;

                            if (Fields[(int)point.X, (int)point.Y].FigurBase != null && Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure == ColorOfFigure.Black)
                            {
                                foreach (var it in deletedBlackFigures)
                                {
                                    if (it.Content == null)
                                    {
                                        it.Content = Buttons[(int)item.X, (int)item.Y].Content;
                                    }
                                }
                            }

                            Buttons[(int)item.X, (int)item.Y].Content = Buttons[(int)CurrentPoint.X, (int)CurrentPoint.Y].Content;
                            Buttons[(int)CurrentPoint.X, (int)CurrentPoint.Y].Content = null;

                            IsChek = false;

                            Player = ColorOfFigure.Black;

                            return;
                        }
                    }
                }
                IsChek = false;
                return;
            }
            else if (color == ColorOfFigure.Black)
            {
                foreach (var item in listRules)
                {
                    if (item == point)
                    {
                        if ((Fields[(int)point.X, (int)point.Y].FigurBase != null && Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure == ColorOfFigure.White) ||
                            Fields[(int)point.X, (int)point.Y].FigurBase == null)
                        {
                            if (Fields[(int)point.X, (int)point.Y].FigurBase is King king)
                            {
                                king.CurrentField = point;
                            }
                            Fields[(int)item.X, (int)item.Y].FigurBase = Fields[(int)CurrentPoint.X, (int)CurrentPoint.Y].FigurBase;
                            Fields[(int)CurrentPoint.X, (int)CurrentPoint.Y].FigurBase = null;

                            //if(Fields[(int)point.X, (int)point.Y].FigurBase != null && Fields[(int)point.X, (int)point.Y].FigurBase.ColorFigure == ColorOfFigure.White)
                            //{
                            foreach (var it in deletedWhiteFigures)
                            {
                                if (it.Content == null)
                                {
                                    it.Content = Buttons[(int)item.X, (int)item.Y].Content;
                                }
                            }
                            // }

                            Buttons[(int)item.X, (int)item.Y].Content = Buttons[(int)CurrentPoint.X, (int)CurrentPoint.Y].Content;
                            Buttons[(int)CurrentPoint.X, (int)CurrentPoint.Y].Content = null;

                            IsChek = false;

                            Player = ColorOfFigure.White;
                            return;
                        }
                    }
                }
                IsChek = false;
            }
        }

        public void AddRulesInDictionary(FigurBase figur, List<Point> rules)
        {
            if (ForCheckChessOrMate.Count == 0)
            {
                ForCheckChessOrMate.Add(figur, rules);
                return;
            }
            else
            {
                foreach (var item in ForCheckChessOrMate)
                {
                    if (item.Key == figur)
                    {
                        ForCheckChessOrMate.Remove(figur);
                        ForCheckChessOrMate.Add(figur, rules);
                        return;
                    }
                }
            }
            ForCheckChessOrMate.Add(figur, rules);
        }
    }
}