using System;

namespace PhotoEditor;

public class Image:IImage
{
    private IPixel[,] _pixels;

    public Image(IPixel[,] pixels)
    {
        this._pixels = pixels;
    }

    public IPixel[,] Pixels
    {
        get => _pixels;
        set => _pixels = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Width => _pixels.GetLength(1);
    public int Height => _pixels.GetLength(0);
}