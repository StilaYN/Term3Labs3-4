namespace PhotoEditor;

public interface IImageLoader
{
    Pixel[,] Load(string path);
}