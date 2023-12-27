using PhotoEditor.Commands;
using System;
using System.CodeDom.Compiler;
using System.Windows;
using System.Windows.Input;

namespace PhotoEditor.ModelView;

public class EnterWindowMV
{
    private int _width;
    private int _height;

    public EnterWindowMV()
    {
        _height = 512;
        _width = 512;
    }
    public int Width
    {
        get { return _width; }
        set
        {
            if (value > 0)
            {
                _width = value;
            }
        }
    }
    public int Height
    {
        get { return _height; }
        set
        {
            if (value > 0)
            {
                _height = value;
            }
        }
    }

    public ICommand Create => new CommandTest(CreateMainWindow);

    private void CreateMainWindow(object? s, EventArgs args)
    {
        MainWindow window = new MainWindow();
        window.InitializeComponent();
        window.DataContext = new PhotoEditorMainModelView(_width, _height);
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.Show();
        App.Current.Windows[0].Close();
        App.Current.MainWindow = window;
    }
}