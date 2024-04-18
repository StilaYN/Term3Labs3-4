using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PhotoEditor.Commands;
using PhotoEditor.Filters;
using PhotoEditor.MainLogic;

namespace PhotoEditor.ModelView;

public class PhotoEditorMainModelView:INotifyPropertyChanged
{
    private IImageProcessor? _imageProcessor;
    private ICommandExecutor<IProgramCommand> _executor;
    private int _width;
    private int _height;
    private ObservableCollection<string> _layersName;
    private BitmapSource _bitmap;
    private IConvertImageToBitmapSource _converter;
    private string _selectedLayerName;
    private int _step;
    private bool _isCutUsed;
    private bool _isScaleUsed;
    private Frame _cutFrame;
    private Frame _scaleFrame;
    private Visibility _isCutVisibility;
    private Visibility _isScaleVisibility;
    public int CutFrameWidth

    {
        get
        {
            return _cutFrame.Width; 
        }
        set
        {
            if (value > 0)
            {
                
                _cutFrame.ResizeFrame(value,_cutFrame.Height);
                OnPropertyChanged("CutFrameWidth");
            }
        }
    }
    public int CutFrameHeight
    {
        get
        {
            return _cutFrame.Height;
        }
        set
        {
            if (value > 0)
            {
                _cutFrame.ResizeFrame(_cutFrame.Width,value);
                OnPropertyChanged("CutFrameHeight");
            }
        }
    }
    public int ScaleFrameWidth
    {
        get
        {
            return _scaleFrame.Width;
        }
        set
        {
            if (value > 0)
            {
                _scaleFrame.ResizeFrame(value, _scaleFrame.Height);
                OnPropertyChanged("ScaleFrameWidth");
            }
        }
    }

    public int ScaleFrameHeight
    {
        get
        {
            return _scaleFrame.Height;
        }
        set
        {
            if (value > 0)
            {
                _scaleFrame.ResizeFrame(_scaleFrame.Width, value);
                OnPropertyChanged("ScaleFrameHeight");
            }
        }
    }

    public bool IsCutUsed
    {
        get
        {
            return _isCutUsed;
        }
        set
        {
            _isCutUsed = value;
            OnPropertyChanged("IsCutUsed");
        }
    }

    public bool IsScaleUsed
    {
        get
        {
            return _isScaleUsed;
        }
        set
        {
            _isScaleUsed = value;
            OnPropertyChanged("IsScaleUsed");
        }
    }

    public Visibility IsCutVisibility
    {
        get { return _isCutVisibility; }
        set
        {
            _isCutVisibility = value;
            OnPropertyChanged("IsCutVisibility");
        }
    }

    public Visibility IsScaleVisibility
    {
        get { return _isScaleVisibility; }
        set
        {
            _isScaleVisibility = value;
            OnPropertyChanged("IsScaleVisibility");
        }
    }


    public PhotoEditorMainModelView(int width,int height)
    {
        _imageProcessor = new ImageProcessor(width, height);
        _width = width;
        _height = height;
        _executor = new AbstractCommandExecutor<IProgramCommand>(40);
        _layersName = new ObservableCollection<string>();
        _layersName.Add("default");
        _imageProcessor.PropertyChanged += ImageProcessorChangedEventHandler;
        _converter = new ConvertImageToBitmapSource(new PixelMapSeparator(), new PixelMapUploader());
        _bitmap = _converter.ToBitmapSource(_imageProcessor.ResultImage, 96, 96);
        PropertyChanged += PhotoEditorMVEventHandler;
        _step = 25;
        _cutFrame = new Frame(width,height, new Pixel(0,0,255,255));
        _cutFrame.PropertyChanged += PhotoEditorMVEventHandler;
        _isCutUsed = false;
        _isCutVisibility = Visibility.Hidden;
        _scaleFrame = new Frame(width, height, new Pixel(255, 0, 0, 255));
        _scaleFrame.PropertyChanged += PhotoEditorMVEventHandler;
        _isScaleUsed = false;
        _isScaleVisibility = Visibility.Hidden;
    }

