using PhotoEditor.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoEditor.MainLogic
{
    public interface ILayer
    {
        string Name { get; }

        public List<IFilter> Filters { get; }
        public void AddFilter(IFilter filter);
        public void CancelFilter(IFilter filter);
        public void AddImage(IImage image);
        public BitmapSource Draw();
    }
}
