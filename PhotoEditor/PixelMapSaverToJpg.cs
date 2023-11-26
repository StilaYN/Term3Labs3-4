using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PhotoEditor;

public class PixelMapSaverToJpg:IPixelMapSaveToFile
{
    public void Save(BitmapSource image, string path)
    {
        using (FileStream fstream = new FileStream(path, FileMode.Create))
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.FlipHorizontal = false;
            encoder.FlipVertical = false;
            encoder.QualityLevel = 30;
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(fstream);
        }
    }
}