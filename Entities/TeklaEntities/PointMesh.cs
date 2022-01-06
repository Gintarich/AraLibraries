using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;

namespace AraLibraries.Entities.TeklaEntities
{
    public class PointMesh
    {
        public Mesh PtMesh { get; private set; }
        public double Size { get; private set; }
        public Point TeklaPoint { get; set; }

        public PointMesh(Point point, double size)
        {
            this.Size = size;
            this.TeklaPoint = point;
            ComposeMesh();
        }

        private void ComposeMesh()
        {
            var gd = ModelOperationSingleton.GetGD();
            PtMesh = new Mesh();
            // negative values
            var nx = TeklaPoint.X - Size / 2;
            var ny = TeklaPoint.Y - Size / 2;
            var nz = TeklaPoint.Z - Size / 2;
            // pozitive values
            var px = TeklaPoint.X + Size / 2;
            var py = TeklaPoint.Y + Size / 2;
            var pz = TeklaPoint.Z + Size / 2;

            // 0 Vert
            PtMesh.AddPoint(new Point(nx, py, nz));
            // 1 Vert
            PtMesh.AddPoint(new Point(nx, ny, nz));
            // 2 Vert
            PtMesh.AddPoint(new Point(nx, ny, pz));
            // 3 Vert
            PtMesh.AddPoint(new Point(nx, py, pz));
            // 4 Vert
            PtMesh.AddPoint(new Point(px, py, nz));
            // 5 Vert
            PtMesh.AddPoint(new Point(px, ny, nz));
            // 6 Vert
            PtMesh.AddPoint(new Point(px, ny, pz));
            // 7 Vert
            PtMesh.AddPoint(new Point(px, py, pz));
            gd.DrawText(TeklaPoint, $"({TeklaPoint.X}, {TeklaPoint.Y}, {TeklaPoint.Z})", new Color(0, 0, 1));

            // Front face
            PtMesh.AddTriangle(0, 1, 2);
            PtMesh.AddTriangle(0, 2, 3);
            // Bottom face
            PtMesh.AddTriangle(4, 1, 0);
            PtMesh.AddTriangle(4, 5, 1);
            // Left face
            PtMesh.AddTriangle(7, 4, 0);
            PtMesh.AddTriangle(0, 3, 7);
            // Right face
            PtMesh.AddTriangle(1, 5, 6);
            PtMesh.AddTriangle(6, 2, 1);
            // Top face
            PtMesh.AddTriangle(3, 2, 6);
            PtMesh.AddTriangle(6, 7, 3);
            // Behind face
            PtMesh.AddTriangle(6, 5, 4);
            PtMesh.AddTriangle(4, 7, 6);

            // Lines for Front face
            PtMesh.AddLine(0, 1); PtMesh.AddLine(1, 2); PtMesh.AddLine(2, 3); PtMesh.AddLine(3, 0);
            // Lines for Bottom face
            PtMesh.AddLine(0, 4); PtMesh.AddLine(1, 5);
            // Lines for Top face
            PtMesh.AddLine(3, 7); PtMesh.AddLine(2, 6);
            // Lines for Behind face
            PtMesh.AddLine(4, 5); PtMesh.AddLine(5, 6); PtMesh.AddLine(6, 7); PtMesh.AddLine(7, 4);
        }
    }
}
