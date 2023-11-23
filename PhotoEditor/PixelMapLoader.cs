using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoEditor;

public class PixelMapLoader:IPixelMapLoader
{
    public (byte[], int,int) Load(string path)
    {
        BitmapSource bitmap = new BitmapImage(new Uri(path));
        int width = bitmap.PixelWidth;
        int height = bitmap.PixelHeight;
        int stride = (int)(width * ((bitmap.Format.BitsPerPixel) / 8));
        var buffer = new byte[stride * height];
        bitmap.CopyPixels(buffer,stride,0);
        return (buffer,width,height);
    }   
}   