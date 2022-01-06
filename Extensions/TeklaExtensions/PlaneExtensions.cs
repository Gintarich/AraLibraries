using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class PlaneExtensions
    {
        public static Vector GetNormal (this Plane plane)
        {
            return plane.AxisX.Cross(plane.AxisY).Unitize();
        }
    }
}
