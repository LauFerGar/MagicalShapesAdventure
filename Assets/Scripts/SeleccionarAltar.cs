using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionarAltar : MonoBehaviour
{
    private Material materialOriginal;
    private GameObject altarSeleccionado;
    private JuegoMemoria juegoMem;
    private int nivelJuego;

    public Material materialSeleccionado;

    void Start()
    {
        //Guardamos el material original del altar antes de ser seleccionado
        materialOriginal = GetComponent<Renderer>().material;

        juegoMem = GetComponentInParent<JuegoMemoria>();
        nivelJuego = juegoMem.nivel;
    }

    private void OnMouseDown()
    {
        if (GetComponent<Renderer>().material == materialOriginal)
        {
            // Almacena el altar seleccionado.
            altarSeleccionado = gameObject;
        }
        else
        {
            altarSeleccionado = null;
        }

        // Cambia el material cuando se hace clic en el altar.
        CambiarMaterial();
    }

    public void CambiarMaterial()
    {
        // Comprueba si ya hay un altar seleccionado en otro lugar.
        SeleccionarAltar[] altares = FindObjectsOfType<SeleccionarAltar>();
        foreach (SeleccionarAltar altar in altares)
        {
            if (altar != this)
            {
                altar.DeseleccionarAltar();
            }
        }

        if (GetComponent<Renderer>().material == materialOriginal)
        {
            // Cambia el material del altar actual.
            GetComponent<Renderer>().material = materialSeleccionado;
        }
        else
        {
            // Cambia el material del altar actual.
            GetComponent<Renderer>().material = materialOriginal;
        }

        
    }

    public void RestaurarMaterial()
    {
        //Restauramos el material original del altar
        GetComponent<Renderer>().material = materialOriginal;
    }

    public GameObject ObtenerAltarSeleccionado()
    {
        
        return altarSeleccionado;
    }

    public void DeseleccionarAltar()
    {
        //Debug.Log("Altar " + this.name + " deseleccionado");

        // Restaura el material y anula la selección del altar.
        altarSeleccionado = null;

        RestaurarMaterial();
    }

    void Update()
    {
        if (nivelJuego != juegoMem.nivel)
        {
            DeseleccionarAltar();
            Debug.Log("Altares deseleccionador por finalizacion de partida");

            nivelJuego = juegoMem.nivel;
        }
    }

}
