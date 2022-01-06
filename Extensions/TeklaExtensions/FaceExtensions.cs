using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class FaceExtensions
    {
        public static Point GetCenterPoint(this Face face)
        {
            Point ceanterPoint = face.GetPolygon().GetCenterPoint();
            return ceanterPoint;
        }
        public static Polygon GetPolygon(this Face face)
        {
            var loops = face.GetLoopEnumerator().ToList<Loop>();
            if (loops.Count >1)
            {
                throw new Exception("OBJEKTA PLAKNEI IR VAIRĀK PAR 1 LOOP, SKATĪT FACEEXTENSIONS");
            }
            List<Point> points = loops[0].GetVertexEnumerator().ToList<Point>();
            Polygon polygon = new Polygon();
            polygon.AddPoints(points);
            return polygon;
        }
    }
}
