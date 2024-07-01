using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.NewRTree
{
    public interface ISpatial
    {
        Rect Rect { get; }
    }
}
