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

    private List<INodo> CrearBloqueSudoku()
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
        return nodos;
    }

    private List<INodo> CrearSudoku()
    {
        List<INodo> sudoku = new List<INodo>();

        List<List<INodo>> bloquesNueve = new List<List<INodo>>();

        for (int i = 0; i < 9; i++)
            bloquesNueve.Add(CrearBloqueSudoku());

        foreach (List<INodo> bloqueNueve in bloquesNueve)
            foreach (INodo nodo in bloqueNueve)
                sudoku.Add(nodo);

        // vincular filas
        for (int bloqueI = 0; bloqueI < 3; bloqueI++)
            for (int bloqueJ = 0; bloqueJ < 3; bloqueJ++)
                for (int celdaI = 0; celdaI < 3; celdaI++)
                    for (int celdaJ = 0; celdaJ < 3; celdaJ++)
                    {
                        INodo nodoActual = bloquesNueve[bloqueI + 3 * bloqueJ][celdaI + 3 * celdaJ];

                        Debug.Log("Estando en bloque: " + bloqueI + ", " + bloqueJ + ", celda: " + celdaI + ", " + celdaJ);
                        for (int i = 0; i < 3; i++)
                        {
                            if (i == bloqueI)
                                continue;
                            for (int j = 0; j < 3; j++)
                            {
                                INodo nodoAVincular = bloquesNueve[bloqueI + 3 * i][celdaI + 3 * j];
                                Debug.Log("Bloque: " + bloqueI + ", " + i + ", celda: " + celdaI + ", " + j);

                                nodoActual.AgregarAdyacente(new Arista(nodoAVincular, _valor));
                            }
                        }
                    }

        // vincular columnas
        for (int bloqueI = 0; bloqueI < 3; bloqueI++)
            for (int bloqueJ = 0; bloqueJ < 3; bloqueJ++)
                for (int celdaI = 0; celdaI < 3; celdaI++)
                    for (int celdaJ = 0; celdaJ < 3; celdaJ++)
                    {
                        INodo nodoActual = bloquesNueve[bloqueI + 3 * bloqueJ][celdaI + 3 * celdaJ];
                        for (int i = 0; i < 3; i++)
                        {
                            if (i == bloqueJ)
                                continue;
                            for (int j = 0; j < 3; j++)
                            {
                                INodo nodoAVincular = bloquesNueve[i + 3 * bloqueJ][j + 3 * celdaJ];
                                nodoActual.AgregarAdyacente(new Arista(nodoAVincular, _valor));
                            }
                        }
                    }

        return sudoku;
    }

    [Test]
    public void Test10SeCreaUnSudokuYLaEntropiaEsUnoYEstaEnUnEstadoValido()
    {
        List<INodo> nodos = CrearSudoku();
        WaveFunctionCollapse.Ejecutar(ref nodos, _generadorDeNumeros);
        //int contador = 1;
        foreach (INodo nodo in nodos)
        {
            Assert.AreEqual(1, nodo.Entropia());
            EstadoPrueba estadoNodo = nodo.EstadoFinal() as EstadoPrueba;
            //Assert.AreEqual((contador++) % 9, estadoNodo.Numero);
            //Debug.Log(estadoNodo.Numero);
        }
    }
}
