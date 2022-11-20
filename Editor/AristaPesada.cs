using System.Collections.Generic;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public class AristaPesada : IArista
    {
        private INodo _adyacente;
        private IValor _peso;

        public AristaPesada(INodo adyacente, IValor peso)
        {
            _adyacente = adyacente;
            _peso = peso;
        }

        public void Propagar(List<IEstado> estados)
        {
            _adyacente.Colapsar(estados, _peso);
        }
    }
}
