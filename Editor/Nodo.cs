using System.Collections.Generic;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public class Nodo : INodo
    {
        private List<IArista> _adyacentes;
        private List<IEstado> _estados;

        public Nodo(List<IEstado> estados)
        {
            _adyacentes = new List<IArista>();
            _estados = estados;
        }

        public int Entropia()
        {
            return _estados.Count;
        }


        public void Colapsar(IGeneradorDeNumeros generadorDeNumeros)
        {
            IEstado estadoUnico = _estados[generadorDeNumeros.Numero(_estados.Count)];
            _estados.Clear();
            _estados.Add(estadoUnico);
            Propagar(_estados);
        }

        public void Colapsar(List<IEstado> estados, IValor valor)
        {
            List<IEstado> estadosASacar = new List<IEstado>();

            foreach (IEstado estado in _estados)
                if (estado.SeElimina(estados, valor))
                    estadosASacar.Add(estado);

            foreach (IEstado estadoASacar in estadosASacar)
                _estados.Remove(estadoASacar);

            if (estadosASacar.Count > 0)
                Propagar(_estados);
        }

        public void Propagar(List<IEstado> estados)
        {
            _adyacentes.ForEach(adyacente => adyacente.Propagar(_estados));
        }

        public void AgregarAdyacente(IArista adyacente)
        {
            _adyacentes.Add(adyacente);
        }

        public IEstado EstadoFinal()
        {
            if (Entropia() > 1)
                return null;
            return _estados[0];
        }
    }
}
