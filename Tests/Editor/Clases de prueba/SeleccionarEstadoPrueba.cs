using System.Collections.Generic;
using ItIsNotOnlyMe.WaveFunctionCollapse;

public class SeleccionarEstadoPrueba : ISeleccionarEstado
{
    public IEstado Elegir(List<IEstado> estados) => (estados.Count == 0) ? null : estados[0];
}
