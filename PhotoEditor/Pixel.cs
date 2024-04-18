using PhotoEditor.Commands;

namespace PhotoEditor;

public struct Pixel:IPixel
{
    public Pixel()
    {
        Red = 0;
        Green = 0;
        Blue = 0;
        Alpha = 0;
    }
    public Pixel(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }
    public byte Alpha { get; set; }
}