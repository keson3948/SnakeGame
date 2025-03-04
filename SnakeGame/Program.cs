using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

// Červená hlava, zelené tělo, modré třešničky
//Console.WindowHeight = 16;
//Console.WindowWidth = 32;

namespace Snake
{
    class Program
    {
        static Random random = new Random();
        static int windowWidth = Console.WindowWidth;
        static int windowHeight = Console.WindowHeight;
        
        static int score = 5;
        static bool isGameOver;
        static Direction movement = Direction.Right;
        
        static Pixel head;
        static Pixel berry;
        static List<Pixel> body = new List<Pixel>();
        
        static void Main(string[] args)
        {
            InitializeGame();
            GameLoop();
        }

        private static void InitializeGame()
        {
            head =  new Pixel(windowWidth/2, windowHeight/2, ConsoleColor.Red);
            berry = new Pixel(random.Next(1, windowWidth - 2), random.Next(1, windowHeight - 2), ConsoleColor.Cyan);
        }

        private static void GameLoop()
        {
            while (isGameOver != true)
            {
                DrawBorders(windowHeight, windowWidth);
                DrawSnake();
                DrawBerry();
                HandleInput();
                MoveSnake();
                GameOverCheck();
                Task.Delay(100).Wait();
            }

            ShowEndgameScreen();
        }

        private static void MoveSnake()
        {
            body.Add(new Pixel(head.Xpos, head.Ypos, ConsoleColor.Green));
            switch (movement)
            {
                case Direction.Up:
                    head.Ypos--;
                    break;
                case Direction.Down:
                    head.Ypos++;
                    break;
                case Direction.Left:
                    head.Xpos--;
                    break;
                case Direction.Right:
                    head.Xpos++;
                    break;
            }
            if (body.Count() > score)
            {
                body.RemoveAt(0);
            }
        }

        public static void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo toets = Console.ReadKey(true);
                if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != Direction.Down)
                {
                    movement = Direction.Up;
                }
                if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != Direction.Up )
                {
                    movement = Direction.Down;
                }
                if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != Direction.Right)
                {
                    movement = Direction.Left;
                }
                if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != Direction.Left)
                {
                    movement = Direction.Right;
                }
            }
        }

        private static void GameOverCheck()
        {
            if (head.Xpos == windowWidth-1 || head.Xpos == 0 ||head.Ypos == windowHeight-1 || head.Ypos == 0)
            { 
                isGameOver = true;
            }
            
            if (berry.Xpos == head.Xpos && berry.Ypos == head.Ypos)
            {
                score++;
                berry = new Pixel(random.Next(1, windowWidth-2), random.Next(1, windowHeight-2), ConsoleColor.Cyan);
            } 
        }

        public static void ShowEndgameScreen()
        {
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 +1);
        }

        public static void DrawBorders(int windowHeight, int windowWidth)
        {
            Console.Clear();
            
            for (int i = 0;i< windowWidth; i++)
            {
                DrawPixel(i, 0);
            }
            for (int i = 0; i < windowWidth; i++)
            {
                DrawPixel(i, windowHeight -1);
            }
            for (int i = 0; i < windowHeight; i++)
            {
                DrawPixel(0, i);
            }
            for (int i = 0; i < windowHeight; i++)
            {
                DrawPixel(windowWidth - 1, i);
            }
        }

        public static void DrawSnake()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            
            for (int i = 0; i < body.Count(); i++)
            {
                Console.SetCursorPosition(body[i].Xpos, body[i].Ypos);
                Console.Write("■");
                if (body[i].Xpos == head.Xpos && body[i].Ypos == head.Ypos)
                {
                    isGameOver = true;
                }
            }
            
            Console.SetCursorPosition(head.Xpos, head.Ypos);
            Console.ForegroundColor = head.Color;
            Console.Write("■");
        }

        public static void DrawBerry()
        {
            DrawPixel(berry.Xpos,berry.Ypos, ConsoleColor.Cyan);
        }

        public static void DrawPixel(int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write("■");
        }

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        
        class Pixel
        {
            public Pixel(int x, int y, ConsoleColor consoleColor)
            {
                Xpos = x;
                Ypos = y;
                Color = consoleColor;
            }
  
            public int Xpos { get; set; }
            public int Ypos { get; set; }
            public ConsoleColor Color { get; set; }
        }
    }
}
