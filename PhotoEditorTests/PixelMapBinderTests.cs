using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditor.Tests
{
    [TestClass()]
    public class PixelMapBinderTests
    {
        class TestPixel : IPixel
        {
            public byte Red { get; set; }
            public byte Green { get; set; }
            public byte Blue { get; set; }
            public byte Alpha { get; set; }
            public TestPixel(byte red, byte green, byte blue, byte alpha)
            {
                Red = red;
                Green = green;
                Blue = blue;
                Alpha = alpha;
            }
        }
        [TestMethod()]
        public void CreateTest()

        {
            byte[] data = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39 };
            IPixel [,] expectedDate = new IPixel[5,2];
            int width = 2;
            int height = 5;
            expectedDate[0, 0] = new TestPixel(0, 1, 2, 3);
            expectedDate[0, 1] = new TestPixel(4, 5, 6, 7);

            expectedDate[1, 0] = new TestPixel(8, 9, 10, 11);
            expectedDate[1, 1] = new TestPixel(12, 13, 14, 15);
            
            expectedDate[2, 0] = new TestPixel(16, 17, 18, 19);
            expectedDate[2, 1] = new TestPixel(20, 21, 22, 23);

            expectedDate[3, 0] = new TestPixel(24, 25, 26, 27);
            expectedDate[3, 1] = new TestPixel(28, 29, 30, 31);

            expectedDate[4, 0] = new TestPixel(32, 33, 34, 35);
            expectedDate[4, 1] = new TestPixel(36, 37, 38, 39);

            var pixelMapBinder = new PixelMapBinder();
            IPixel[,] receivedData = pixelMapBinder.Create(data,width,height);
            for (int i = 0; i < expectedDate.GetLength(0); i++)
            {
                for (int j = 0; j < expectedDate.GetLength(1); j++)
                {
                    var exp = expectedDate[i,j];
                    var rec = receivedData[i,j];
                    if (exp.Red == rec.Red && exp.Blue == rec.Blue && exp.Green == rec.Green &&
                        exp.Alpha == rec.Alpha) continue;
                    else Assert.Fail();
                    
                }
            }
        }
    }
}