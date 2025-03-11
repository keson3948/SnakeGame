namespace Snake;

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