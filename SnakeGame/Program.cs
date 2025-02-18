using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WindowHeight = 16;
            //Console.WindowWidth = 32;
            
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;
            
            Random random = new Random();
            
            int score = 5;
            bool isGameOver = false;
            
            Pixel head = new Pixel(windowWidth/2, windowHeight/2, ConsoleColor.Red);
            
            string movement = "RIGHT";
            
            List<int> bodyXPositions = new List<int>();
            List<int> bodyYPositions = new List<int>();
            
            int berryXPosition = random.Next(0, windowWidth);
            int berryYPosition = random.Next(0, windowHeight);
            
            DateTime startTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            
            string buttonpressed = "no";
            
            
            while (true)
            {
                Console.Clear();
                if (head.Xpos == windowWidth-1 || head.Xpos == 0 ||head.Ypos == windowHeight-1 || head.Ypos == 0)
                { 
                    isGameOver = true;
                }
                for (int i = 0;i< windowWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < windowWidth; i++)
                {
                    Console.SetCursorPosition(i, windowHeight -1);
                    Console.Write("■");
                }
                for (int i = 0; i < windowHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < windowHeight; i++)
                {
                    Console.SetCursorPosition(windowWidth - 1, i);
                    Console.Write("■");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if (berryXPosition == head.Xpos && berryYPosition == head.Ypos)
                {
                    score++;
                    berryXPosition = random.Next(1, windowWidth-2);
                    berryYPosition = random.Next(1, windowHeight-2);
                } 
                for (int i = 0; i < bodyXPositions.Count(); i++)
                {
                    Console.SetCursorPosition(bodyXPositions[i], bodyYPositions[i]);
                    Console.Write("■");
                    if (bodyXPositions[i] == head.Xpos && bodyYPositions[i] == head.Ypos)
                    {
                        isGameOver = true;
                    }
                }
                if (isGameOver)
                {
                    break;
                }
                Console.SetCursorPosition(head.Xpos, head.Ypos);
                Console.ForegroundColor = head.Color;
                Console.Write("■");
                Console.SetCursorPosition(berryXPosition, berryYPosition);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                startTime = DateTime.Now;
                buttonpressed = "no";
                while (true)
                {
                    currentTime = DateTime.Now;
                    if (currentTime.Subtract(startTime).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        //Console.WriteLine(toets.Key.ToString());
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && buttonpressed == "no")
                        {
                            movement = "UP";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && buttonpressed == "no")
                        {
                            movement = "DOWN";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && buttonpressed == "no")
                        {
                            movement = "LEFT";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && buttonpressed == "no")
                        {
                            movement = "RIGHT";
                            buttonpressed = "yes";
                        }
                    }
                }
                bodyXPositions.Add(head.Xpos);
                bodyYPositions.Add(head.Ypos);
                switch (movement)
                {
                    case "UP":
                        head.Ypos--;
                        break;
                    case "DOWN":
                        head.Ypos++;
                        break;
                    case "LEFT":
                        head.Xpos--;
                        break;
                    case "RIGHT":
                        head.Xpos++;
                        break;
                }
                if (bodyXPositions.Count() > score)
                {
                    bodyXPositions.RemoveAt(0);
                    bodyYPositions.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 +1);
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
