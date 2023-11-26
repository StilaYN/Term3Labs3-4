using System.Windows.Media.Imaging;

namespace PhotoEditor;

public class ConvertImageToBitmapSource:IConvertImageToBitmapSource
{
    public IPixelMapSeparator Separator { get; set; } 
    public IPixelMapUploader Uploader { get; set; }
    public ConvertImageToBitmapSource(IPixelMapSeparator separator, IPixelMapUploader uploader)
    {
        Separator = separator;
        Uploader = uploader;
    }

    public BitmapSource ToBitmapSource(IImage image, int dpiX, int dpiY)
    {
        (byte[] data, int width, int height, int bytePerPixel) = Separator.Separate(image.Pixels);
        BitmapSource source = Uploader.Upload(data, width, height, dpiX, dpiY);
        return source;
    }
}
