using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Ekz_WPF
{
    internal class Tura : FigurBase
    {
        public Tura(Image icon, ColorOfFigure colorFigure) : base(icon, colorFigure)
        {
        }

        public override IEnumerable<Point> Rules(Field[,] fields, Button[,] buttons, Point point)
        {
            int size = buttons.GetLength(0);

            int indexRowDown = (int)point.X + 1;
            int indexColRight = (int)point.Y + 1;

            int indexRowUp = 0;
            int indexColLeft = 0;

            if (point.X > 0 && point.Y > 0)
            {
                indexRowUp = (int)point.X - 1;
                indexColLeft = (int)point.Y - 1;
            }
            else if (point.X <= 0)
            {
                indexRowUp = 0;
                indexColLeft = (int)point.Y - 1;
            }
            else if (point.Y <= 0)
            {
                indexRowUp = (int)point.X - 1;
                indexColLeft = 0;
            }

            //Ход Вверх
            for (int i = indexRowUp; i >= 0; i--)
            {
                if (fields[i, (int)point.Y].FigurBase == null)
                {
                    yield return new Point(i, point.Y);
                }
                else if (fields[i, (int)point.Y].FigurBase.ColorFigure != this.ColorFigure)
                {
                    yield return new Point(i, point.Y);
                    break;
                }
                else if(fields[i, (int)point.Y].FigurBase.ColorFigure == this.ColorFigure)
                    break;
            }
            //Ход Вниз
            for (int i = indexRowDown; i < size; i++)
            {
                if (fields[i, (int)point.Y].FigurBase == null)
                {
                    yield return new Point(i, point.Y);
                }
                else if (fields[i, (int)point.Y].FigurBase.ColorFigure != this.ColorFigure)
                {
                    yield return new Point(i, point.Y);
                    break;
                }
                else if (fields[i , (int)point.Y].FigurBase.ColorFigure == this.ColorFigure)
                    break;
            }
            //Ход Влево
            for (int i = indexColLeft; i >= 0; i--)
            {
                if (fields[(int)point.X, i].FigurBase == null)
                {
                    yield return new Point(point.X, i);
                }
                else if (fields[(int)point.X, i].FigurBase.ColorFigure != this.ColorFigure)
                {
                    yield return new Point(point.X, i);
                    break;
                }
                else if (fields[(int)point.X , i].FigurBase.ColorFigure == this.ColorFigure)
                    break;
            }
            //Ход Вправо
            for (int i = indexColRight; i < size; i++)
            {
                if (fields[(int)point.X, i].FigurBase == null)
                {
                    yield return new Point(point.X, i);
                }
                else if (fields[(int)point.X, i].FigurBase.ColorFigure != this.ColorFigure)
                {
                    yield return new Point(point.X, i);
                    break;
                }
                else if(fields[(int)point.X, i].FigurBase.ColorFigure == this.ColorFigure)
                    break;
            }
        }
    }
}