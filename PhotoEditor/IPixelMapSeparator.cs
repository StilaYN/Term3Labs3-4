namespace PhotoEditor;

public interface IPixelMapSeparator
{
    (byte[] date, int width, int height,int bytePerPixel) Separate(IPixel[,] pixelMap);
}