using System;
using System.Windows.Input;

namespace PhotoEditor.Commands;

public class CommandTest:ICommand
{
    public CommandTest(EventHandler method) => CanExecuteChanged += method;

    public bool CanExecute(object? parameter) => CanExecuteChanged != null;

    public void Execute(object? parameter) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;
}