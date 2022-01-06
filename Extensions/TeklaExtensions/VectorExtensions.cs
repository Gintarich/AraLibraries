using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class VectorExtensions
    {
        public static Vector Unitize(this Vector vector)
        {
            double length = vector.GetLength();

            var vx = vector.X / length;
            var vy = vector.Y / length;
            var vz = vector.Z / length;

            return new Vector(vx, vy, vz);
        }
    }
}