    public BitmapSource BitMap
    {
        get
        {
            return _bitmap;
        }
        set
        {
            _bitmap = value;
            OnPropertyChanged("BitMap");
        }
    }

    public string FindLoadPath()
    {
        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
        openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";

        if (openFileDialog.ShowDialog() == true)
        {
            string imagePath = openFileDialog.FileName;
            return imagePath;
        }
        return null;
    }

    public string FindSavePath()
    {
        Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
        saveFileDialog.Filter = "Изображение|*.jpg;";
        if (saveFileDialog.ShowDialog() == true)
        {
            string imagePath = saveFileDialog.FileName;
            return imagePath;
        }
        return null;
    }

    public ObservableCollection<string> LayersName
    {
        get
        {
            return _layersName;
        }
        set
        {
            _layersName = value;
            OnPropertyChanged("LayersName");
        }
    }

    public string SelectedLayerName
    {
        get
        {
            return _selectedLayerName;
        }
        set
        {
            if (value != _selectedLayerName)
            {
                _selectedLayerName = value;
                OnPropertyChanged("SelectedLayerName");
            }
        }

    }

    public ICommand LoadImage => new CommandTest(LoadImageMethod);
    public ICommand UseSepia => new CommandTest(UseSepiaMethod);
    public ICommand SaveImage => new CommandTest(SaveImageMethod);
    public ICommand AddLayer => new CommandTest(AddLayerMethod);
    public ICommand RemoveLayer => new CommandTest(RemoveLayerMethod);
    public ICommand Undo => new CommandTest(UndoMethod);
    public ICommand MoveUp => new CommandTest(MoveUpMethod);
    public ICommand MoveDown => new CommandTest(MoveDownMethod);
    public ICommand MoveLeft => new CommandTest(MoveLeftMethod);
    public ICommand MoveRight => new CommandTest(MoveRightMethod);
    public ICommand SwitchCut => new CommandTest(SwitchCutMethod);
    public ICommand SwitchScale => new CommandTest(SwitchScaleMethod);
    public ICommand DefaultFrame => new CommandTest(DefaultCutFrameMethod);
    public ICommand DefaultScaleFrame => new CommandTest(DefaultScaleFrameMethod);
    public ICommand DoCut => new CommandTest(DoCutMethod);
    public ICommand DoScale => new CommandTest(DoScaleMethod);

    private void DoScaleMethod(object? sender, EventArgs e)
    {
        double scaleX = (double)ScaleFrameWidth / _imageProcessor.CurrentLayer.ResultImage.Width;
        double scaleY = (double)ScaleFrameHeight/_imageProcessor.CurrentLayer.ResultImage.Height;
        ImageScaler imageScaler = new ImageScaler(scaleY, scaleX);
        _executor.Execute(new UseScaleFilter(imageScaler, _imageProcessor.CurrentLayer));
        _executor.Execute(new MoveLayer(_imageProcessor.CurrentLayer, 0, 0));
    }

    private void SwitchScaleMethod(object? sender, EventArgs e)
    {
        IsScaleUsed = !IsScaleUsed;
    }
    private void DoCutMethod(object? sender, EventArgs e)
    {
        int curX = 0;
        int curY = 0;
        int frameX = _cutFrame.X - _imageProcessor.CurrentLayer.X;
        int frameY = _cutFrame.Y - _imageProcessor.CurrentLayer.Y;
        int curWidth = _imageProcessor.CurrentLayer.ResultImage.Width;
        int curHeight = _imageProcessor.CurrentLayer.ResultImage.Height;
        int frameWidth = _cutFrame.Width;
        int frameHeight = _cutFrame.Height;
        int leftUpY = int.Max(curY,frameY);
        int leftUpX = int.Max(curX,frameX);
        int rightDownY = int.Min(curY+curHeight,frameY+frameHeight);
        int rightDownX = int.Min(curX+curWidth,frameX+frameWidth);
        int width = rightDownX - leftUpX;
        int height = rightDownY - leftUpY;
        CropFilter cropFilter = new CropFilter(leftUpX,leftUpY,width,height);
        _executor.Execute(new UseCropFilter(cropFilter,_imageProcessor.CurrentLayer));
        _executor.Execute(new MoveLayer(_imageProcessor.CurrentLayer,0,0));
    }

