using System;

namespace PhotoEditor;

public class Image:IImage
{
    private IPixel[,] _pixels;

    public Image(IPixel[,] pixels)
    {
        this._pixels = pixels;
    }

    public Image(int widght,int height)
    {
        _pixels=new Pixel[widght,height];
    }

    public IPixel[,] Pixels
    {
        get => _pixels;
        set => _pixels = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Width => _pixels.GetLength(0);
    public int Height => _pixels.GetLength(1);
}