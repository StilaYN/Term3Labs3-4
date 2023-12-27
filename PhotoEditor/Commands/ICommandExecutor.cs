namespace PhotoEditor.Commands;

public interface ICommandExecutor<TCommand> where TCommand: IProgramCommand
{
    void Execute(TCommand command);
    void Undo();
}