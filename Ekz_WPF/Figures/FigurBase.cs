using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Ekz_WPF
{
    internal abstract class FigurBase
    {
        public Image Icon { get; set; }
        public readonly ColorOfFigure ColorFigure;

        public FigurBase(Image icon, ColorOfFigure colorFigure)
        {
            Icon = icon;

            ColorFigure = colorFigure;
        }

        public Dictionary<int, int> Rules(Field[] fields , Button[] buttons, Point point)
        {
            var toGo = new Dictionary<int, int>();

            return toGo;
        }
    }
}