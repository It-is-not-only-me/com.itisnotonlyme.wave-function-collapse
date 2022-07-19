using System.Collections.Generic;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public interface ISeleccionarNodo
    {
        public INodo Elegir(List<INodo> nodos);
    }

    public interface ISeleccionarEstado
    {
        public IEstado Elegir(List<IEstado> estados);
    }
}
