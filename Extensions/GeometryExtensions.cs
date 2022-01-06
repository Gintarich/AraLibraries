using AraLibraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace AraLibraries.Extensions
{
    public static class GeometryExtensions
    {
        // Examples found here http://www.dcs.gla.ac.uk/~pat/52233/slides/Geometry1x1.pdf
        public static bool IsPointInFace(this AraFace face,Point referencePoint)
        {
            Polygon polygon = new Polygon();
            //Create logic for polygon/reference point inclusion
            throw new NotImplementedException();
        }
        public static bool IsPointInPlane(this AraFace face , Point referencePoint)
        {
            var normal = face.Normal;
            Vector vectorToCheck ;
            if (face.Origin is null)
            {
                vectorToCheck = new Vector(face.Points[0] - referencePoint);
                var dotProduct = normal.Dot(vectorToCheck);
                if (dotProduct == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                vectorToCheck = new Vector(face.Origin - referencePoint);
                var dotProduct = normal.Dot(vectorToCheck);
                if (dotProduct == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
