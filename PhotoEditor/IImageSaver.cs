namespace PhotoEditor;

public interface IImageSaver
{
    void Save(IImage image, int dpiX, int dpiY, string path);
}