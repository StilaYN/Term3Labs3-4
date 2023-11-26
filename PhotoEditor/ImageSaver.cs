using System.Windows.Media.Imaging;

namespace PhotoEditor;

public class ImageSaver:IImageSaver
{
    public IPixelMapSeparator Separator { get; set; }
    public IPixelMapUploader Uploader { get; set; }
    public IPixelMapSaveToFile Saver { get; set; }
    public ImageSaver(IPixelMapSeparator separator, IPixelMapUploader uploader, IPixelMapSaveToFile saver)
    {
        this.Separator = separator;
        this.Uploader = uploader;
        this.Saver = saver;
    }

    public void Save(IImage image, int dpiX, int dpiY, string path)
    {
        (byte[] date, int width, int height, int bytePerPixel) = Separator.Separate(image.Pixels);
        BitmapSource source =Uploader.Upload(date, width, height, dpiX, dpiY);
        Saver.Save(source,path);
    }
}