using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using TSD = Tekla.Structures.Drawing;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class PointExtensions
    {
        public static Point GetClosestPoint(this Point point, List<Point> points)
        {
            Point bestPoint = new Point();
            var distVector = new Vector(new Vector(points[0])-new Vector(point));
            var bestDistance = distVector.GetLength();
            foreach (var pt in points)
            {
                distVector = new Vector(new Vector(pt) - new Vector(point));
                if (distVector.GetLength()<bestDistance)
                {
                    bestPoint = pt;
                    bestDistance = distVector.GetLength();
                }
            }
            return bestPoint;
        }
    }
}
