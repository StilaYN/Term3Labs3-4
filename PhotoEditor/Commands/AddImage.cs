using System;
using PhotoEditor.MainLogic;

namespace PhotoEditor.Commands;

public class AddImage:IProgramCommand
{
    private ILayer? _layer;
    private string _path;
    private IImage? _oldImage;
    private IImageLoader _loader;

    public AddImage(ILayer? layer, string path,IImageLoader loader)
    {
        _layer = layer;
        _path = path;
        _loader = loader;
    }

    public void Execute()
    {
        if (_path == null) throw new ArgumentException();
        if(_layer.ResultImage != null) _oldImage = (IImage)_layer.ResultImage.Clone();
        else _oldImage = null;
        _layer.ResultImage = _loader.Load(_path);
    }

    public void Undo()
    {
        if (_oldImage != null) _layer.ResultImage = (IImage)_oldImage.Clone();
        else _layer.ResultImage = null;
    }
}