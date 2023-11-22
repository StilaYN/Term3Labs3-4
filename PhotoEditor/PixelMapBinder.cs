using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace PhotoEditor;

public class PixelMapBinder:IPixelMapBinder
{
    public Pixel[,] Create(byte[] buffer, int width, int height)
    {
        Pixel[,] pixels = new Pixel[height, width];
        Task task = CopyAsync(pixels, buffer, height);
        task.Wait();
        return pixels;
    }

    private async Task CopyAsync(Pixel[,] pixels, byte[] buffer,  int height)
    {
        int numberOfAsync = 5;
        int unitHeight = height / numberOfAsync;
        for (int i = 0; i < numberOfAsync; i++)
        {
            if(i!=numberOfAsync-1) await Task.Run(() => CopyPartOfPixels(pixels,buffer,i*unitHeight,(i+1)*unitHeight));
            else await Task.Run(() => CopyPartOfPixels(pixels, buffer, i * unitHeight, height));
        }
    }

    private void CopyPartOfPixels(Pixel[,] pixels, byte[] buffer, int startHeight, int endHeight)
    {
        for (int i = startHeight; i < endHeight; i++)
        {
            for (int j = 0; j < pixels.GetLength(1); j++)
            {
                int indexInBuffer = i* pixels.GetLength(1)*4+j*4;
                pixels[i, j] = new Pixel(buffer[indexInBuffer], buffer[indexInBuffer + 1], buffer[indexInBuffer + 2],
                    buffer[indexInBuffer + 3]);
            }
        }
    }
}