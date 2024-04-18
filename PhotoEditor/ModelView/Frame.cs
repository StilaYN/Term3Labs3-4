using System.ComponentModel;
using PhotoEditor.MainLogic;

namespace PhotoEditor.ModelView;

public class Frame:INotifyPropertyChanged
{
    private int _posX;
    private int _posY;
    private int _width;
    private int _height;
    private IImage _frame;
    private IPixel _examplePixel;

    public int Width
    {
        get { return _width; }
        private set
        {
            _width = value; 
            OnPropertyChanged("FrameSize");
        }
    }

    public int Height
    {
        get { return _height; }
        private set
        {
            _height = value; 
            OnPropertyChanged("FrameSize");
        }
    }
    public int X => _posX;
    public int Y => _posY;

    public Frame(int width, int height,IPixel examplePixel)
    {
        _width = width;
        _height = height;
        _posX = 0;
        _posY = 0;
        _examplePixel = examplePixel;
        ResizeFrame(width,height);
    }
    
    public void ResizeFrame(int width, int height)
    {
        _frame = new Image(width, height);
        Width = width;
        Height = height;
        for (int i = 0; i < width; i++)
        {
            _frame.Pixels[0,i] = _examplePixel;
            _frame.Pixels[height-1, i] = _examplePixel;
        }
        for (int i = 0; i < height; i++)
        {
            _frame.Pixels[i,0] = _examplePixel;
            _frame.Pixels[i,width-1] = _examplePixel;
        }
        OnPropertyChanged("Frame");
    }

    public void MoveFrame(int x, int y)
    {
        _posX = x;
        _posY = y;
        OnPropertyChanged("Frame");
    }

    public IImage GetResultImage(IImage? image)
    {
        if (image != null)
        {
            IImage result = (IImage)image.Clone();
            if (image != null)
            {
                for (int i = 0; i + _posY < result.Height && i < _frame.Height; i++)
                {
                    for (int j = 0; j + _posX < result.Width && j < _frame.Width; j++)
                    {
                        if (i + _posY >= 0 && j + _posX >= 0)
                        {
                            IPixel pixel = _frame.Pixels[i, j];
                            if (pixel.Alpha != 0)
                                result.Pixels[i + _posY, j + _posX] = _frame.Pixels[i, j];
                        }
                    }
                }
            }

            return result;
        }
        else return _frame;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}