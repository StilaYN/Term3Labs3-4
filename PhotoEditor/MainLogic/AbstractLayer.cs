using PhotoEditor.Filters;
using System.Collections.Generic;
using System.ComponentModel;

namespace PhotoEditor.MainLogic;

public abstract class AbstractLayer:ILayer
{
    private string _name;
    private int _x;
    private int _y;
    private IImage? _resualtImage;

    public AbstractLayer(string name)
    {
        _name = name;
        ImageLoader = new ImageLoader(new PixelMapLoader(), new PixelMapBinder());
    }
    public AbstractLayer(string name,string path)
    {
        _name = name;
        ImageLoader = new ImageLoader(new PixelMapLoader(), new PixelMapBinder());
        ResultImage = ImageLoader.Load(path);
    }
    public AbstractLayer(string name, IImage image)
    {
        _name = name;
        ImageLoader = new ImageLoader(new PixelMapLoader(), new PixelMapBinder());
        ResultImage = image;
    }

    public virtual List<IFilter> Filters { get; }

    public virtual string Name
    {
        get { return _name; }
        set
        {
            if (value != _name && value != null)
            {
                _name = value;
                OnPropertyChanged("LayerName");
            }
        }
    }

    public virtual int X
    {
        get
        {
            return _x;
        }
        set
        {
            if (_x!=value)
            {
                _x =  value;
                OnPropertyChanged("LayerPosition");
            }
        }
    }

    public virtual int Y {
        get
        {
            return _y;
        }
        set
        {
            if (_y != value)
            {
                _y = value;
                OnPropertyChanged("LayerPosition");
            }
        }
    }
    public virtual IImageLoader ImageLoader { get; set; }

    public virtual IImage? ResultImage
    {
        get
        {
            return _resualtImage;
        }
        set
        {
            _resualtImage = value;
            OnPropertyChanged("LayerImage");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}