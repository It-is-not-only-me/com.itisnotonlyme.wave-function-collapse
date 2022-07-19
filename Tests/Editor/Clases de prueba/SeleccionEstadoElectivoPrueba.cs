using ItIsNotOnlyMe.WaveFunctionCollapse;
using System.Collections.Generic;

public class SeleccionEstadoElectivoPrueba : ISeleccionarEstado
{
    private int _numero;

    public SeleccionEstadoElectivoPrueba(int numero)
    {
        _numero = numero;
    }

    public IEstado Elegir(List<IEstado> estados)
    {
        return estados[_numero - 1];
    }
}
