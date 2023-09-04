using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastrarAnimal : MonoBehaviour
{
    private Vector3 posicionInicial;
    //private Vector3[] posicionesA;
    //private JuegoMemoria juegoMem;
    //private bool arrastrando = false;

    //public GameObject tablero;

    void Start()
    {
        JuegoMemoria juegoMemoria = GetComponentInParent<JuegoMemoria>();

        if (juegoMemoria != null)
        {
            Vector3[] arrayRecibido = juegoMemoria.posicionesAltar;

            foreach (Vector3 vector in arrayRecibido)
            {
                Debug.Log("Vector3 en ArrastrarAnimal: " + vector.ToString());
            }
        }
        else
        {
            Debug.LogError("No se encontr� JuegoMemoria en los padres de Gallina.");
        }

        posicionInicial = transform.localPosition;
        //CargarPosicionesAltar();

    }

    private void OnMouseDown()
    {
        //Cuando el usuario toca la pantalla comienza a arrastrar
        //arrastrando = true;

        //Debug.Log(transform.localPosition);
    }



    private void OnMouseUp()
    {
        /*//Cuando el usuario levanta el dedo, verificamos si estamos en la posici�n del altar
        JuegoMemoria juegoMemoria = GetComponentInParent<JuegoMemoria>();
        Vector3 primeraPosici�nAltar = juegoMemoria.posicionesAltar[0];

        //Si el gameObject est� cerca de la posici�n deseada, se coloca ah�
        if (Vector3.Distance(transform.localPosition, primeraPosici�nAltar)<0.1f)
        {
            transform.localPosition = primeraPosici�nAltar;
        }
        else
        {
            //Si no est� cerca vuelve a la posici�n inicial
            transform.localPosition = posicionInicial;
        }*/
    }

    /*void CargarPosicionesAltar()
    {
        juegoMem = tablero.GetComponent<JuegoMemoria>();

        //Debug.Log("Inicializamos el altar en ArrastrarAnimal");

        /*for (int i=0; i<juegoMem.posicionesAltar.Length; i++)
        {
            posicionesA[0] = juegoMem.posicionesAltar[0];

            Debug.Log("Posicion 0 en ArrastrarAnimal" + posicionesA[0]);
        }

        foreach (Vector3 posicion in juegoMem.posicionesAltar)
        {
            Debug.Log(posicion);
        }

    }*/
    
}
