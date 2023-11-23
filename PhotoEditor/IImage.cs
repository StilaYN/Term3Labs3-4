namespace PhotoEditor;

public interface IImage
{
    IPixel[,] Pixels { get; set; }
    int Width { get;}
    int Height { get;}
}