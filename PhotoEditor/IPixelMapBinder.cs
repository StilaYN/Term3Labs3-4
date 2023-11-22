namespace PhotoEditor;

public interface IPixelMapBinder
{
    Pixel[,] Create(byte[] buffer, int width, int height);
}