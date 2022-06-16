using ItIsNotOnlyMe.WaveFunctionCollapse;

public class GeneradorElectivoPrueba : IGeneradorDeNumeros
{
    private int _numero;

    public GeneradorElectivoPrueba(int numero)
    {
        _numero = numero;
    }

    public int Numero(int final, int inicio = 0)
    {
        return _numero - 1;
    }
}
