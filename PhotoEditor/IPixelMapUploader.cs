using System.Windows.Media.Imaging;

namespace PhotoEditor;

public interface IPixelMapUploader
{
    BitmapSource Upload(byte[] date, int width, int height, int dpiX, int dpiY);
}