using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSeleccionadoMemoria : MonoBehaviour
{
    public bool enElAltar = false;
    private Material materialAltarOriginal;
    private Vector3 posicionOriginal;
    private SeleccionarAltar altarOcupadoConAnimal;

    private void Awake()
    {
        //Guardamos la posición original del animal
        posicionOriginal = transform.localPosition;
    }

    private void OnMouseDown()
    {
        if (transform.localPosition==posicionOriginal)
        {
            // Comprueba si hay un altar seleccionado.
            SeleccionarAltar[] altares = FindObjectsOfType<SeleccionarAltar>();
            foreach (SeleccionarAltar altar in altares)
            {
                GameObject altarSeleccionado = altar.ObtenerAltarSeleccionado();
                if (altarSeleccionado != null)
                {
                    // Coloca el animal en el altar seleccionado.
                    Transform posicionAltar = altarSeleccionado.transform;
                    transform.position = posicionAltar.position;
                    enElAltar = true;

                    // Restaura el material del altar y deselecciónalo.
                    altar.DeseleccionarAltar(); // Llama al nuevo método DeseleccionarAltar.
                }
            }
        }
        else
        {
            SeleccionarAltar[] altares = FindObjectsOfType<SeleccionarAltar>();
            foreach (SeleccionarAltar altar in altares)
            {
                GameObject altarAnimal = altar.gameObject;
                Vector3 posicionAltarAnimal = altarAnimal.transform.localPosition;
                if (altarAnimal != null && posicionAltarAnimal == this.transform.localPosition)
                {
                    altar.CambiarMaterial();
                    altarOcupadoConAnimal = altar;
                    JuegoMemoria juegoMem = GetComponentInParent<JuegoMemoria>();
                    juegoMem.AnimalSeleccionadoEnAltar(gameObject);
                }
            }
        }
        
    }

    public void DevolverAlTablero()
    {
        // Devuelve el animal a su posición original en el tablero.
        transform.localPosition = posicionOriginal;
        enElAltar = false;

        //Cambiamos color del altar
        altarOcupadoConAnimal.CambiarMaterial();
    }

}
