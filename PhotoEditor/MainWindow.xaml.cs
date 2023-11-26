using PhotoEditor.Filters;
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
        Layer mainLayer;
        public MainWindow()
        {
            InitializeComponent();
            loader = new ImageLoader(new PixelMapLoader(), new PixelMapBinder());
            saver=new ImageSaver(new PixelMapSeparator(), new PixelMapUploader(),new PixelMapSaverToJpg());
            converter=new ConvertImageToBitmapSource(new PixelMapSeparator(),new PixelMapUploader());
            mainLayer = new Layer();

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
                mainLayer.Image = image;
                ImageArea.Source = converter.ToBitmapSource(mainLayer.Image, 300, 300);
            }
        }

        private void RotateButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SepiaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainLayer.ApplyFilter(new SepianFilter());
                ImageArea.Source = converter.ToBitmapSource(mainLayer.Image, 300, 300);
            }catch(Exception)
            {
                MessageBox.Show("Изображение пустое!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                saver.Save(mainLayer.Image, 300, 300, "result.jpg");
            }catch(Exception)
            {
                MessageBox.Show("Изображение пустое!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
