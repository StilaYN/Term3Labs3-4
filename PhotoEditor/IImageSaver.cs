namespace PhotoEditor;

public interface IImageSaver
{
    void Save(Pixel[,] image, string path);
}