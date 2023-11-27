using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditor.MainLogic
{
    public interface IImageProcessor
    {
        public List<ILayer> layers { get;}
        public Layer CurretLayer { get; set; }
        public void AddLayer(string newLayerName);
        public void RemoveLayer(string layerName);
    }
}
