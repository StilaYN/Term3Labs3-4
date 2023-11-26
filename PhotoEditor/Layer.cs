using PhotoEditor.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditor
{
    public class Layer
    {
        public Layer(IImage image)
        {
            _image = image;
        }

        public Layer()
        {
        }

        private IImage? _image;
        public IImage Image
        {
            get { return _image; }
            set { _image = value; }
        }
        public void ApplyFilter(IFilter filter)
        {
            _image = filter.ApplyFiler(_image);
        }

        public void Rotate(IRotate rotation)
        {
            _image = rotation.Rotate(_image,45);
        }
    }
}