    private void DefaultCutFrameMethod(object? sender, EventArgs e)
    {
        _cutFrame.MoveFrame(0,0);
        CutFrameHeight = _height;
        CutFrameWidth = _width;
    }
    private void DefaultScaleFrameMethod(object? sender, EventArgs e)
    {
        _scaleFrame.MoveFrame(0, 0);
        ScaleFrameHeight = _height;
        ScaleFrameWidth = _width;
    }

    private void SwitchCutMethod(object? sender, EventArgs e)
    {
        IsCutUsed = !IsCutUsed;
    }


    private void MoveRightMethod(object? sender, EventArgs e)
    {
        if (IsCutUsed)
        {
            _cutFrame.MoveFrame(_cutFrame.X+Step,_cutFrame.Y);
        }
        else
        {
            ILayer layer = _imageProcessor.CurrentLayer;
            if (layer != null) _executor.Execute(new MoveLayer(layer, layer.X + Step, layer.Y));
        }
    }

    private void MoveLeftMethod(object? sender, EventArgs e)
    {
        if (IsCutUsed)
        {
            _cutFrame.MoveFrame(_cutFrame.X - Step, _cutFrame.Y);
        }
        else
        {
            ILayer layer = _imageProcessor.CurrentLayer;
            if (layer != null) _executor.Execute(new MoveLayer(layer, layer.X - Step, layer.Y));
        }
    }

    private void MoveDownMethod(object? sender, EventArgs e)
    {
        if (IsCutUsed) { _cutFrame.MoveFrame(_cutFrame.X , _cutFrame.Y+Step); }
        else
        {
            ILayer layer = _imageProcessor.CurrentLayer;
            if (layer != null) _executor.Execute(new MoveLayer(layer, layer.X, layer.Y + Step));
        }
    }


    private void MoveUpMethod(object? sender, EventArgs e)
    {
        if (IsCutUsed) { _cutFrame.MoveFrame(_cutFrame.X, _cutFrame.Y-Step); }
        else
        {
            ILayer layer = _imageProcessor.CurrentLayer;
            if (layer != null) _executor.Execute(new MoveLayer(layer, layer.X, layer.Y - Step));
        }
    }

    public int Step
    {
        get { return _step; }
        set
        {
            if (value > 0) _step = value;
            OnPropertyChanged("Step");
        }

    }

    private void RemoveLayerMethod(object? sender, EventArgs e)
    {
        string name = SelectedLayerName;
        _selectedLayerName = null;
        _executor.Execute(new RemoveLayer(_imageProcessor, name));
    }

    private void UndoMethod(object? sender, EventArgs e)
    {
        _executor.Undo();
    }


    private void AddLayerMethod(object? s, EventArgs args)
    {
        EnterNameWindow window = new EnterNameWindow();
        window.InitializeComponent();
        window.Owner = App.Current.MainWindow;
        EnterNameWindowMV nameMV = new EnterNameWindowMV(window, _imageProcessor.BusyLayersName);
        window.DataContext = nameMV;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.ShowDialog();
        if (nameMV.CanGetName)
        {
            string name = nameMV.Name;
            _executor.Execute(new AddLayer(_imageProcessor,name));
        }


        //_executor.Execute(new AddLayer(_imageProcessor,));
    }
    private void UseSepiaMethod(object? s, EventArgs args)
    {
        _executor.Execute(new UseColorFilter(new SepiaFilter(),_imageProcessor.CurrentLayer));
    }
    private void LoadImageMethod(object? s, EventArgs args)
    {
        _executor.Execute(new AddImage(_imageProcessor.CurrentLayer,FindLoadPath(), new ImageLoader(new PixelMapLoader(), new PixelMapBinder())));
    }

