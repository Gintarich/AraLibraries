using AraLibraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace AraLibraries.Entities
{
    public class AraMainPart
    {
        public ModelObject CastUnitMainpart { get;private set; }
        public List<AraFace> MaipartFaces { get;private set; }
        public AraMainPart(ModelObject modelObject)
        {
            CastUnitMainpart = modelObject;
            InitializeFaces();
        }
        private void InitializeFaces()
        {
            Part part = CastUnitMainpart as Part;
            var solid = part.GetSolid();
            var faceEnum = solid.GetFaceEnumerator();
            while (faceEnum.MoveNext())
            {
                MaipartFaces.Add(new AraFace(faceEnum.Current));
            }
        }
    }
}
