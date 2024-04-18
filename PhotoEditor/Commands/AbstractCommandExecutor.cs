
using System;
using System.Collections.Generic;

namespace PhotoEditor.Commands;

public class AbstractCommandExecutor<TCommand>:ICommandExecutor<TCommand> where TCommand : IProgramCommand
{
    private List<IProgramCommand> _commandHistory;

    public AbstractCommandExecutor(int capacity)
    {
        _commandHistory = new List<IProgramCommand>(capacity);
    }

    public void Execute(TCommand command)
    {
        try
        {
            command.Execute();
            if (_commandHistory.Count == _commandHistory.Capacity)
            {
                _commandHistory.PopStart();
                _commandHistory.Add(command);
            }
            else _commandHistory.Add(command);
        }
        catch { }
    }
    public void Undo()
    {
        try
        {
            IProgramCommand programCommand = _commandHistory.PopEnd();
            programCommand.Undo();
        }
        catch (ArgumentException e)
        {

        }
    }
    
}