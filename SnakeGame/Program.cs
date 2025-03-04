using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

// Červená hlava, zelené tělo, modré třešničky

namespace Snake
{
    class Program
    {
        static int windowWidth = Console.WindowWidth;
        static int windowHeight = Console.WindowHeight;
        static int score = 5;
        static Direction movement = Direction.Right;
        static Pixel head;
        static bool isGameOver;
        static List<int> bodyXPositions = new List<int>();
        static List<int> bodyYPositions = new List<int>();
        static bool isButtonPressed = false;
        static Random random = new Random();
        
        static void Main(string[] args)
        {
            //Console.WindowHeight = 16;
            //Console.WindowWidth = 32;
  
            isGameOver = false;
            isButtonPressed = false;
            
            int berryXPosition = random.Next(0, windowWidth);
            int berryYPosition = random.Next(0, windowHeight);
            
            head =  new Pixel(windowWidth/2, windowHeight/2, ConsoleColor.Red);
            
            while (isGameOver != true)
            {
                Console.Clear();
                GameOverCheck();
                DrawBorders(windowHeight, windowWidth);
                if (berryXPosition == head.Xpos && berryYPosition == head.Ypos)
                {
                    score++;
                    berryXPosition = random.Next(1, windowWidth-2);
                    berryYPosition = random.Next(1, windowHeight-2);
                } 
                DrawSnake();
                DrawBerry(berryXPosition, berryYPosition);
                isButtonPressed = false;
                HandleInput();
                MoveSnake();
                Task.Delay(100).Wait();
            }

            ShowEndgameScreen();
        }

        private static void MoveSnake()
        {
            bodyXPositions.Add(head.Xpos);
            bodyYPositions.Add(head.Ypos);
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
            if (bodyXPositions.Count() > score)
            {
                bodyXPositions.RemoveAt(0);
                bodyYPositions.RemoveAt(0);
            }
        }

        public static void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo toets = Console.ReadKey(true);
                if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != Direction.Down && isButtonPressed == false)
                {
                    movement = Direction.Up;
                    isButtonPressed = true;
                }
                if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != Direction.Up && isButtonPressed == false)
                {
                    movement = Direction.Down;
                    isButtonPressed = true;
                }
                if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != Direction.Right && isButtonPressed == false)
                {
                    movement = Direction.Left;
                    isButtonPressed = true;
                }
                if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != Direction.Left && isButtonPressed == false)
                {
                    movement = Direction.Right;
                    isButtonPressed = true;
                }
            }
        }

        private static void GameOverCheck()
        {
            if (head.Xpos == windowWidth-1 || head.Xpos == 0 ||head.Ypos == windowHeight-1 || head.Ypos == 0)
            { 
                isGameOver = true;
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
            
            for (int i = 0; i < bodyXPositions.Count(); i++)
            {
                Console.SetCursorPosition(bodyXPositions[i], bodyYPositions[i]);
                Console.Write("■");
                if (bodyXPositions[i] == head.Xpos && bodyYPositions[i] == head.Ypos)
                {
                    isGameOver = true;
                }
            }
            
            Console.SetCursorPosition(head.Xpos, head.Ypos);
            Console.ForegroundColor = head.Color;
            Console.Write("■");
        }

        public static void DrawBerry(int x, int y)
        {
            DrawPixel(x,y, ConsoleColor.Cyan);
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
