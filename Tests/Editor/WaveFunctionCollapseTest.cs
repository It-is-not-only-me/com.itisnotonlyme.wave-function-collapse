using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ItIsNotOnlyMe.WaveFunctionCollapse;

public class EstadoPrueba : IEstado
{
    public int Numero { get; }

    public EstadoPrueba(int numero)
    {
        Numero = numero;
    }

    public bool SeElimina(List<IEstado> estados, IValor valor)
    {
        bool seElimina = estados.Count > 0;
        foreach (IEstado estado in estados)
        {
            EstadoPrueba estadoPrueba = estado as EstadoPrueba;
            seElimina &= Numero == estadoPrueba.Numero;
        }
        return seElimina;
    }
}

public class GeneradorInicioPrueba : IGeneradorDeNumeros
{
    public int Numero(int final, int inicio = 0, int separacion = 1)
    {
        return inicio;
    }
}

public class ValorPruba : IValor
{

}

public class WaveFunctionCollapseTest
{
    private IGeneradorDeNumeros _generadorDeNumeros = new GeneradorInicioPrueba();

    private IEstado _estado1 = new EstadoPrueba(1);
    private IEstado _estado2 = new EstadoPrueba(2);
    private IEstado _estado3 = new EstadoPrueba(3);
    private IEstado _estado4 = new EstadoPrueba(4);

    private IValor _valor = new ValorPruba();

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
}
