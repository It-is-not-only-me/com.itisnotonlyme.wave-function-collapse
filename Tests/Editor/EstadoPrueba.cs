using System.Collections.Generic;
using ItIsNotOnlyMe.WaveFunctionCollapse;

public class EstadoPrueba : IEstado
{
    public int Numero { get; }

    public EstadoPrueba(int numero)
    {
        Numero = numero;
    }

    public bool SeElimina(List<IEstado> estados, IValor valor)
    {
        bool seElimina = estados.Count > 0;
        foreach (IEstado estado in estados)
        {
            EstadoPrueba estadoPrueba = estado as EstadoPrueba;
            seElimina &= Numero == estadoPrueba.Numero;
        }
        return seElimina;
    }
}
