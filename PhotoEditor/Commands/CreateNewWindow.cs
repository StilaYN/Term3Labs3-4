using System.Windows;
using PhotoEditor.ModelView;

namespace PhotoEditor.Commands;

public class CreateNewWindow:IProgramCommand
{
    private int _width;
    private int _height;

    public CreateNewWindow(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void Execute()
    {
        MainWindow window = new MainWindow();
        window.InitializeComponent();
        window.DataContext = new PhotoEditorMainModelView(_width, _height);
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.Show();
        App.Current.Windows[0].Close();
    }

    public void Undo()
    {

    }
}