using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G__Interpreter;

namespace GeoWallE.Utiles //se llama jerarquia de ensamblador.
{
   public static class Utiles
    {
        public static event Action<Figure> DrawThis;
        public static void InvokeEvent(Figure figure)
        {
            DrawThis.Invoke(figure);
        }
        //to do
        //metodo estatico que recibe el codigo funte como string
        //llamar lexer, llamar parser, hacerle chequeo semantico y scope y no evaluar por el momento
        //el metodo va a devolver una lista de expresiones 
    }
}
