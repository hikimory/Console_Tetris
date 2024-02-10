using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Shapes
{
    internal abstract class Shape
    {
        public Point[] Dots;
        public PointF Center;
        public ConsoleColor Color;

        protected Shape(Point[] dots, PointF center, ConsoleColor color)
        {
            Dots = dots;
            Center = center;
            Color = color;
        }

        public void Print(string symbols, int offsetX = 0, int offsetY = 0)
        {
            Console.BackgroundColor = Color;
            for (int i = 0; i < Dots.Length; i++)
            {
                Point point = Dots[i];
                Utilities.PlaceCursor(point.X + offsetX, point.Y + offsetY);
                //Console.Write("██");
                Console.Write(symbols);
            }
            //Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Print(string symbols, ConsoleColor color, int offsetX = 0, int offsetY = 0)
        {
            Console.BackgroundColor = color;
            for (int i = 0; i < Dots.Length; i++)
            {
                Point point = Dots[i];
                Utilities.PlaceCursor(point.X + offsetX, point.Y + offsetY);
                //Console.Write("██");
                Console.Write(symbols);
            }
            //Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void RotateLeft()
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                PointF p = new PointF(Dots[i].X, Dots[i].Y);
                Dots[i].X = ((int)((p.Y - Center.Y) + Center.X));
                Dots[i].Y = ((int)(-(p.X - Center.X) + Center.Y));
            }
            //var pieceBlocks = PieceBlocks;
            //for (int i = 0; i < pieceBlocks.Count; i++)
            //{
            //    (double x, double y) block = pieceBlocks[i];
            //    block = ((block.y - Center.y) + Center.x, -(block.x - Center.x) + Center.y);
            //    NextPieceBlocks[i] = ((int x, int y))block;
            //}

            //if (!CheckNormalCollision()) return;

            //Draw();
            //PieceBlocks = new(NextPieceBlocks);
            //DrawDropProjection();
        }

        public void RotateRight()
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                PointF p = new PointF(Dots[i].X, Dots[i].Y);
                Dots[i].X = ((int)(-(p.Y - Center.Y) + Center.X));
                Dots[i].Y = ((int)((p.X - Center.X) + Center.Y));
            }
            //var pieceBlocks = PieceBlocks;
            //for (int i = 0; i < pieceBlocks.Count; i++)
            //{
            //    (double x, double y) block = pieceBlocks[i];
            //    block = (-(block.y - Center.y) + Center.x, (block.x - Center.x) + Center.y);
            //    NextPieceBlocks[i] = ((int x, int y))block;
            //}

            //if (!CheckNormalCollision()) return;

            //Draw();
            //PieceBlocks = new(NextPieceBlocks);
            //DrawDropProjection();
        }

        public void MoveLeft()
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                Dots[i].X--;
            }
            Center.X--;
            //for (int i = 0; i < Dots.Length; i++)
            //{
            //    Dots[i].X -= 1;
            //}
            //Center.X -= 1;
            //var pieceBlocks = PieceBlocks;
            //for (int i = 0; i < pieceBlocks.Count; i++)
            //{
            //    var block = PieceBlocks[i];
            //    block.x--;
            //    NextPieceBlocks[i] = block;
            //}

            //if (!CheckNormalCollision()) return;

            //Center = (Center.x - 1, Center.y);
            //Draw();
            //PieceBlocks = new(NextPieceBlocks);
            //DrawDropProjection();
        }

        public void MoveRight()
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                Dots[i].X++;
            }
            Center.X++;
            //for (int i = 0; i < Dots.Length; i++)
            //{
            //    Dots[i].X += 1;
            //}
            //Center.X += 1;
            //var pieceBlocks = PieceBlocks;
            //for (int i = 0; i < pieceBlocks.Count; i++)
            //{
            //    var block = pieceBlocks[i];
            //    block.x++;
            //    NextPieceBlocks[i] = block;
            //}

            //if (!CheckNormalCollision()) return;

            //Center = (Center.x + 1, Center.y);
            //Draw();
            //PieceBlocks = new(NextPieceBlocks);
            //DrawDropProjection();
        }

        public void MoveDown()
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                Dots[i].Y++;
            }
            Center.Y++;
            //for (int i = 0; i < Dots.Length; i++)
            //{
            //    Dots[i].Y += 1;
            //}
            //Center.Y += 1;
            //var pieceBlocks = PieceBlocks;
            //for (int i = 0; i < pieceBlocks.Count; i++)
            //{
            //    var block = pieceBlocks[i];
            //    block.y++;
            //    NextPieceBlocks[i] = block;
            //}

            //if (!CheckStoppingCollision()) return false;
            //if (!CheckNormalCollision()) return true;

            //Center = (Center.x, Center.y + 1);
            //Draw();
            //PieceBlocks = new(NextPieceBlocks);
            //return true;
        }

        public abstract Shape Clone();
    }
}
