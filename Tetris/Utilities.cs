using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal static class Utilities
    {
        internal static void PlaceCursor(int x, int y)
        {
            int offset = 1;
            Console.SetCursorPosition(x * 2 + offset, y + offset);
        }

        internal static string Repeat(this string str, int times)
        {
            return string.Concat(Enumerable.Repeat(str, times));
        }
    }
}
