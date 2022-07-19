using ItIsNotOnlyMe.WaveFunctionCollapse;
using System.Collections.Generic;

public class SeleccionarNodoPrueba : ISeleccionarNodo
{
    public INodo Elegir(List<INodo> nodos) => (nodos.Count == 0) ? null : nodos[0];
}
