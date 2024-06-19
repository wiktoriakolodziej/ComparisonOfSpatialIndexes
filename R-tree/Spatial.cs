using ComparisonOfSpatialIndexes.Quadtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.R_tree
{
    internal interface ISpatial
    {
        Boundary Boundary { get; }
    }
}
