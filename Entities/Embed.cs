using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using AraLibraries.Entities;
using System;

namespace AraLibraries.Entities
{

    /// <summary>
    /// The class that represents embeded object in Tekla Structures
    /// </summary>
    public class Embed
    {

        #region Properties

        /// <summary>
        /// The Main Part of the embed
        /// </summary>
        public TSM.ModelObject MainPart { get; private set; }

        /// <summary>
        /// The Component of the embed if it has one
        /// </summary>
        public CustomPart ComponentOfTheEmbed { get; private set; }

        /// <summary>
        /// Location of the main part(On which plane does the embed lie)
        /// </summary>
        public AraFace LocationOnMainPart { get; set; }

        /// <summary>
        /// Refernce point of the embed
        /// </summary>
        public Point ReferencePoint { get; private set; }

        #endregion


        #region Constructors
        public Embed(ModelObject modelObject, CustomPart customPart)
        {
            MainPart = modelObject;
            ComponentOfTheEmbed = customPart;
            
        }
        public Embed(TSM.ModelObject modelObject)
        {
            MainPart = modelObject;
        }


        #endregion
        public void CalculateFace(AraMainPart mainPart)
        {
            foreach (var face in mainPart.MaipartFaces)
            {
                if (!(ReferencePoint is null) && face.ContainsPoint(ReferencePoint))
                {
                    LocationOnMainPart = face;
                }
            }
        }
    }


}
