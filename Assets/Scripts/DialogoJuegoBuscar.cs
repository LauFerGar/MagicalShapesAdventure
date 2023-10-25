using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoJuegoBuscar : MonoBehaviour
{
    public Text textoDialogo;
    public GameObject[] figuras;
    private List<GameObject> figurasAleatorias = new List<GameObject>();
    private int indiceActual = 0, numeroAdivinar = 6;
    public Button dialogo, volverJuegos, pausaBuscar, juegoBuscar, juegoSonido, juegoMemoria, botonVolver;
    public Image fondo;
    public GenerarPosiciones generarPos;

    private bool finalizado = false;

    private void Start()
    {
        GenerarFigurasAleatorias();
        MostrarNuevoDialogo();

        dialogo.onClick.AddListener(SeguirDialogo);
        volverJuegos.onClick.AddListener(ReseteoFiguras);
    }

    private void GenerarFigurasAleatorias()
    {
        figurasAleatorias.AddRange(figuras);
        figurasAleatorias.MezclaFiguras(); // Mezcla la lista de figuras de manera aleatoria
    }

    private void MostrarNuevoDialogo()
    {
        if (indiceActual < numeroAdivinar)
        {
            if (figurasAleatorias[indiceActual].name.Contains("Esfera") || figurasAleatorias[indiceActual].name.Contains("Estrella"))
            {
                textoDialogo.text = "Busca la " + figurasAleatorias[indiceActual].name;
            }
            else
            {
                textoDialogo.text = "Busca el " + figurasAleatorias[indiceActual].name;
            }
            
        }
        else if (indiceActual == numeroAdivinar && finalizado == false)
        {
            textoDialogo.text = "Juego Finalizado";
            FinJuego();
        }
    }

    public void VerificarSeleccion(GameObject figuraSeleccionada)
    {
        if (indiceActual < numeroAdivinar && figuraSeleccionada == figurasAleatorias[indiceActual])
        {
            if (indiceActual == numeroAdivinar)
            {
                MostrarNuevoDialogo();
            }
            else
            {
                textoDialogo.text = "¡Genial! ¡Pulsa aqui para buscar!";
                indiceActual++;
                generarPos.GenerarPosicionesAleatorias();
                
            }

        }
        else
        {
            textoDialogo.text = "Vuelve a intentarlo.";

        }
    }

    private void SeguirDialogo()
    {
        MostrarNuevoDialogo();
    }

    private void ReseteoFiguras()
    {
        generarPos.GenerarPosicionesAleatorias();
    }

    private void FinJuego()
    {
        dialogo.onClick.RemoveListener(SeguirDialogo);

        textoDialogo.text = "Juego Finalizado";

        foreach (GameObject figura in figuras)
        {
            if (figura.GetComponent<BoxCollider>() != null)
            {
                figura.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                figura.GetComponent<SphereCollider>().enabled = false;
            }
            
        }

        dialogo.onClick.AddListener(FinalizarJuego);
    }
    
    public void FinalizarJuego()
    {
        indiceActual = 0;
        finalizado = false;

        dialogo.onClick.RemoveListener(FinalizarJuego);

        dialogo.gameObject.SetActive(false);
        pausaBuscar.gameObject.SetActive(false);

        fondo.gameObject.SetActive(true);
        juegoBuscar.gameObject.SetActive(true);
        juegoSonido.gameObject.SetActive(true);
        juegoMemoria.gameObject.SetActive(true);
        botonVolver.gameObject.SetActive(true);

        ReseteoFiguras();
        MostrarNuevoDialogo();

        foreach (GameObject figura in figuras)
        {
            if (figura.GetComponent<BoxCollider>() != null)
            {
                figura.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                figura.GetComponent<SphereCollider>().enabled = true;
            }

        }

        if (figurasAleatorias[indiceActual].name.Contains("Esfera") || figurasAleatorias[indiceActual].name.Contains("Estrella"))
        {
            textoDialogo.text = "Busca la " + figurasAleatorias[indiceActual].name;
        }
        else
        {
            textoDialogo.text = "Busca el " + figurasAleatorias[indiceActual].name;
        }

        dialogo.onClick.AddListener(SeguirDialogo);
    }

}

// Nueva clase para generar de manera aleatoria las figuras que se van a ir pidiendo
public static class Listas
{
    public static void MezclaFiguras<T>(this IList<T> list)
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
