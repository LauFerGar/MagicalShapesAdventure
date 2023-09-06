using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionarAltar : MonoBehaviour
{
    private Material materialOriginal;
    private GameObject altarSeleccionado;

    public Material materialSeleccionado;


    void Start()
    {
        //Guardamos el material original del altar antes de ser seleccionado
        materialOriginal = GetComponent<Renderer>().material;
    }

    private void OnMouseDown()
    {
        // Cambia el material cuando se hace clic en el altar.
        CambiarMaterial();

        // Almacena el altar seleccionado.
        altarSeleccionado = gameObject;
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
        // Restaura el material y anula la selección del altar.
        altarSeleccionado = null;
        RestaurarMaterial();
    }

}
