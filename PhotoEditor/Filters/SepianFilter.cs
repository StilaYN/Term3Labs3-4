using System;

namespace PhotoEditor.Filters
{

    public class SepianFilter: IFilter
    {
        public IImage ApplyFiler(IImage image) { 
            IImage result = new Image(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    IPixel originalPixel = image.Pixels[x, y];

                    // Вычисление новых значений цветов для сепии
                    byte sepiaRed = (byte)(0.393 * originalPixel.Red + 0.769 * originalPixel.Green + 0.189 * originalPixel.Blue);
                    byte sepiaGreen = (byte)(0.349 * originalPixel.Red + 0.686 * originalPixel.Green + 0.168 * originalPixel.Blue);
                    byte sepiaBlue = (byte)(0.272 * originalPixel.Red + 0.534 * originalPixel.Green + 0.131 * originalPixel.Blue);

                    // Ограничиваем значения до 255
                    sepiaRed = (byte)Math.Min((byte)255, sepiaRed);
                    sepiaGreen = (byte)Math.Min((byte)255, sepiaGreen);
                    sepiaBlue = (byte)Math.Min((byte)255, sepiaBlue);

                    IPixel sepiaPixel = new Pixel
                    (
                        sepiaRed,
                        sepiaGreen,
                        sepiaBlue,
                        originalPixel.Alpha
                    );

                    result.Pixels[x, y] = sepiaPixel;
                }
            }
            return result;
        }
    }
}
