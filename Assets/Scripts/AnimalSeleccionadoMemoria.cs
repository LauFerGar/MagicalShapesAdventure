using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSeleccionadoMemoria : MonoBehaviour
{
    public bool enElAltar = false;
    public Vector3 posicionAltarQueHaTocado;
    public GameObject[] altares;
    public Vector3 posicionOriginal;

    private GameObject altarOcupadoConAnimal;

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
                    if (posicionAltarQueHaTocado == altarSeleccionado.transform.localPosition)
                    {
                        // Colocamos el animal en el altar seleccionado.
                        Transform posicionAltar = altarSeleccionado.transform;
                        transform.localPosition = posicionAltar.localPosition;
                        enElAltar = true;
                        if (posicionAltarQueHaTocado == posicionAltar.localPosition)
                        {
                            //Debug.Log("Has acertado el animal " + gameObject.name + " en el altar");

                            // Notificamos a JuegoMemoria que un animal fue colocado correctamente
                            JuegoMemoria juegoMem = GetComponentInParent<JuegoMemoria>();
                            juegoMem.AnimalColocadoEnAltarCorrecto();

                            altarSeleccionado.GetComponent<Renderer>().material = juegoMem.materialAltarAcierto;
                        }
                        else
                        {
                            //Debug.Log("No has acertado el animal");
                            JuegoMemoria juegoMem = GetComponentInParent<JuegoMemoria>();
                            juegoMem.haFallado = true;

                            juegoMem.falloAnimal();

                            altarSeleccionado.GetComponent<Renderer>().material = juegoMem.materialAltarError;
                        }

                        // Restaura el material del altar y deselecciónalo.
                        //altar.DeseleccionarAltar(); // Llama al nuevo método DeseleccionarAltar.
                    }
                    else
                    {
                        Debug.Log("No has acertado el animal");
                        JuegoMemoria juegoMem = GetComponentInParent<JuegoMemoria>();
                        juegoMem.haFallado = true;

                        juegoMem.falloAnimal();

                        altarSeleccionado.GetComponent<Renderer>().material = juegoMem.materialAltarError;
                    }

                }
            }
        }
        else
        {

            JuegoMemoria juegoMem = GetComponentInParent<JuegoMemoria>();
            foreach (GameObject altar in altares)
            {
                if (altar != null && transform.localPosition == altar.transform.localPosition)
                {
                    juegoMem.AnimalSeleccionadoEnAltar(this.gameObject);
                    altar.GetComponent<SeleccionarAltar>().CambiarMaterial();
                    altarOcupadoConAnimal = altar;
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
        altarOcupadoConAnimal.GetComponent<SeleccionarAltar>().DeseleccionarAltar();
    }

}
