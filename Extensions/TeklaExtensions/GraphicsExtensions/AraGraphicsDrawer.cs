using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Model;
using T3D = Tekla.Structures.Geometry3d;
using AraLibraries.Entities;
using AraLibraries.Entities.TeklaEntities;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class AraGraphicsDrawer
    {
        
        private static readonly GraphicsDrawer GraphicsDrawer = new GraphicsDrawer();

        #region GeometricObjects
        public static void DrawRectangle(Point startPoint, Point endPoint, double thickness)
        {
            var upDirection = new Vector(0, 0, 1);
            // if X and Y coordinates are equal the vector is vertical
            if (startPoint.X == endPoint.X && startPoint.Y == endPoint.Y)
            {
                upDirection = new Vector(0, 1, 0);
            }
            var newXAxis = new Vector(endPoint-startPoint);
            var newYAxis = upDirection.Cross(newXAxis).Unitize();
            var cs = new CoordinateSystem(startPoint, newXAxis, newYAxis);
            DrawCoordinateSytem(cs);
            CoordinateExtensions.ChengeCoordinateSystem(cs);

            var delta = endPoint - startPoint;
            var v1 = new Vector(delta);
            var length = v1.GetLength();

            RectangleMesh rm = new RectangleMesh(thickness,thickness, length);
            GraphicsDrawer.DrawMeshSurface(rm.RectMesh, new Color(1, 0, 0));
            GraphicsDrawer.DrawMeshLines(rm.RectMesh, new Color(0, 1, 0));
            CoordinateExtensions.SetPreviousCS();
        }
        public static void DrawPoint(Point pt)
        {
            PointMesh pointMesh = new PointMesh(pt, 30);
            GraphicsDrawer.DrawMeshSurface(pointMesh.PtMesh, new Color(1, 0, 0));
            GraphicsDrawer.DrawMeshLines(pointMesh.PtMesh, new Color(0, 1, 0));
        }
        #endregion

        #region CoordinateSystems/Vectors
        //Draws the coordinate system in which the values are shown
        public static void DrawCoordinateSytem(CoordinateSystem CoordinateSystem)
        {
            DrawVector(CoordinateSystem.Origin, CoordinateSystem.AxisX, "X", new Color(1, 0, 0));
            DrawVector(CoordinateSystem.Origin, CoordinateSystem.AxisY, "Y", new Color(0, 1, 0));
            var axisZ = CoordinateSystem.AxisX.Cross(CoordinateSystem.AxisY);
            DrawVector(CoordinateSystem.Origin, axisZ, "Z", new Color(0, 0, 1));
        }
        //Draws the vector of the coordinate system
        public static void DrawVector(T3D.Point StartPoint, T3D.Vector Vector, string Text, Color c)
        {
            Color Color = c;
            const double Radians = 0.43;

            Vector = Vector.GetNormal();
            T3D.Vector Arrow01 = new T3D.Vector(Vector);

            Vector.Normalize(500);
            T3D.Point EndPoint = new T3D.Point(StartPoint);
            EndPoint.Translate(Vector.X, Vector.Y, Vector.Z);
            GraphicsDrawer.DrawLineSegment(StartPoint, EndPoint, Color);

            GraphicsDrawer.DrawText(EndPoint, Text, Color);

            Arrow01.Normalize(-100);
            T3D.Vector Arrow = ArrowVector(Arrow01, Radians);

            T3D.Point ArrowExtreme = new T3D.Point(EndPoint);
            ArrowExtreme.Translate(Arrow.X, Arrow.Y, Arrow.Z);
            GraphicsDrawer.DrawLineSegment(EndPoint, ArrowExtreme, Color);

            Arrow = ArrowVector(Arrow01, -Radians);

            ArrowExtreme = new T3D.Point(EndPoint);
            ArrowExtreme.Translate(Arrow.X, Arrow.Y, Arrow.Z);
            GraphicsDrawer.DrawLineSegment(EndPoint, ArrowExtreme, Color);
        }
        public static void DrawVector(T3D.Point StartPoint, T3D.Vector Vector, string Text)
        {
            Color Color = new Color(0, 1, 1);
            DrawVector(StartPoint, Vector, Text, Color);
        }
        //Draws the arrows of the vectors
        public static T3D.Vector ArrowVector(T3D.Vector Vector, double Radians)
        {
            double X, Y, Z;

            if (Vector.X == 0 && Vector.Y == 0)
            {
                X = Vector.X;
                Y = (Vector.Y * Math.Cos(Radians)) - (Vector.Z * Math.Sin(Radians));
                Z = (Vector.Y * Math.Sin(Radians)) + (Vector.Z * Math.Cos(Radians));
            }
            else
            {
                X = (Vector.X * Math.Cos(Radians)) - (Vector.Y * Math.Sin(Radians));
                Y = (Vector.X * Math.Sin(Radians)) + (Vector.Y * Math.Cos(Radians));
                Z = Vector.Z;
            }

            return new T3D.Vector(X, Y, Z);
        }
        #endregion

    }
}
