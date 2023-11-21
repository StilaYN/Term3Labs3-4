namespace PhotoEditor;

public interface IImageSaver
{
    void Save(Image image, string path);
}