    private void SaveImageMethod(object? s, EventArgs args)
    {
        SaveWindow window = new SaveWindow();
        window.InitializeComponent();
        window.Owner = App.Current.MainWindow;
        SaveWindowMV saveMv = new SaveWindowMV(window);
        window.DataContext = saveMv;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.ShowDialog();
        if (saveMv.CanSave)
        {
            float dpiX = saveMv.DpiX;
            float dpiY = saveMv.DpiY;
            string path = FindSavePath();
            if (path != null)
            {
                ImageSaver imageSaver = new ImageSaver(new PixelMapSeparator(), new PixelMapUploader(),
                    new PixelMapSaverToJpg());
                IImage image = _imageProcessor.ResultImage;
                imageSaver.Save(image,dpiX,dpiY,path);
            }
        }

    }

    private void ImageProcessorChangedEventHandler(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName== "LayerImage") BitMap = _converter.ToBitmapSource(_imageProcessor.ResultImage, 96, 96);
        if (e.PropertyName == "LayersList")
        {
            BitMap = _converter.ToBitmapSource(_imageProcessor.ResultImage, 96, 96);
            var layersName = new ObservableCollection<string>();
            foreach (var i in _imageProcessor.Layers)
            {
                layersName.Add(i.Name);
            }
            LayersName = layersName;
        }
        if (e.PropertyName == "LayerPosition") BitMap = _converter.ToBitmapSource(_imageProcessor.ResultImage, 96, 96);

    }

    private void PhotoEditorMVEventHandler(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "SelectedLayerName")
        {
            _imageProcessor.SetCurrentLayer(_selectedLayerName);
        }

        if (e.PropertyName == "IsScaleUsed"&&IsScaleUsed)
        {
            IsCutUsed = false;
            IsScaleVisibility = (IsScaleUsed)? Visibility.Visible: Visibility.Hidden;
            IImage image = _scaleFrame.GetResultImage(_imageProcessor.ResultImage);
            BitMap = _converter.ToBitmapSource(image, 96, 96);
        }

        if (e.PropertyName == "IsScaleUsed" && !IsScaleUsed)
        {
            DefaultScaleFrameMethod(this,new EventArgs());
            IsScaleVisibility = (IsScaleUsed) ? Visibility.Visible : Visibility.Hidden;
            BitMap = _converter.ToBitmapSource(_imageProcessor.ResultImage, 96, 96);
        }

        if (e.PropertyName == "IsCutUsed" && IsCutUsed)
        {
            IsScaleUsed = false;
            IsCutVisibility = (IsCutUsed) ? Visibility.Visible : Visibility.Hidden;
            IImage image = _cutFrame.GetResultImage(_imageProcessor.ResultImage);
            BitMap = _converter.ToBitmapSource(image, 96, 96);
        }
        if (e.PropertyName == "IsCutUsed" && !IsCutUsed)
        {
            DefaultCutFrameMethod(this,new EventArgs());
            IsCutVisibility = (IsCutUsed) ? Visibility.Visible : Visibility.Hidden;
            BitMap = _converter.ToBitmapSource(_imageProcessor.ResultImage, 96, 96);
            // _frame.GetResultImage(_imageProcessor.ResultImage);
        }

        if (e.PropertyName == "Frame" && IsCutUsed)
        {
            IImage image = _cutFrame.GetResultImage(_imageProcessor.ResultImage);
            BitMap = _converter.ToBitmapSource(image, 96, 96);
        }
        if (e.PropertyName == "Frame" && IsScaleUsed)
        {
            IImage image = _scaleFrame.GetResultImage(_imageProcessor.ResultImage);
            BitMap = _converter.ToBitmapSource(image, 96, 96);
        }

    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    
}