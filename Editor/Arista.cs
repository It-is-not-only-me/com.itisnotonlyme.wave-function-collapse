using System.Collections.Generic;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public class Arista : IArista
    {
        private INodo _adyacente;

        public Arista(INodo adyacente)
        {
            _adyacente = adyacente;
        }

        public void Propagar(List<IEstado> estados)
        {
            _adyacente.Colapsar(estados);
        }
    }
}
