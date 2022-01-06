using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using TSD = Tekla.Structures.Drawing;
using TSG = Tekla.Structures.Geometry3d;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class GridExtensions
    {
        public static List<TSG.Point> GetIntersections(this Grid grid)
        {
            var gridPlanes = grid.GetChildren().ToList<GridPlane>().ToList();
            var planes = new List<Plane>();
            foreach (var gPlane in gridPlanes)
            {
                var plane = gPlane.Plane;
                if (plane.GetNormal() == new TSG.Vector(0,0,1))
                {
                    continue;
                }
                planes.Add(plane);
            }

            return GetIntersectionPoints(planes).Distinct().ToList();
        }

        private static List<TSG.Point> GetIntersectionPoints(List<Plane> gridPlane)
        {
            var points = new List<TSG.Point>();
            var geoPlanes = ConvertPlaneToGeoPlane(gridPlane);

            foreach (var geoPlane in geoPlanes)
            {
                for (int i = 0; i < geoPlanes.Count; i++)
                {
                    if (!TSG.Parallel.PlaneToPlane(geoPlane, geoPlanes[i]))
                    {
                        TSG.Line intersection = TSG.Intersection.PlaneToPlane(geoPlane, geoPlanes[i]);
                        if (!(intersection is null ) )
                        {
                            points.Add(intersection.Origin);
                        }
                    }
                }
            }
            return points;
        }

        private static List<TSG.GeometricPlane> ConvertPlaneToGeoPlane(List<Plane> planes)
        {
            var geoPlanes = new List<TSG.GeometricPlane>();

            planes.ForEach(p =>
                {
                    geoPlanes.Add(new TSG.GeometricPlane(p.Origin, p.AxisX, p.AxisY));
                });
            return geoPlanes;
        }


        public static void DrawPoint(List<TSG.Point> points)
        {
            var drawer = new GraphicsDrawer();
            var pointsToDraw = points.FindAll(x => x.Z == 0);
            for (int i = 0; i < pointsToDraw.Count; i++)
            {
                drawer.DrawText(pointsToDraw[i], i.ToString(), new Color(0.99,0,0));
            }
        }
    }
}
