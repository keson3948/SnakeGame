namespace Snake
{
    static class Renderer
    {
        public static void DrawBorders(int windowHeight, int windowWidth)
        {
            Console.Clear();

            for (int i = 0; i < windowWidth; i++)
            {
                DrawPixel(i, 0);
                DrawPixel(i, windowHeight - 1);
            }

            for (int i = 0; i < windowHeight; i++)
            {
                DrawPixel(0, i);
                DrawPixel(windowWidth - 1, i);
            }
        }

        public static void DrawSnake(Snake snake)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var part in snake.Body)
            {
                Console.SetCursorPosition(part.Xpos, part.Ypos);
                Console.Write("■");
            }

            Console.SetCursorPosition(snake.Head.Xpos, snake.Head.Ypos);
            Console.ForegroundColor = snake.Head.Color;
            Console.Write("■");
        }

        public static void DrawBerry(Berry berry)
        {
            DrawPixel(berry.Xpos, berry.Ypos, berry.Color);
        }

        private static void DrawPixel(int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write("■");
        }
    }
}