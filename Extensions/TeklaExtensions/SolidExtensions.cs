using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;
using AraLibraries.Extensions.TeklaExtensions.GraphicsExtensions;
using Tekla.Structures.Geometry3d;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class SolidExtensions
    {
        public static Face GetBottomPlane(this Solid solid)
        {
            Face faceOut = null;
            List<Face> faces = solid.GetFaceEnumerator().ToList<Face>();
            foreach (Face face in faces)
            {
                if (face.Normal.Dot(new Vector(0,0,-1))>0.95)
                {
                    faceOut = face;
                }
            }
            
            return faceOut;
        }
        public static void DrawNormals(this Solid solid)
        {
            int iterator = 0;
            List<Face> faces = solid.GetFaceEnumerator().ToList<Face>();
            foreach (Face face in faces)
            {
                AraGraphicsDrawer.DrawVector(face.GetCenterPoint(), face.Normal, iterator.ToString());
                iterator++;
            }
        }
        
    }
}
