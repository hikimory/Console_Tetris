using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Shapes
{
    internal class BackwardsLShape : Shape
    {
        public BackwardsLShape() : base(
            new Point[4]
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 1),
                new Point(2, 1)
            },
            new PointF(1f, 1f),
            ConsoleColor.Blue)
        { }

        public override Shape Clone()
        {
            var cloneDots = new Point[4];

            for (int i = 0; i < Dots.Length; i++)
            {
                cloneDots[i] = Dots[i];
            }

            return new BackwardsLShape
            {
                Dots = cloneDots,
                Center = Center,
                Color = Color
            };
        }
    }
}
