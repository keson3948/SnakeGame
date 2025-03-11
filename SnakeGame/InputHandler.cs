namespace Snake;

static class InputHandler
{
    public static void HandleInput(ref Direction movement)
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
}