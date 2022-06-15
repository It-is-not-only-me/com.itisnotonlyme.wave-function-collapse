using System.Collections.Generic;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public interface IEstado
    {
        public bool SeElimina(List<IEstado> estados, IValor valor);
    }
}
