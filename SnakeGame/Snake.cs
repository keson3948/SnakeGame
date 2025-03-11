namespace Snake

{
    class Snake
    {
        public List<Pixel> Body { get; private set; }
        public Pixel Head { get; private set; }

        public Snake(Pixel head)
        {
            Head = head;
            Body = new List<Pixel>();
        }

        public void Move(Direction direction)
        {
            Body.Add(new Pixel(Head.Xpos, Head.Ypos, ConsoleColor.Green));

            switch (direction)
            {
                case Direction.Up:
                    Head.Ypos--;
                    break;
                case Direction.Down:
                    Head.Ypos++;
                    break;
                case Direction.Left:
                    Head.Xpos--;
                    break;
                case Direction.Right:
                    Head.Xpos++;
                    break;
            }

            if (Body.Count > 5) // Základní délka hada
            {
                Body.RemoveAt(0);
            }
        }

        public void Grow()
        {
            Body.Add(new Pixel(Head.Xpos, Head.Ypos, ConsoleColor.Green));
        }

        public bool IsCollision(int windowWidth, int windowHeight)
        {
            if (Head.Xpos == 0 || Head.Xpos == windowWidth - 1 || Head.Ypos == 0 || Head.Ypos == windowHeight - 1)
            {
                return true;
            }

            foreach (var part in Body)
            {
                if (part.Xpos == Head.Xpos && part.Ypos == Head.Ypos)
                {
                    return true;
                }
            }

            return false;
        }
    }
}