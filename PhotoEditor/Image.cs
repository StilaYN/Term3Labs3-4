using System;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PhotoEditor;

public class Image:IImage
{
    private IPixel[,] _pixels;

    public Image(IPixel[,] pixels)
    {
        this._pixels = pixels;
    }

    public Image(int width,int height)
    {
        _pixels=new IPixel[height,width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                _pixels[i, j] = new Pixel(255,255,255,0);
            }
        }
    }

    public IPixel[,] Pixels
    {
        get => _pixels;
        set => _pixels = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Width => _pixels.GetLength(1);
    public int Height => _pixels.GetLength(0);
    public object Clone()
    {
        Image image = new Image(Width, Height);
        Task task = CloneAsync(image);
        task.Wait();
        return image;
    }

    private async Task CloneAsync(Image image)
    {
        int numberOfAsync = 20;
        int unitHeight = image.Height / numberOfAsync;
        for (int i = 0; i < numberOfAsync; i++)
        {
            if (i != numberOfAsync - 1)
                await ClonePartAsync(image, i * unitHeight, (i + 1) * unitHeight);
            else await ClonePartAsync(image, i * unitHeight, image.Height);
        }
    }

    private async Task ClonePartAsync(Image image, int startHeight, int endHeight)
    {
        for (int i = startHeight; i < endHeight; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                IPixel pixel = _pixels[i, j];
                image.Pixels[i, j] = new Pixel(pixel.Red, pixel.Green, pixel.Blue, pixel.Alpha);
            }
        }
    }
}