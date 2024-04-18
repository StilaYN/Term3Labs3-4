using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditor.MainLogic
{
    public interface IImageProcessor:INotifyPropertyChanged
    {
        public List<ILayer> Layers { get; }
        public HashSet<string> BusyLayersName { get; }
        public ILayer? CurrentLayer { get; set; }
        public void SetCurrentLayer(string name);
        public void AddLayer(string newLayerName, string? path);
        public void RemoveLayer(string layerName);
        public void UnRemoveLayer(ILayer layer,int pos);
        public int SearchLayerPosition(string layerName);
        public IImage ResultImage { get; }
    }
}
