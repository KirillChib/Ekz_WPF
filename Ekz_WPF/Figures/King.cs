using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Ekz_WPF
{
    internal class King : FigurBase
    {
        public Point CurrentField { get; set; }

        public King(Image icon, ColorOfFigure colorFigure) : base(icon, colorFigure)
        {
        }

        public override IEnumerable<Point> Rules(Field[,] fields, Button[,] buttons, Point point)
        {
            int size = buttons.GetLength(0);

            int indexRowDown = (int)point.X + 1;
            int indexColDown = (int)point.Y + 1;

            int indexRowUp = 0;
            int indexColUp = 0;

            if (point.X > 0 && point.Y > 0)
            {
                indexRowUp = (int)point.X - 1;
                indexColUp = (int)point.Y - 1;
            }
            else if (point.X <= 0)
            {
                indexRowUp = 0;
                indexColUp = (int)point.Y - 1;
            }
            else if (point.Y <= 0)
            {
                indexRowUp = (int)point.X - 1;
                indexColUp = 0;
            }

            //Расчет хода по диагонали вниз-вправо
            int j = indexColDown;
            for (int i = indexRowDown; i < size; i++)
            {
                if (fields[i, j].FigurBase == null || fields[i, j].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(i, j);

                break;
            }

            //Расчет хода по диагонали вниз-влево
            j = indexColUp;
            for (int i = indexRowDown; i < size; i++)
            {
                if (fields[i, j].FigurBase == null || fields[i, j].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(i, j);
                break;
            }

            //Расчет хода по диагонали вверх-вправо
            j = indexColDown;
            for (int i = indexRowUp; i > 0; i--)
            {
                if (fields[i, j].FigurBase == null || fields[i, j].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(i, j);
                break;
            }

            //Расчет хода по диагонали вверх - влево
            j = indexColUp;
            for (int i = indexRowUp; i >= 0; i--)
            {
                if (fields[i, j].FigurBase == null || fields[i, j].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(i, j);

                break;
            }

            //Ход Вверх
            for (int i = indexRowUp; i >= 0; i--)
            {
                if (fields[i, (int)point.Y].FigurBase == null || fields[i, (int)point.Y].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(i, point.Y);
                break;
            }
            //Ход Вниз
            for (int i = indexRowDown; i < size; i++)
            {
                if (fields[i, (int)point.Y].FigurBase == null || fields[i, (int)point.Y].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(i, point.Y);
                break;
            }
            //Ход Влево
            for (int i = indexColUp; i >= 0; i--)
            {
                if (fields[(int)point.X, j].FigurBase == null || fields[(int)point.X, j].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(point.X, i);
                break;
            }
            //Ход Вправо
            for (int i = indexColDown; i < size; i++)
            {
                if (fields[(int)point.X, j].FigurBase == null || fields[(int)point.X, j].FigurBase.ColorFigure != this.ColorFigure)
                    yield return new Point(point.X, i);
                break;
            }
        }

        public bool CheckChess(Dictionary<FigurBase, List<Point>> points)
        {
            var list = new List<Point>();
            foreach (var item in points)
            {
                foreach (var it in points.Values)
                    list = it;
                foreach (var it in list)
                {
                    if (it == CurrentField)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckMate(Dictionary<FigurBase, List<Point>> points, List<Point> rules)
        {
            var list = new List<Point>();
            foreach (var item in points)
            {
                foreach (var it in points.Values)
                    list = it;
                foreach (var it in list)
                {
                    foreach(var i in rules)
                    {
                        if (i != it)
                        {
                            return false;
                        }
                    }
                    
                }
            }
            return true; ;
        }
    }
}