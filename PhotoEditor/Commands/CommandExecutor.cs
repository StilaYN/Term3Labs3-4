namespace PhotoEditor.Commands;

public class CommandExecutor<TCommand>:AbstractCommandExecutor<TCommand> where TCommand : IProgramCommand
{
    public CommandExecutor(int capacity):base (capacity){}
}