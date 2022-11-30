using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Ekz_WPF
{
    internal class Pawn : FigurBase
    {
        public Pawn(Image icon, ColorOfFigure colorFigure) : base(icon, colorFigure)
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

            if (ColorFigure == ColorOfFigure.Black)
            {
                if ( fields[indexRowDown,(int) point.Y].FigurBase == null  )
                {
                    if (point.X == 1 && fields[indexRowDown + 1, (int)point.Y].FigurBase == null)
                    {
                        yield return new Point(indexRowDown, point.Y);
                        yield return new Point(indexRowDown + 1, point.Y);
                    }
                    else
                    {
                        if (fields[indexRowDown, (int)point.Y].FigurBase == null )
                             yield return new Point(indexRowDown, point.Y);

                    }
                }

                 if((point.Y + 1) < size && fields[indexRowDown, (int)point.Y + 1].FigurBase != null && fields[indexRowDown , (int)point.Y + 1].FigurBase.ColorFigure == ColorOfFigure.White)
                {
                    yield return new Point(indexRowDown, (int)point.Y + 1);
                }

                if ((point.Y - 1) >= 0 && fields[indexRowDown, (int)point.Y - 1].FigurBase != null && fields[indexRowDown , (int)point.Y - 1].FigurBase.ColorFigure == ColorOfFigure.White)
                {
                    yield return new Point(indexRowDown , (int)point.Y - 1);
                }

            }
            else if (ColorFigure == ColorOfFigure.White)
            {
                if (fields[indexRowUp, (int)point.Y].FigurBase == null)
                {
                    if (point.X == size - 2 && fields[indexRowUp - 1, (int)point.Y].FigurBase == null)
                    {
                        yield return new Point(indexRowUp, point.Y);
                        yield return new Point(indexRowUp - 1, point.Y);
                    }
                    else
                        yield return new Point(indexRowUp, point.Y);
                }

                if ((point.Y + 1) < size && fields[indexRowUp, (int)point.Y + 1].FigurBase != null && fields[indexRowUp , (int)point.Y + 1].FigurBase.ColorFigure == ColorOfFigure.Black)
                {
                    yield return new Point(indexRowUp , (int)point.Y + 1);
                }

                if ((point.Y - 1) >= 0 && fields[indexRowUp, (int)point.Y - 1].FigurBase != null && fields[indexRowUp, (int)point.Y - 1].FigurBase.ColorFigure == ColorOfFigure.Black)
                {
                    yield return new Point(indexRowUp, (int)point.Y - 1);
                }

            }
        }
    }
}
