using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace AraLibraries.Entities
{
    public sealed class ModelOperationSingleton
    {
        private static readonly ModelOperationSingleton _instance = new ModelOperationSingleton();
        private static readonly Model _model = new Model();
        private static readonly GraphicsDrawer _graphicsDrawer = new GraphicsDrawer();
        private ModelOperationSingleton()
        {

        }
        public static ModelOperationSingleton GetInstance() => _instance;
        
        public static Model GetModel() => _model;

        public static GraphicsDrawer GetGD() => _graphicsDrawer;
    }
}
