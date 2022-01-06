using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Tekla.Structures.Geometry3d.Point;
using Line = Tekla.Structures.Geometry3d.Line;

namespace AraLibraries.Extensions.Geometry
{
    public static class LineIntersections
    {
        public static bool OnSegment(this Line line ,Point point )
        {
            return false;
        }
    }
}
