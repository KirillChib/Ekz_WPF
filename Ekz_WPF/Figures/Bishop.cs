﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Ekz_WPF
{
    internal class Bishop : FigurBase
    {
        public Bishop(Image icon, ColorOfFigure colorFigure) : base(icon, colorFigure)
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
                if (j > size - 1 || i > size - 1)
                    break;

                if (fields[i, j].FigurBase == null)
                {
                    yield return new Point(i, j);
                }
                else if ((fields[i, j].FigurBase.ColorFigure != this.ColorFigure))
                {
                    yield return new Point(i, j);
                    break;
                }
                else if (fields[i, j].FigurBase.ColorFigure == this.ColorFigure)
                {
                    break;
                }

                j++;
            }

            //Расчет хода по диагонали вниз-влево
            j = indexColUp;
            for (int i = indexRowDown; i < size; i++)
            {
                if (j < 0 || i > size - 1)
                    break;

                if (fields[i, j].FigurBase == null)
                {
                    yield return new Point(i, j);
                }
                else if (fields[i, j].FigurBase.ColorFigure != this.ColorFigure)
                {
                    yield return new Point(i, j);
                    break;
                }
                else if (fields[i, j].FigurBase.ColorFigure == this.ColorFigure)
                    break;

                j--;
            }

            //Расчет хода по диагонали вверх-вправо
            j = indexColDown;
            for (int i = indexRowUp; i >= 0; i--)
            {
                if (i < 0 || j > size - 1)
                    break;

                if (fields[i, j].FigurBase == null)
                {
                    yield return new Point(i, j);
                }
                else if (fields[i, j].FigurBase.ColorFigure != this.ColorFigure)
                {
                    yield return new Point(i, j);
                    break;
                }
                else if (fields[i, j].FigurBase.ColorFigure == this.ColorFigure)
                    break;

                j++;
            }

            //Расчет хода по диагонали вверх - влево
            j = indexColUp;
            for (int i = indexRowUp; i >= 0; i--)
            {
                if (j < 0)
                    break;

                if (fields[i, j].FigurBase == null)
                {
                    yield return new Point(i, j);
                }
                else if (fields[i, j].FigurBase.ColorFigure != this.ColorFigure)
                {
                    yield return new Point(i, j);
                    break;
                }
                else if (fields[i, j].FigurBase.ColorFigure == this.ColorFigure)
                    break;

                j--;
            }
        }
    }
}