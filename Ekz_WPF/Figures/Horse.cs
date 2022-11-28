using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ekz_WPF
{
    internal class Horse : FigurBase
    {
        public Horse(Image icon, ColorOfFigure colorFigure) : base(icon, colorFigure)
        {
        }

        public override IEnumerable<Point> Rules(Field[,] fields, Button[,] buttons, Point point)
        {
            int step = 2;
            //if ((fields[(int)(point.X + 2), (int)(point.Y + 1)] == null) || (fields[(int)(point.X + 2), (int)(point.Y + 1)] != null && fields[(int)(point.X + 2), (int)(point.Y + 1)].FigurBase.ColorFigure != this.ColorFigure))
                yield return new Point(point.X + 2, point.Y + 1);
            //else if(fields[(int)(point.X + 2), (int)(point.Y - 1)] == null || (fields[(int)(point.X + 2), (int)(point.Y - 1)].FigurBase.ColorFigure != this.ColorFigure))
            yield return new Point(point.X + 2, point.Y - 1);
            //else if (fields[(int)(point.X - 2), (int)(point.Y + 1)] == null || (fields[(int)(point.X - 2), (int)(point.Y + 1)].FigurBase.ColorFigure != this.ColorFigure))
            yield return new Point(point.X - 2, point.Y + 1);
            //else if (fields[(int)(point.X - 2), (int)(point.Y - 1)] == null || (fields[(int)(point.X - 2), (int)(point.Y - 1)].FigurBase.ColorFigure != this.ColorFigure))
            yield return new Point(point.X - 2, point.Y - 1);
            //else if (fields[(int)(point.X - 1), (int)(point.Y + 2)] == null || (fields[(int)(point.X - 1), (int)(point.Y + 2)].FigurBase.ColorFigure != this.ColorFigure))
            yield return new Point(point.X - 1, point.Y + 2);
            //else if (fields[(int)(point.X - 1), (int)(point.Y - 2)] == null || (fields[(int)(point.X - 1), (int)(point.Y - 2)].FigurBase.ColorFigure != this.ColorFigure))
            yield return new Point(point.X - 1, point.Y - 2);
            //else if (fields[(int)(point.X + 1), (int)(point.Y - 2)] == null || (fields[(int)(point.X + 1), (int)(point.Y - 2)].FigurBase.ColorFigure != this.ColorFigure))
            yield return new Point(point.X + 1, point.Y - 2);




        }
    }
}
