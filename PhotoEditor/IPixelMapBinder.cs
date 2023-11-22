namespace PhotoEditor;

public interface IPixelMapBinder
{
    IPixel[,] Create(byte[] buffer, int width, int height);
}