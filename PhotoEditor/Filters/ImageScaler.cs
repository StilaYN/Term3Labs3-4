using System;

namespace PhotoEditor.Filters;

public class ImageScaler
{
    public double ScaleX { get; set; }
    public double ScaleY { get; set; }
    public ImageScaler(double scaleX, double scaleY)
    {
        ScaleX = scaleX;
        ScaleY = scaleY;
    }

    public IImage ApplyFilter(IImage image)
    {
        int width = image.Pixels.GetLength(0);
        int height = image.Pixels.GetLength(1);
        int newWidth = (int)(width * ScaleX);
        int newHeight = (int)(height * ScaleY);

        IPixel[,] scaledPixels = new IPixel[newWidth, newHeight];

        for (int x = 0; x < newWidth; x++)
        {
            for (int y = 0; y < newHeight; y++)
            {
                double originalX = x / ScaleX;
                double originalY = y / ScaleY;

                int x1 = (int)originalX;
                int x2 = Math.Min(x1 + 1, width - 1);
                int y1 = (int)originalY;
                int y2 = Math.Min(y1 + 1, height - 1);

                double deltaX = originalX - x1;
                double deltaY = originalY - y1;

                // Билинейная интерполяция короче
                IPixel interpolatedPixel = Interpolate(
                    image.Pixels[x1, y1],
                    image.Pixels[x2, y1],
                    image.Pixels[x1, y2],
                    image.Pixels[x2, y2],
                    deltaX, deltaY);

                scaledPixels[x, y] = interpolatedPixel;
            }
        }

        return new Image(scaledPixels);
    }

    private IPixel Interpolate(IPixel p1, IPixel p2, IPixel p3, IPixel p4, double x, double y)
    {
        double w1 = (1 - x) * (1 - y);
        double w2 = x * (1 - y);
        double w3 = (1 - x) * y;
        double w4 = x * y;

        byte red = (byte)(w1 * p1.Red + w2 * p2.Red + w3 * p3.Red + w4 * p4.Red);
        byte green = (byte)(w1 * p1.Green + w2 * p2.Green + w3 * p3.Green + w4 * p4.Green);
        byte blue = (byte)(w1 * p1.Blue + w2 * p2.Blue + w3 * p3.Blue + w4 * p4.Blue);
        byte alpha = (byte)(w1 * p1.Alpha + w2 * p2.Alpha + w3 * p3.Alpha + w4 * p4.Alpha);

        return new Pixel(red, green, blue, alpha);
    }


}
