using System;

namespace PhotoEditor;

public interface IImage:ICloneable
{
    IPixel[,] Pixels { get; set; }
    int Width { get;}
    int Height { get;}
}