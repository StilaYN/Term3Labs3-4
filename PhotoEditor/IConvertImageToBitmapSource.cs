using System.Windows.Media.Imaging;

namespace PhotoEditor;

interface IConvertImageToBitmapSource
{
    BitmapSource ToBitmapSource(IImage image, int dpiX, int dpiY);
}