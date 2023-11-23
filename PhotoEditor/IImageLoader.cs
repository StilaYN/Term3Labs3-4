namespace PhotoEditor;

public interface IImageLoader
{
    IImage Load(string path);
}