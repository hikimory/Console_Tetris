using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Shapes
{
    internal class LineShape : Shape
    {
        public LineShape() : base(
            new Point[4]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0)
            },
            new PointF(1.5f, 0.5f),
            ConsoleColor.Cyan)
        {}

        public override Shape Clone()
        {
            var cloneDots = new Point[4];

            for (int i = 0; i < Dots.Length; i++)
            {
                cloneDots[i] = Dots[i];
            }

            return new LineShape
            {
                Dots = cloneDots,
                Center = Center,
                Color = Color
            };
        }
    }
}
