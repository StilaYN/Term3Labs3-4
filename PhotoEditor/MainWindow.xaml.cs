using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoEditor
{
   
    public partial class MainWindow : Window
    {
        IImageLoader loader;
        IImageSaver saver;
        IConvertImageToBitmapSource converter;
        public MainWindow()
        {
            InitializeComponent();
            loader = new ImageLoader(new PixelMapLoader(), new PixelMapBinder());
            saver=new ImageSaver(new PixelMapSeparator(), new PixelMapUploader(),new PixelMapSaverToJpg());
            converter=new ConvertImageToBitmapSource(new PixelMapSeparator(),new PixelMapUploader());

        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            IImageLoader loader = new ImageLoader(new PixelMapLoader(), new PixelMapBinder());
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                IImage image = loader.Load(imagePath);
            }
        }

        private void RotateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SepiaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
