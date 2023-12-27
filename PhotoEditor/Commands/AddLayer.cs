using PhotoEditor.MainLogic;

namespace PhotoEditor.Commands;

public class AddLayer:IProgramCommand
{
    private IImageProcessor _imageProcessor;
    private string _layerName;
    private string? _path;

    public AddLayer(IImageProcessor imageProcessor, string layerName, string? path=null)
    {
        _imageProcessor = imageProcessor;
        _layerName = layerName;
        _path = path;
    }

    public void Execute()
    {
        _imageProcessor.AddLayer(_layerName,_path);
    }

    public void Undo()
    {
        _imageProcessor.RemoveLayer(_layerName);
    } 
}