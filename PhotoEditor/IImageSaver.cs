namespace PhotoEditor;

public interface IImageSaver
{
    void Save(IImage image, float dpiX, float dpiY, string path);
}