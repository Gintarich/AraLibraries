using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AraLibraries.Entities;
using TSD = Tekla.Structures.Drawing;
using TSDUI = Tekla.Structures.Drawing.UI;
using Tekla.Structures.Model;
using AraLibraries.Extensions.TeklaExtensions;
using Tekla.Structures.Geometry3d;

namespace AraLibraries.DrawingFunctions
{
    public static class Dimensions
    {
        public static TSD.DrawingHandler DH = DrawingOperationSingleton.GetDrawingHandler();
        public static Model TeklaModel = ModelOperationSingleton.GetModel();
        public static TSD.StraightDimensionSet.StraightDimensionSetAttributes myDimSetAttri = new TSD.StraightDimensionSet.StraightDimensionSetAttributes(null, null);
        
        public static void DimensionGeometryToGrid( ModelObject modelObject, TSD.View view, string atributeFileName = "standard" )
        {
            //Get Grid intersections
            var mos = TeklaModel.GetModelObjectSelector();
            var mosEnum = mos.GetAllObjectsWithType(ModelObject.ModelObjectEnum.GRID);
            mosEnum.MoveNext();
            var grid = mosEnum.Current as Grid;
            var gridPoints = grid.GetIntersections();
            //Dimmension settings
            myDimSetAttri = new TSD.StraightDimensionSet.StraightDimensionSetAttributes(null, atributeFileName);
            myDimSetAttri.ExtensionLine = TSD.DimensionSetBaseAttributes.ExtensionLineTypes.No;
            myDimSetAttri.Placing = new TSD.DimensionSetBaseAttributes.DimensionPlacingAttributes
                (TSD.DimensionSetBaseAttributes.Placings.Fixed, new TSD.PlacingDirectionAttributes(), new TSD.PlacingDistanceAttributes());
            if (modelObject is Beam)
            {
                Beam beam = modelObject as Beam;
                Solid solid = beam.GetSolid(Solid.SolidCreationTypeEnum.RAW);
                var xPoints = solid.GetBottomPlane().GetPolygon().GetUniqueXPoints();
                var yPoints = solid.GetBottomPlane().GetPolygon().GetUniqueYPoints();

                var closestGridPoint = solid.GetBottomPlane().GetPolygon().GetCenterPoint().GetClosestPoint(gridPoints);

                TSD.PointList xPointList = new TSD.PointList();
                TSD.PointList yPointList = new TSD.PointList();

                xPointList.AddPoints(xPoints);
                yPointList.AddPoints(yPoints);
                xPointList.Add(closestGridPoint);
                yPointList.Add(closestGridPoint);

                Vector yDir = new Vector(0, -1, 0);
                Vector xDir = new Vector(-1, 0, 0);
                
                var xDim1 = new TSD.StraightDimensionSetHandler().CreateDimensionSet(view, xPointList, yDir, 5 * view.Attributes.Scale, myDimSetAttri);
                var yDim1 = new TSD.StraightDimensionSetHandler().CreateDimensionSet(view, yPointList, xDir, 5 * view.Attributes.Scale, myDimSetAttri);
                DH.GetActiveDrawing().CommitChanges();
            }
            else if (modelObject is ContourPlate)
            {
                //TODO: Implement logic for ContourPlate
                throw new NotImplementedException();
            }
        }


    }
}
