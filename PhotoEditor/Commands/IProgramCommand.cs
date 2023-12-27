namespace PhotoEditor.Commands;

public interface IProgramCommand
{
    void Execute();
    void Undo();
}