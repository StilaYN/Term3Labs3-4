using System.Windows.Media.Imaging;

namespace PhotoEditor;

public interface IPixelMapSaveToFile
{
    void Save(BitmapSource source, string path);
}