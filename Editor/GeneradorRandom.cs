using UnityEngine;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public class GeneradorRandom : IGeneradorDeNumeros
    {
        public int Numero(int final, int inicio = 0)
        {
            return Random.Range(inicio, final);
        }
    }
}
