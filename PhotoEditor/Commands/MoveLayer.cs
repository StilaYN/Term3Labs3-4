using PhotoEditor.MainLogic;

namespace PhotoEditor.Commands;

public class MoveLayer:IProgramCommand
{
    private ILayer? _layer;
    private int _oldX;
    private int _oldY;
    private int _newX;
    private int _newY;

    public MoveLayer(ILayer? layer, int newX, int newY)
    {
        _layer = layer;
        _newX = newX;
        _newY = newY;
    }

    public void Execute()
    {
        if (_layer != null)
        {
            _oldX = _layer.X;
            _oldY = _layer.Y;
            _layer.X = _newX;
            _layer.Y = _newY;
        }
    }

    public void Undo()
    {
        if (_layer != null)
        {
            _layer.X = _newX;
            _layer.Y = _oldY;
        }
    }
}