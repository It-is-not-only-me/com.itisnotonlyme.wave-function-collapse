using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.WaveFunctionCollapse
{
    public static class WaveFunctionCollapse
    {
        public static void Ejecutar(ref List<INodo> nodos, IGeneradorDeNumeros generadorDeNumeros)
        {
            int inicio = generadorDeNumeros.Numero(nodos.Count);
            nodos[inicio].Colapsar(generadorDeNumeros);

            while (!Terminado(nodos))
            {
                INodo nodoActual = NodoConMenorEntropia(nodos, generadorDeNumeros);
                nodoActual.Colapsar(generadorDeNumeros);
            }
        }

        private static bool Terminado(List<INodo> nodos)
        {
            return nodos.TrueForAll(nodo => Nodoterminado(nodo));
        }

        private static INodo NodoConMenorEntropia(List<INodo> nodos, IGeneradorDeNumeros generadorDeNumeros)
        {
            if (Terminado(nodos))
                return null;

            List<INodo> nodosConEntropia = new List<INodo>();
            foreach (INodo nodo in nodos)
                if (!Nodoterminado(nodo))
                    nodosConEntropia.Add(nodo);

            INodo nodoMinimo = nodosConEntropia[0];
            List<INodo> nodosMinimos = new List<INodo> { nodoMinimo };
            int entropiaMinima = nodoMinimo.Entropia();

            foreach (INodo nodo in nodosConEntropia)
            {
                int entropiaActual = nodo.Entropia();

                if (entropiaActual < entropiaMinima)
                {
                    nodosMinimos.Clear();
                    entropiaMinima = entropiaActual;
                }

                if (entropiaActual == entropiaMinima)
                    nodosMinimos.Add(nodo);
            }

            return nodosMinimos[generadorDeNumeros.Numero(nodosMinimos.Count)];
        }

        private static bool Nodoterminado(INodo nodo)
        {
            return nodo.Entropia() <= 1;
        }
    }
}
