using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM=Tekla.Structures.Model;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class PolygonExtensions
    {
        public static bool IsPointInsidePolygon(this Polygon polygon, Point point)
        {
            //var cs = polygon.GetCoordSystem();
            //CordinateSystem coordinateSystem = new CoordinateSystem()
            //TODO : 3D conversion
            int crossingNumber = 0;
            var polygonPoints = polygon.GetPointList();
            
            var p = point;
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                var currentVert = polygonPoints[i];
                Point nextVert;
                // Resolve last point index exception
                if (i ==(polygonPoints.Count-1))
                {
                    nextVert = polygonPoints[0];
                }
                else
                {
                    nextVert = polygonPoints[i+1];
                }
                //if point y is in bounds of the line
                if (((currentVert.Y <= p.Y) && (nextVert.Y > p.Y)) ||
                    ((currentVert.Y > p.Y) && (nextVert.Y <= p.Y)))
                {
                    double vt = (p.Y - currentVert.Y) / (nextVert.Y - currentVert.Y);
                    if (p.X < currentVert.X + vt * (nextVert.X - currentVert.X))
                    {
                        crossingNumber++;
                    }
                }
                return (crossingNumber % 2==1);
            }
            return false;
            
        }
        public static CoordinateSystem GetCoordSystem(this Polygon polygon)
        {
            //TODO: Implement check for the case when v2 and v3 are almost paralel
            var previousPoint = polygon.Points[0] as Point;
            var origin = polygon.Points[1] as Point;
            var nextPoint = polygon.Points[2] as Point;
            var v1 = new Vector(origin - previousPoint);
            var v2 = new Vector(origin - nextPoint);
            var nomral = v1.Cross(v2);
            v1 = v2.Cross(nomral);
            return new CoordinateSystem(origin, v1, v2);
        }
        public static List<Point> GetPointList(this Polygon polygon)
        {
            var pointList = new List<Point>();
            foreach (Point point in polygon.Points)
            {
                pointList.Add(point);
            }
            return pointList;
        }
        public static void AddPoints(this Polygon polygon, List<Point> points)
        {
            foreach (var pt in points)
            {
                polygon.Points.Add(pt);
            }
        }
        public static Point GetCenterPoint(this Polygon polygon)
        {
            Point point = new Point();
            foreach (Point pt in polygon.Points)
            {
                point.X += pt.X;
                point.Y += pt.Y;
                point.Z += pt.Z;
            }
            point.X = point.X / polygon.Points.Count;
            point.Y = point.Y / polygon.Points.Count;
            point.Z = point.Z / polygon.Points.Count;

            return point;
        }
        public static List<Point> GetUniqueXPoints(this Polygon polygon)
        {
            List<Point> uniqueXPts = new List<Point>();
            List<Point> sortedPts = polygon.GetPointList().OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            uniqueXPts.Add(sortedPts[0]);
            for (int i = 1; i < sortedPts.Count; i++)
            {
                var current = i;
                var previous = i - 1;
                if (sortedPts[current].X != sortedPts[previous].X)
                {
                    uniqueXPts.Add(sortedPts[current]);
                }
            }
            return uniqueXPts;
        }
        public static List<Point> GetUniqueYPoints(this Polygon polygon)
        {
            List<Point> uniqueYPts = new List<Point>();
            List<Point> sortedPts = polygon.GetPointList().OrderBy(p => p.Y).ThenBy(p => p.X).ToList();
            uniqueYPts.Add(sortedPts[0]);
            for (int i = 1; i < sortedPts.Count; i++)
            {
                var current = i;
                var previous = i - 1;
                if (sortedPts[current].Y != sortedPts[previous].Y)
                {
                    uniqueYPts.Add(sortedPts[current]);
                }
            }
            return uniqueYPts;
        }

    }
}
