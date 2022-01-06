using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using AraLibraries.Entities.TeklaEntities;

namespace AraLibraries.Entities.TeklaEntities
{
    public class RectangleMesh
    {
        public Mesh RectMesh { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public double Length { get; private set; }
        public RectangleMesh()
        {
            Height = 1;
            Width = 1;
            Length = 1;
            ComposeMesh();
        }
        public RectangleMesh(double height, double width, double length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
            ComposeMesh();
        }
       

        private void ComposeMesh()
        {
            var gd = ModelOperationSingleton.GetGD();
            RectMesh = new Mesh();
            var x = Length;
            var y = Width / 2;
            var z = Height / 2;
            // 0 Vert
            RectMesh.AddPoint(new Point(0, y,-z));
            gd.DrawText(new Point(0, y, -z), "0", new Color(0, 0, 1));
            // 1 Vert
            RectMesh.AddPoint(new Point(0, -y, -z));
            gd.DrawText(new Point(0, -y, -z), "1", new Color(0, 0, 1));
            // 2 Vert
            RectMesh.AddPoint(new Point(0, -y, z));
            gd.DrawText(new Point(0, -y, z), "2", new Color(0, 0, 1));
            // 3 Vert
            RectMesh.AddPoint(new Point(0, y, z));
            gd.DrawText(new Point(0, y, z), "3", new Color(0, 0, 1));
            // 4 Vert
            RectMesh.AddPoint(new Point(x, y, -z));
            gd.DrawText(new Point(x, y, -z), "4", new Color(0, 0, 1));
            // 5 Vert
            RectMesh.AddPoint(new Point(x, -y, -z));
            gd.DrawText(new Point(x, -y, -z), "5", new Color(0, 0, 1));
            // 6 Vert
            RectMesh.AddPoint(new Point(x, -y, z));
            gd.DrawText(new Point(x, -y, z), "6", new Color(0, 0, 1));
            // 7 Vert
            RectMesh.AddPoint(new Point(x, y, z));
            gd.DrawText(new Point(x, y, z), "7", new Color(0, 0, 1));

            // Front face
            RectMesh.AddTriangle(0, 1, 2);
            RectMesh.AddTriangle(0, 2, 3);
            // Bottom face
            RectMesh.AddTriangle(4, 1, 0);
            RectMesh.AddTriangle(4, 5, 1);
            // Left face
            RectMesh.AddTriangle(7, 4, 0);
            RectMesh.AddTriangle(0, 3, 7);
            // Right face
            RectMesh.AddTriangle(1, 5, 6);
            RectMesh.AddTriangle(6, 2, 1);
            // Top face
            RectMesh.AddTriangle(3, 2, 6);
            RectMesh.AddTriangle(6, 7, 3);
            // Behind face
            RectMesh.AddTriangle(6, 5, 4);
            RectMesh.AddTriangle(4, 7, 6);

            // Lines for Front face
            RectMesh.AddLine(0, 1); RectMesh.AddLine(1, 2); RectMesh.AddLine(2, 3); RectMesh.AddLine(3, 0);
            // Lines for Bottom face
            RectMesh.AddLine(0, 4); RectMesh.AddLine(1, 5);
            // Lines for Top face
            RectMesh.AddLine(3, 7); RectMesh.AddLine(2, 6);
            // Lines for Behind face
            RectMesh.AddLine(4, 5); RectMesh.AddLine(5, 6); RectMesh.AddLine(6, 7); RectMesh.AddLine(7, 4);
        }
    }
}
