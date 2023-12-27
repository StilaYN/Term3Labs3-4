using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using PhotoEditor.Commands;

namespace PhotoEditor.ModelView;

public class EnterNameWindowMV:INotifyPropertyChanged
{
    private HashSet<string> _busyName;
    private string _name;
    private Window _window;
    public bool CanGetName { get; private set; }
    public EnterNameWindowMV(Window window,HashSet<string> busyName)
    {
        _busyName = busyName;
        _window = window;
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (value != _name)
            {
                _name=value;
            }
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
        }
    }

    public ICommand Ready => new CommandTest(ReadyMethod);

    private void ReadyMethod(object? sender, EventArgs args)
    {
        if (!string.IsNullOrEmpty(Name) && !_busyName.Contains(Name))
        {
            CanGetName = true;
            _window.Close();
        }
        else
        {
            Name += "(1)";
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
}