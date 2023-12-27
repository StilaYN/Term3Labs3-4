using PhotoEditor.Filters;
using PhotoEditor.MainLogic;

namespace PhotoEditor.Commands;

public class UseColorFilter:IProgramCommand
{
    private IColorFilter _filter;
    private ILayer _layer;
    private IImage? _oldImage;

    public UseColorFilter(IColorFilter filter, ILayer layer)
    {
        _filter = filter;
        _layer = layer;
    }

    public void Execute()
    {
        if (_layer.ResultImage != null)
        {
            _oldImage = (IImage)_layer.ResultImage.Clone();
        }
        else _oldImage = null;
        _layer.ResultImage = _filter.ApplyFilter(_layer.ResultImage);
    }

    public void Undo()
    {
        if(_oldImage!=null) _layer.ResultImage = (IImage)_oldImage.Clone();
        else _layer.ResultImage = null;
    }
}