﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditor.Filters
{
    public interface IFilter
    {
        public IImage ApplyFilter(IImage? image);
    }
}
