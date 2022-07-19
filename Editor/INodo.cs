using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public interface INodo
    {
        public int Entropia();

        public void AgregarAdyacente(IArista adyacente);

        public void Colapsar(ISeleccionarEstado seleccionarEstado);

        public void Propagar(List<IEstado> estados);

        public void Colapsar(List<IEstado> estados, IValor valor);

        public IEstado EstadoFinal();
    }
}
