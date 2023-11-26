using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PhotoEditor;

public class PixelMapSeparator : IPixelMapSeparator
{
    public (byte[] date, int width, int height, int bytePerPixel) Separate(IPixel[,] pixelMap)
    {
        int bytePerPixel = 4;
        int height = pixelMap.GetLength(0);
        int width = pixelMap.GetLength(1);
        byte[] date = new byte[height * width * bytePerPixel];
        Task task = SeparateAsync(pixelMap, date, height);
        task.Wait();
        return (date, width, height, bytePerPixel);
    }

    private async Task SeparateAsync(IPixel[,] pixels, byte[] buffer, int height)
    {
        int numberOfAsync = 10;
        int unitHeight = height / numberOfAsync;
        for (int i = 0; i < numberOfAsync; i++)
        {
            if (i != numberOfAsync - 1)
                await SeparatePartOfBitmap(pixels, buffer, i * unitHeight, (i + 1) * unitHeight);
            else await SeparatePartOfBitmap(pixels, buffer, i * unitHeight, height);
        }
    }

    private async Task SeparatePartOfBitmap(IPixel[,] pixels, byte[] buffer, int startHeight, int endHeight)
    {
        for (int i = startHeight; i < endHeight; i++)
        {
            for (int j = 0; j < pixels.GetLength(1); j++)
            {
                int indexInBuffer = i * pixels.GetLength(1) * 4 + j * 4;
                buffer[indexInBuffer] = pixels[i, j].Red;
                buffer[indexInBuffer + 1] = pixels[i, j].Green;
                buffer[indexInBuffer + 2] = pixels[i, j].Blue;
                buffer[indexInBuffer + 3] = pixels[i, j].Alpha;
            }
        }
    }
}