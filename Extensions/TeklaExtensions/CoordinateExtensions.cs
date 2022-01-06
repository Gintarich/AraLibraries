using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using AraLibraries.Entities;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class CoordinateExtensions
    {
        public static TransformationPlane CurentCoordinateSystem;
        public static void ChengeCoordinateSystem(CoordinateSystem cs)
        {
            //Gets the Tekla Structures model instance from singleton
            Model model = ModelOperationSingleton.GetModel();

            var wpHandler = model.GetWorkPlaneHandler();
            CurentCoordinateSystem = wpHandler.GetCurrentTransformationPlane();
            wpHandler.SetCurrentTransformationPlane(new TransformationPlane(cs));
        }
        public static void SetPreviousCS()
        {
            //Gets the Tekla Structures model instance from singleton
            Model model = ModelOperationSingleton.GetModel();

            if (CurentCoordinateSystem == null)
            {
                throw new NullReferenceException("Nav pieejama iepriekšējā darba plakne");
            }
            var wpHandler = model.GetWorkPlaneHandler();
            wpHandler.SetCurrentTransformationPlane(CurentCoordinateSystem);
        }
    }
}
