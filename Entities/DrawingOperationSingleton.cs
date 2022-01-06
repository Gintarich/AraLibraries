using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace AraLibraries.Entities
{
    public sealed class DrawingOperationSingleton
    {
        private static readonly DrawingOperationSingleton _instace = new DrawingOperationSingleton();
        private static readonly DrawingHandler _drawingHandler = new DrawingHandler();
        private DrawingOperationSingleton()
        {

        }
        public static DrawingOperationSingleton GetInstance() => _instace;
        public static DrawingHandler GetDrawingHandler() => _drawingHandler;
    }
}
