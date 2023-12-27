using System;
using System.Collections.Generic;

namespace PhotoEditor.Commands;

public static class ListExtension
{
    
    public static IProgramCommand PopEnd(this List<IProgramCommand> list)
    {
        if (list.Count > 0)
        {
            IProgramCommand programCommand = list[^1];
            list.RemoveAt(list.Count - 1);
            return programCommand;
        }
        else throw new ArgumentException("List is empty");
    }
    public static IProgramCommand PopStart(this List<IProgramCommand> list)
    {
        IProgramCommand programCommand = list[0];
        list.RemoveAt(0);
        return programCommand;
    }
}