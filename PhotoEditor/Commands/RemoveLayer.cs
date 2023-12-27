using PhotoEditor.MainLogic;

namespace PhotoEditor.Commands;

public class RemoveLayer:IProgramCommand
{
    private IImageProcessor _imageProcessor;
    private string _layerName;
    private ILayer _oldLayer;
    private int _oldLayerPos;

    public RemoveLayer(IImageProcessor imageProcessor, string layerName)
    {
        _imageProcessor = imageProcessor;
        _layerName = layerName;
        _oldLayerPos = _imageProcessor.SearchLayerPosition(layerName);
        _oldLayer = _imageProcessor.Layers[_oldLayerPos];
    }

    public void Execute()
    {
        _imageProcessor.RemoveLayer(_layerName);
    }

    public void Undo()
    {
        _imageProcessor.UnRemoveLayer(_oldLayer,_oldLayerPos);
    }
}