using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Shapes;

namespace Tetris
{
    internal static class Menu
    {
        private static Point[] _menuCoordinate;
        private static string _pointer = "==>";
        private static int _currentIndex = 0;
        private static int _bestScore = 0;

        static Menu()
        {
            _menuCoordinate = new Point[] {
                new Point(30, 11),
                new Point(30, 13),
            };
        }

        internal static void DisplayStartMenu()
        {
            SetDafaultConsole();
            PrintStartLogo();
            PrinMenu("Start Game", "End Game");
        }

        internal static void DisplayFailMenu()
        {
            SetDafaultConsole();
            PrintFailLogo();
            PrinMenu("Restart Game", "End Game");
        }

        internal static void PrintStartLogo()
        {
            string T = """
             |_|_|_|_|_|
                 |_|    
                 |_|    
                 |_|    
                 |_|    
             """;


            string E = """
             |_|_|_|_|
             |_|      
             |_|_|_|  
             |_|      
             |_|_|_|_|
             """;

            string R = """
             |_|_|_|  
             |_|   |_|
             |_|_|_|  
             |_|   |_|
             |_|   |_|
             """;

            string I = """
             |_|_|_|
               |_|  
               |_|  
               |_|  
             |_|_|_|
             """;

            string S = """
               |_|_|_|  
             |_|        
               |_|_|    
                   |_|  
             |_|_|_|    
             """;

            PrintLetter(T, 11, 3, ConsoleColor.Black, ConsoleColor.Blue);
            PrintLetter(E, 22, 3, ConsoleColor.Black, ConsoleColor.Cyan);
            PrintLetter(T, 31, 3, ConsoleColor.Black, ConsoleColor.DarkYellow);
            PrintLetter(R, 42, 3, ConsoleColor.Black, ConsoleColor.Yellow);
            PrintLetter(I, 51, 3, ConsoleColor.Black, ConsoleColor.Green);
            PrintLetter(S, 58, 3, ConsoleColor.Black, ConsoleColor.Red);
        }

        internal static void PrintFailLogo()
        {
                                 
            string F = """
             |_|_|_|_|
             |_|       
             |_|_|_|  
             |_|      
             |_|      
             """;


            string A = """
               |_|_|  
             |_|   |_|
             |_|_|_|_|
             |_|   |_|
             |_|   |_|
             """;

            string I = """
             |_|_|_|
               |_|  
               |_|  
               |_|  
             |_|_|_|
             """;

            string L = """
             |_|
             |_|      
             |_|      
             |_|
             |_|_|_|_|
             """;

            PrintLetter(F, 21, 3, ConsoleColor.Black, ConsoleColor.Blue);
            PrintLetter(A, 32, 3, ConsoleColor.Black, ConsoleColor.Cyan);
            PrintLetter(I, 43, 3, ConsoleColor.Black, ConsoleColor.Green);
            PrintLetter(L, 52, 3, ConsoleColor.Black, ConsoleColor.Yellow);
        }

        internal static void PrinMenu(string option1, string option2)
        {
            SetDafaultColor();
            Console.SetCursorPosition(_menuCoordinate[0].X + 5, _menuCoordinate[0].Y);
            Console.WriteLine(option1);
            Console.SetCursorPosition(_menuCoordinate[1].X + 5, _menuCoordinate[1].Y);
            Console.WriteLine(option2);

            Console.SetCursorPosition(_menuCoordinate[0].X, _menuCoordinate[0].Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(_pointer);
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(32, 16);
            Console.WriteLine($"Best score: {_bestScore}");
        }

        internal static void StartGame()
        {
            DisplayStartMenu();
            while (true)
            {
                int choice = MakeChoice();
                if (choice != 0) Environment.Exit(0);
                int score = Game.StartGame();
                _bestScore = Math.Max(score, _bestScore);
                DisplayFailMenu();
            }
        }


        internal static void SetDafaultConsole()
        {
            SetDafaultColor();
            Console.Clear();
        }
        internal static void SetDafaultColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static int MakeChoice()
        {
            bool isDone = false;
            while (isDone == false)
            {
                var keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        int newIndex = _currentIndex - 1;
                        if (CanMove(newIndex))
                        {
                            MakeMove(_currentIndex, newIndex);
                            _currentIndex = newIndex;
                        }
                        break;
                    }
                    case ConsoleKey.DownArrow:
                    {
                        int newIndex = _currentIndex + 1;
                        if (CanMove(newIndex))
                        {
                            MakeMove(_currentIndex, newIndex);
                            _currentIndex = newIndex;
                        }
                        break;
                    }
                    case ConsoleKey.Enter:
                    {
                        isDone = true;
                        break;
                    }
                    default:
                        break;
                }
            }
            Console.Clear();
            return _currentIndex;
        }

        internal static void MakeMove(int oldIndex, int newIndex)
        {
            Console.SetCursorPosition(_menuCoordinate[oldIndex].X, _menuCoordinate[oldIndex].Y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(_pointer);
            Console.SetCursorPosition(_menuCoordinate[newIndex].X, _menuCoordinate[newIndex].Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(_pointer);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static bool CanMove(int index)
        {
            return index >= 0 && index <= 1;
        }

        static void PrintLetter(string letter, int offsetX, int offsetY, ConsoleColor backColor, ConsoleColor textColor)
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = textColor;
            var letterByLine = letter.Split('\n');
            for (int i = 0; i < letterByLine.Length; i++)
            {
                Console.SetCursorPosition(offsetX, i + offsetY);
                Console.WriteLine(letterByLine[i]);
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
