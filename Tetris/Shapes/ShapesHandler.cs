using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Shapes
{
    internal static class ShapesHandler
    {
        private static Shape[] shapesArray;

        static ShapesHandler()
        {
            shapesArray = new Shape[]
                {
                    new BackwardsLShape(),
                    new LShape(),
                    new SShape(),
                    new LineShape(),
                    new ZShape(),
                    new TShape(),
                    new SquareShape()
                };
        }

        public static Shape GetRandomShape()
        {
            var shape = shapesArray[new Random().Next(shapesArray.Length)];
            return shape;
        }

        public static bool CheckCollision(Shape shape)
        {
            foreach (var p in shape.Dots)
            {
                if (p.X < 0 ||
                    p.X > Game.Width - 1 ||
                    p.Y < 0 ||
                    p.Y > Game.Height - 1) return true;
            }
            if (Game.Board.Contains(shape)) return true;
            return false;
        }

        public static void MoveShapeLeft(ref Shape shape)
        {
            Shape clone = shape.Clone();
            clone.MoveLeft();

            if (CheckCollision(clone)) return;
            Draw(shape, clone);
            shape = clone;
        }

        public static void MoveShapeRight(ref Shape shape)
        {
            Shape clone = shape.Clone();
            clone.MoveRight();

            if (CheckCollision(clone)) return;
            Draw(shape, clone);
            shape = clone;
        }

        public static bool MoveShapeDown(ref Shape shape)
        {
            Shape clone = shape.Clone();
            clone.MoveDown();

            if (CheckCollision(clone)) return false;
            Draw(shape, clone);
            shape = clone;
            return true;
        }

        public static void RotateShapeLeft(ref Shape shape)
        {
            Shape clone = shape.Clone();
            clone.RotateLeft();

            if (CheckCollision(clone)) return;
            Draw(shape, clone);
            shape = clone;
        }

        public static void RotateShapeRight(ref Shape shape)
        {
            Shape clone = shape.Clone();
            clone.RotateRight();

            if (CheckCollision(clone)) return;
            Draw(shape, clone);
            shape = clone;
        }

        static void Draw(Shape oldShape, Shape newShape)
        {
            Game.IsDrawing = true;
            oldShape.Print("[]", ConsoleColor.Black);
            newShape.Print("  ");
            Game.IsDrawing = false;
        }

        public static void DrawFirst(Shape shape)
        {
            shape.Print("  ");
        }

        internal static void DisplayToSide(Shape shape)
        {
            int offsetX = 5, y = 1;
            Console.BackgroundColor = ConsoleColor.Black;

            Utilities.PlaceCursor(Game.Width + offsetX, y);
            Console.Write("        ");
            Utilities.PlaceCursor(Game.Width + offsetX, y + 1);
            Console.Write("        ");


            Console.BackgroundColor = shape.Color;
            foreach (var point in shape.Dots)
            {
                Utilities.PlaceCursor(Game.Width + offsetX + point.X, y + point.Y);
                Console.Write("  ");
            }
        }
    }
}
