using PhotoEditor.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditor.MainLogic
{
    public class Layer:AbstractLayer
    {
        public Layer(string name):base(name){}
        public Layer(string name,string path) : base(name, path) { }
        public Layer(string name,IImage image) : base(name, image) { }
        
    }
}
