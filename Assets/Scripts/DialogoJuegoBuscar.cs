using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoJuegoBuscar : MonoBehaviour
{
    public Text textoDialogo;
    public GameObject[] figuras;
    private List<GameObject> figurasAleatorias = new List<GameObject>();
    private int indiceActual = 0;
    //private bool esperandoToque = false;

    private void Start()
    {
        GenerarFigurasAleatorias();
        MostrarNuevoDialogo();
    }

    private void GenerarFigurasAleatorias()
    {
        figurasAleatorias.AddRange(figuras);
        figurasAleatorias.Shuffle(); // Mezcla la lista de figuras de manera aleatoria
    }

    private void MostrarNuevoDialogo()
    {
        if (indiceActual < figurasAleatorias.Count)
        {
            textoDialogo.text = "Selecciona " + figurasAleatorias[indiceActual].name;
        }
        else
        {
            textoDialogo.text = "¡Has completado todas las figuras!";
        }
    }

    public void VerificarSeleccion(GameObject figuraSeleccionada)
    {
        Debug.Log(figuraSeleccionada + "   " + figurasAleatorias[indiceActual]);
        if (indiceActual < figurasAleatorias.Count && figuraSeleccionada == figurasAleatorias[indiceActual])
        {
            Debug.Log("Figuras Iguales");
            //textoDialogo.text = "¡Genial! ¡La has encontrado!";
            Debug.Log("Genial");
            indiceActual++;
            if (Input.GetMouseButtonDown(0))
            {
                MostrarNuevoDialogo();
            }
        }
        else
        {
            Debug.Log("No son figuras iguales");
            //textoDialogo.text = "Vuelve a intentarlo. Toca la pantalla para continuar.";
            Debug.Log("Volver a intentar");
            if (Input.GetMouseButtonDown(0))
            {
                MostrarNuevoDialogo();
            }
            //esperandoToque = true;
        }
    }

    /*private void Update()
    {
        if (esperandoToque && Input.GetMouseButtonDown(0))
        {
            esperandoToque = false;
            MostrarNuevoDialogo();
        }
    }*/

}

// Nueva clase para generar de manera aleatoria las figuras qu se van a ir pidiendo
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
