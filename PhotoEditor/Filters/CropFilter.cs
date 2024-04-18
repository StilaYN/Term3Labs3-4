using System;

namespace PhotoEditor.Filters;

public class CropFilter:IFilter
{
    public int StartX { get; set; }
    public int StartY { get; set; }
    public int NewWidth { get; set; }
    public int NewHeight { get; set; }

    public CropFilter(int startX, int startY, int newWidth, int newHeight)
    {
        StartX = startX;
        StartY = startY;
        NewWidth = newWidth;
        NewHeight = newHeight;
    }

    public IImage ApplyFilter(IImage? image)
    {
        if (image != null)
        {
            int height = image.Pixels.GetLength(0);
            int width = image.Pixels.GetLength(1);
            // Проверка на допустимые значения координат и размеров
            if (StartX < 0 || StartY < 0 || StartX + NewWidth > width || StartY + NewHeight > height)
            {
                throw new ArgumentException("Invalid crop coordinates or dimensions");
            }

            IPixel[,] croppedPixels = new IPixel[NewHeight, NewWidth];

            for (int y = 0; y < NewHeight; y++)
            {
                for (int x = 0; x < NewWidth; x++)
                {
                    croppedPixels[y, x] = image.Pixels[StartY + y, StartX + x];
                }
            }

            return new Image(croppedPixels);
        }
        else return null;
    }
}