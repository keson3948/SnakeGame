namespace Snake
{
    class Game
    {
        private Random random = new Random();
        private int windowWidth = Console.WindowWidth;
        private int windowHeight = Console.WindowHeight;

        private int score = 5;
        private bool isGameOver;
        private Direction movement = Direction.Right;

        private Pixel head;
        private Berry berry;
        private Snake snake;

        public Game()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            head = new Pixel(windowWidth / 2, windowHeight / 2, ConsoleColor.Red);
            berry = new Berry(random.Next(1, windowWidth - 2), random.Next(1, windowHeight - 2));
            snake = new Snake(head);
        }

        public void Run()
        {
            while (!isGameOver)
            {
                Renderer.DrawBorders(windowHeight, windowWidth);
                Renderer.DrawSnake(snake);
                Renderer.DrawBerry(berry);
                InputHandler.HandleInput(ref movement);
                snake.Move(movement);
                CheckCollisions();
                Task.Delay(100).Wait();
            }

            ShowEndgameScreen();
        }

        private void CheckCollisions()
        {
            if (snake.IsCollision(windowWidth, windowHeight))
            {
                isGameOver = true;
            }

            if (berry.Xpos == head.Xpos && berry.Ypos == head.Ypos)
            {
                score++;
                berry = new Berry(random.Next(1, windowWidth - 2), random.Next(1, windowHeight - 2));
                snake.Grow();
            }
        }

        private void ShowEndgameScreen()
        {
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 + 1);
        }
    }
}