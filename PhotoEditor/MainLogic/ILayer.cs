using PhotoEditor.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PhotoEditor.Commands;

namespace PhotoEditor.MainLogic
{
    public interface ILayer:IKnowPosition, INotifyPropertyChanged
    {
        string Name { get; set; }
        public IImage? ResultImage { get; set; }
    }
}
