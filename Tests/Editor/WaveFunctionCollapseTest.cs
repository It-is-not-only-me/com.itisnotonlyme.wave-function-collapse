using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ItIsNotOnlyMe.WaveFunctionCollapse;

public class WaveFunctionCollapseTest
{
    private IGeneradorDeNumeros _generadorDeNumeros = new GeneradorInicioPrueba();
    private IValor _valor = new ValorPruba();

    private IEstado _estado1, _estado2, _estado3, _estado4, _estado5, _estado6, _estado7, _estado8, _estado9;

    public WaveFunctionCollapseTest()
    {
        _estado1 = new EstadoPrueba(1);
        _estado2 = new EstadoPrueba(2);
        _estado3 = new EstadoPrueba(3);
        _estado4 = new EstadoPrueba(4);
        _estado5 = new EstadoPrueba(5);
        _estado6 = new EstadoPrueba(6);
        _estado7 = new EstadoPrueba(7);
        _estado8 = new EstadoPrueba(8);
        _estado9 = new EstadoPrueba(9);
    }

    private List<IEstado> CuatroEstados()
    {
        return new List<IEstado> { _estado1, _estado2, _estado3, _estado4 };
    }

    private List<IEstado> NueveEstados()
    {
        return new List<IEstado> { _estado1, _estado2, _estado3, _estado4, _estado5, _estado6, _estado7, _estado8, _estado9 };
    }

    [Test]
    public void Test01LaEntropiaDeUnNodoSinEstadosEsCero()
    {
        INodo nodo = new Nodo(new List<IEstado>());
        Assert.AreEqual(0, nodo.Entropia());
    }

    [Test]
    public void Test02LaEntropiaEsIgualALaCantidadDeEstados()
    {
        List<IEstado> estados = new List<IEstado> { 
            _estado1, _estado2, _estado3, _estado4
        };
        INodo nodo = new Nodo(estados);
        Assert.AreEqual(estados.Count, nodo.Entropia());
    }

    [Test]
    public void Test03LaEntropiaSeVuelveUnoAlTenerEstadosYAlColapsarSuEstados()
    {
        List<IEstado> estados = new List<IEstado> {
            _estado1, _estado2, _estado3, _estado4
        };
        INodo nodo = new Nodo(estados);

        nodo.Colapsar(_generadorDeNumeros);
        Assert.AreEqual(1, nodo.Entropia());
    }

    [Test]
    public void Test04ConDosNodosSePropagaLaPropabilidadYSusEntopiasSonUno()
    {
        INodo principal = new Nodo(new List<IEstado> { _estado1, _estado2 });
        INodo secundario = new Nodo(new List<IEstado> { _estado1, _estado2 });

        IArista aristaPriSec = new Arista(secundario, _valor);
        IArista aristaSecPri = new Arista(principal, _valor);

        principal.AgregarAdyacente(aristaPriSec);
        secundario.AgregarAdyacente(aristaSecPri);

        principal.Colapsar(_generadorDeNumeros);

        Assert.AreEqual(1, principal.Entropia());
        Assert.AreEqual(1, secundario.Entropia());

        EstadoPrueba estadoPrincipal = principal.EstadoFinal() as EstadoPrueba;
        EstadoPrueba estadoSecundario = secundario.EstadoFinal() as EstadoPrueba;
        
        Assert.AreEqual(1, estadoPrincipal.Numero);
        Assert.AreEqual(2, estadoSecundario.Numero);
    }

