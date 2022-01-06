using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace AraLibraries.Extensions.TeklaExtensions
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// Creates list of objects with given type from object implementing <see cref="IEnumerator"/> interface.
        /// </summary>
        /// <typeparam name="T">Type of objects in the list.</typeparam>
        /// <param name="enumerator">Object implementing <see cref="IEnumerator"/> interface.</param>
        /// <returns>List of objects of given type.</returns>
        public static List<T> ToList<T>(this IEnumerator enumerator)
        {
            var list = new List<T>();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is T tItem) list.Add(tItem);
            }
            return list;
        }

        /// <summary>
        /// Overload of Tekla's method, using an instance of <see cref="Geometry3d.AABB"/> as input parameter instead of 2 points.
        /// </summary>
        /// <param name="selector">Model object selector to select nearby objects.</param>
        /// <param name="box">Axis-aligned bounding box as input.</param>
        /// <returns>Enumerator of model objects being near to given AABB.</returns>
        public static ModelObjectEnumerator GetObjectsByBoundingBox(this ModelObjectSelector selector, TSG.AABB box)
        {
            return selector.GetObjectsByBoundingBox(box.MinPoint, box.MaxPoint);
        }

        /// <summary>
        /// Overload of Tekla's method, using an instance of <see cref="Geometry3d.OBB"/> as input parameter instead of 2 points.
        /// </summary>
        /// <param name="selector">Model object selector to select nearby objects.</param>
        /// <param name="box">Oriented bounding box as input.</param>
        /// <returns>Enumerator of model objects being near to given AABB.</returns>
        public static ModelObjectEnumerator GetObjectsByBoundingBox(this ModelObjectSelector selector, TSG.OBB box)
        {
            var workPlaneHandler = new Model().GetWorkPlaneHandler();
            var currentTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();
            workPlaneHandler.SetCurrentTransformationPlane(new TransformationPlane(box.Center, box.Axis0, box.Axis1));
            var result = selector.GetObjectsByBoundingBox(
                MinPoint: new TSG.Point(-box.Extent0, -box.Extent1, -box.Extent2),
                MaxPoint: new TSG.Point(box.Extent0, box.Extent1, box.Extent2));
            workPlaneHandler.SetCurrentTransformationPlane(currentTransformationPlane);
            return result;
        }
    }
}
