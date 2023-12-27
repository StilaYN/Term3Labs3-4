using System;
using System.Windows;
using System.Windows.Input;
using PhotoEditor.Commands;

namespace PhotoEditor.ModelView;

public class SaveWindowMV
{
    private Window _window;
    private float _dpiX;
    private float _dpiY;
    public bool CanSave { get; private set; }

    public SaveWindowMV(Window window)
    {
        _window = window;
        _dpiX = 300;
        _dpiY = 300;
        CanSave = false;
    }
    public ICommand SaveCommand => new CommandTest(SaveMethod);

    public float DpiX
    {
        get { return _dpiX;}
        set
        {
            if (value > 0)
            {
                _dpiX = value;
            }
        }
    }
    public float DpiY
    {
        get { return _dpiY; }
        set
        {
            if (value > 0)
            {
                _dpiY = value;
            }
        }
    }

    private void SaveMethod(object? s, EventArgs e)
    {
        CanSave=true;
        _window.Close();
    }
}