    [Test]
    public void Test05ConTresNodosVinculadosEnListaSePropagaLaProbabilidadYSusEntropiasSonUnoDosTres()
    {
        INodo principal = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });
        INodo secundario = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });
        INodo terciario = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });

        IArista aristaPriSec = new Arista(secundario, _valor);
        IArista aristaSecPri = new Arista(principal, _valor);
        IArista aristaSecTri = new Arista(terciario, _valor);
        IArista aristaTriSec = new Arista(secundario, _valor);

        principal.AgregarAdyacente(aristaPriSec);
        secundario.AgregarAdyacente(aristaSecPri);

        secundario.AgregarAdyacente(aristaSecTri);
        terciario.AgregarAdyacente(aristaTriSec);

        principal.Colapsar(_generadorDeNumeros);

        Assert.AreEqual(1, principal.Entropia());
        Assert.AreEqual(2, secundario.Entropia());
        Assert.AreEqual(3, terciario.Entropia());
    }

    [Test]
    public void Test06ConTresNodosEnTrianguloAlColapsarUnoElRestoTieneDosDeEntropiaYColapsarUnoDeLosRestantesEsUno()
    {
        INodo principal = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });
        INodo secundario = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });
        INodo terciario = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });

        IArista aristaAPri = new Arista(principal, _valor);
        IArista aristaASec = new Arista(secundario, _valor);
        IArista aristaATri = new Arista(terciario, _valor);

        principal.AgregarAdyacente(aristaASec);
        principal.AgregarAdyacente(aristaATri);

        secundario.AgregarAdyacente(aristaAPri);
        secundario.AgregarAdyacente(aristaATri);

        terciario.AgregarAdyacente(aristaAPri);
        terciario.AgregarAdyacente(aristaASec);

        principal.Colapsar(_generadorDeNumeros);

        Assert.AreEqual(1, principal.Entropia());
        Assert.AreEqual(2, secundario.Entropia());
        Assert.AreEqual(2, terciario.Entropia());

        secundario.Colapsar(_generadorDeNumeros);

        Assert.AreEqual(1, principal.Entropia());
        Assert.AreEqual(1, secundario.Entropia());
        Assert.AreEqual(1, terciario.Entropia());
    }

    [Test] 
    public void Test07ConTresNodosEnTrianguloAlUsarElAlgoritmoTodasLasEntropiasSonUno()
    {
        INodo principal = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });
        INodo secundario = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });
        INodo terciario = new Nodo(new List<IEstado> { _estado1, _estado2, _estado3 });

        IArista aristaAPri = new Arista(principal, _valor);
        IArista aristaASec = new Arista(secundario, _valor);
        IArista aristaATri = new Arista(terciario, _valor);

        principal.AgregarAdyacente(aristaASec);
        principal.AgregarAdyacente(aristaATri);

        secundario.AgregarAdyacente(aristaAPri);
        secundario.AgregarAdyacente(aristaATri);

        terciario.AgregarAdyacente(aristaAPri);
        terciario.AgregarAdyacente(aristaASec);

        List<INodo> nodos = new List<INodo> { principal, secundario, terciario };

        WaveFunctionCollapse.Ejecutar(ref nodos, _generadorDeNumeros);

        Assert.AreEqual(1, principal.Entropia());
        Assert.AreEqual(1, secundario.Entropia());
        Assert.AreEqual(1, terciario.Entropia());

        EstadoPrueba estadoPrincipal = principal.EstadoFinal() as EstadoPrueba;
        EstadoPrueba estadoSecundario = secundario.EstadoFinal() as EstadoPrueba;
        EstadoPrueba estadoTerciario = terciario.EstadoFinal() as EstadoPrueba;

        Assert.AreEqual(1, estadoPrincipal.Numero);
        Assert.AreEqual(2, estadoSecundario.Numero);
        Assert.AreEqual(3, estadoTerciario.Numero);
    }


    [Test]
    public void Test08ConCuatrosNodosTodosVinculadosConTodosAlUsarElAlgoritmoTodasLasEntropiasSonUno()
    {
        List<INodo> nodos = new List<INodo>();

        for (int i = 0; i < 4; i++)
            nodos.Add(new Nodo(CuatroEstados()));

        for (int i = 0; i < nodos.Count; i++)
            for (int j = 0; j < nodos.Count; j++)
            {
                if (i == j)
                    continue;
                nodos[i].AgregarAdyacente(new Arista(nodos[j], _valor));
            }

        WaveFunctionCollapse.Ejecutar(ref nodos, _generadorDeNumeros);

        foreach (INodo nodo in nodos)
            Assert.AreEqual(1, nodo.Entropia());
    }

    [Test]
    public void Test09ConNueveNodosTodosVinculadosConTodosAlUsarElAlgoritmoTodosLasEntropiasSonUno()
    {
        List<INodo> nodos = new List<INodo>();

        for (int i = 0; i < 9; i++)
            nodos.Add(new Nodo(NueveEstados()));

        for (int i = 0; i < nodos.Count; i++)
            for (int j = 0; j < nodos.Count; j++)
            {
                if (i == j)
                    continue;
                nodos[i].AgregarAdyacente(new Arista(nodos[j], _valor));
            }

        WaveFunctionCollapse.Ejecutar(ref nodos, _generadorDeNumeros);

        int contador = 1;
        foreach (INodo nodo in nodos)
        {
            Assert.AreEqual(1, nodo.Entropia());
            EstadoPrueba estadoNodo = nodo.EstadoFinal() as EstadoPrueba;
            Assert.AreEqual(contador++, estadoNodo.Numero);
        }
    }

    private Arista CrearArista(INodo nodo)
    {
        return new Arista(nodo, _valor);
    }

    private int Indice4x4(int fila, int columna)
    {
        return fila * 4 + columna;
    }

    private List<INodo> CrearSudoku4x4()
    {
        List<INodo> nodos = new List<INodo>();
        for (int i = 0; i < 16; i++)
            nodos.Add(new Nodo(CuatroEstados()));

        for (int posicionNodo = 0; posicionNodo < nodos.Count; posicionNodo++)
        {
            INodo nodoActual = nodos[posicionNodo];
            int filaActual = posicionNodo / 4;
            int columnaActual = posicionNodo % 4;
            for (int posicionVinculo = 0; posicionVinculo < nodos.Count; posicionVinculo++)
            {
                if (posicionNodo == posicionVinculo)
                    continue;

                INodo nodoVinculo = nodos[posicionVinculo];
                int filaVinculo = posicionVinculo / 4;
                int columnaVinculo = posicionVinculo % 4;

                if (columnaActual == columnaVinculo || filaActual == filaVinculo)
                    nodoActual.AgregarAdyacente(CrearArista(nodoVinculo));
            }
        }

        nodos[Indice4x4(0, 0)].AgregarAdyacente(CrearArista(nodos[Indice4x4(1, 1)]));
        nodos[Indice4x4(0, 1)].AgregarAdyacente(CrearArista(nodos[Indice4x4(1, 0)]));
        nodos[Indice4x4(1, 0)].AgregarAdyacente(CrearArista(nodos[Indice4x4(0, 1)]));
        nodos[Indice4x4(1, 1)].AgregarAdyacente(CrearArista(nodos[Indice4x4(0, 0)]));


        nodos[Indice4x4(0, 2)].AgregarAdyacente(CrearArista(nodos[Indice4x4(1, 3)]));
        nodos[Indice4x4(0, 3)].AgregarAdyacente(CrearArista(nodos[Indice4x4(1, 2)]));
        nodos[Indice4x4(1, 2)].AgregarAdyacente(CrearArista(nodos[Indice4x4(0, 3)]));
        nodos[Indice4x4(1, 3)].AgregarAdyacente(CrearArista(nodos[Indice4x4(0, 2)]));


        nodos[Indice4x4(2, 0)].AgregarAdyacente(CrearArista(nodos[Indice4x4(3, 1)]));
        nodos[Indice4x4(2, 1)].AgregarAdyacente(CrearArista(nodos[Indice4x4(3, 0)]));
        nodos[Indice4x4(3, 0)].AgregarAdyacente(CrearArista(nodos[Indice4x4(2, 1)]));
        nodos[Indice4x4(3, 1)].AgregarAdyacente(CrearArista(nodos[Indice4x4(2, 0)]));


        nodos[Indice4x4(2, 2)].AgregarAdyacente(CrearArista(nodos[Indice4x4(3, 3)]));
        nodos[Indice4x4(2, 3)].AgregarAdyacente(CrearArista(nodos[Indice4x4(3, 2)]));
        nodos[Indice4x4(3, 2)].AgregarAdyacente(CrearArista(nodos[Indice4x4(2, 3)]));
        nodos[Indice4x4(3, 3)].AgregarAdyacente(CrearArista(nodos[Indice4x4(2, 2)]));

        return nodos;
    }

    [Test]
    public void Test10SudokuDe4x4SeResuelveYTodasLasEntropiasSonUno()
    {
        List<INodo> nodos = CrearSudoku4x4();

        WaveFunctionCollapse.Ejecutar(ref nodos, _generadorDeNumeros);

        foreach (INodo nodo in nodos)
            Assert.AreEqual(1, nodo.Entropia());
    }

    [Test]
    public void Test11SudokuDe4x4ConAlgunosNodosColapsadosTerminaConTodosSusEntropiasEnUno()
    {
        List<INodo> nodos = CrearSudoku4x4();

        nodos[Indice4x4(0, 3)].Colapsar(new GeneradorElectivoPrueba(3));
        nodos[Indice4x4(1, 0)].Colapsar(new GeneradorElectivoPrueba(2));
        nodos[Indice4x4(2, 1)].Colapsar(new GeneradorElectivoPrueba(1));

        WaveFunctionCollapse.Ejecutar(ref nodos, _generadorDeNumeros);

        foreach (INodo nodo in nodos)
            Assert.AreEqual(1, nodo.Entropia());
    }
}
