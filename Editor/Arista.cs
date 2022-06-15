using System.Collections.Generic;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public class Arista : IArista
    {
        private INodo _adyacente;
        private IValor _valor;

        public Arista(INodo adyacente, IValor valor)
        {
            _adyacente = adyacente;
            _valor = valor;
        }

        public void Propagar(List<IEstado> estados)
        {
            _adyacente.Colapsar(estados, _valor);
        }
    }
}
