namespace PhotoEditor;

public interface IImageSaver
{
    void Save(IImage image, string path);
}