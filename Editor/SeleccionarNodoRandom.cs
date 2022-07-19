using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public class SeleccionarNodoRandom : ISeleccionarNodo
    {
        public INodo Elegir(List<INodo> nodos)
        {
            if (nodos.Count == 0)
                return null;

            int numero = Random.Range(0, nodos.Count);
            return nodos[numero];
        }
    }
}
