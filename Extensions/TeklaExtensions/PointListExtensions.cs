using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class PointListExtensions
    {
        public static void AddPoints(this PointList pointList, List<Point> pointsToAdd)
        {
            foreach (var pt in pointsToAdd)
            {
                pointList.Add(pt);
            }
        }
    }
}
