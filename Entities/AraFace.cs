using System;
using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;

namespace AraLibraries.Entities
{
    public class AraFace
    {
        public Face TeklaFace { get; set; }
        public Vector Normal { get; set; }
        public Point Origin { get; set; }
        public List<Line> Edges { get; set; }
        public List<Point> Points { get; set; }
        public Polygon FacePolygon { get; set; }
        public AraFace(Face face)
        {
            
            TeklaFace = face;
            Normal = face.Normal;
            InitializeGeometry();
            InitializeCoordinateSystem();
        }

        private void InitializeCoordinateSystem()
        {
            //Get Origin
            //Get Coordinate System
            throw new NotImplementedException();
        }

        private void InitializeGeometry()
        {
            if (Points == null)
            {
                Points = new List<Point>();
                if (Edges == null)
                {
                    Edges = new List<Line>();
                }
                var loopEnum = TeklaFace.GetLoopEnumerator();
                while (loopEnum.MoveNext())
                {
                    var vertEnum = loopEnum.Current.GetVertexEnumerator();
                    while (vertEnum.MoveNext())
                    {
                        Points.Add(vertEnum.Current);
                        FacePolygon.Points.Add(vertEnum.Current);
                    }
                }
                for (int i = 0; i < Points.Count; i++)
                {
                    Point startPoint = Points[i];
                    Point endPoint ;
                    if (i == Points.Count - 1)
                    {
                        endPoint = Points[0];
                    }
                    else
                    {
                        endPoint = Points[i + 1];
                    }
                    Edges.Add(new Line(startPoint, endPoint));
                }
            }
        }

        internal bool ContainsPoint(Point referencePoint)
        {

            //transform points and reference point to face coordinate system
            //Figure out if reference point lays inside polygon of a face
            //return false if it doesnt and true if it does
            throw new NotImplementedException();
        }
    }
}