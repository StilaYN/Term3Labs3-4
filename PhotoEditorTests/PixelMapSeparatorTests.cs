using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoEditor;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoEditor.Tests
{
    [TestClass()]
    public class PixelMapSeparatorTests
    {
        public class TestPixelMapLoader : IPixelMapLoader
        {
            public (byte[], int, int) Load(string path)
            {
                var bitmap = new BitmapImage(new Uri(path,UriKind.Relative));

            var width = bitmap.PixelWidth;
                var height = bitmap.PixelHeight;
                int stride = (int)(width * ((bitmap.Format.BitsPerPixel) / 8));
                var buffer = new byte[stride * height];
                bitmap.CopyPixels(buffer, stride, 0);
                return (buffer, width, height);
            }
        }
        [TestMethod()]
        public void SeparateTest()
        {
            string path = "SeparatorTest.jpg";
            IPixelMapLoader pixelMapLoader = new TestPixelMapLoader();
            (byte[] expectedDate,int width,int height) = pixelMapLoader.Load(path);
            IPixelMapBinder pixelMapBinder = new PixelMapBinder();
            IPixel[,] pixels = pixelMapBinder.Create(expectedDate, width, height);
            IPixelMapSeparator pixelMapSeparator = new PixelMapSeparator();
            (byte[] receivedDate,int width1,int height1 ,int bytePerPixel ) = pixelMapSeparator.Separate(pixels);
            if(width!=width1||height!=height1) Assert.Fail();
            for (int i = 0; i < width; i++)
            {
                if (expectedDate[i] != receivedDate[i]) Assert.Fail();
                else continue;
            }
            Assert.AreEqual(true, true);
        }
    }
}