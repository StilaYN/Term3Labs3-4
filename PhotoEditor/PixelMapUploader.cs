using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoEditor;

public class PixelMapUploader:IPixelMapUploader
{
    public BitmapSource Upload(byte[] date, int width, int height, int dpiX, int dpiY)
    {
        List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
        colors.Add(System.Windows.Media.Colors.Red);
        colors.Add(System.Windows.Media.Colors.Blue);
        colors.Add(System.Windows.Media.Colors.Green);
        BitmapPalette myPalette = new BitmapPalette(colors);
        int stride = (int)(width * ((PixelFormats.Bgr32.BitsPerPixel) / 8));
        BitmapSource source = BitmapSource.Create(width,height,dpiX,dpiY,PixelFormats.Bgr32,myPalette,date,stride);
        return source;
    }
}