using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoMemoria : MonoBehaviour
{
    public Vector3[] posicionesAltar;
    public GameObject[] animales;
    public Button contador;

    private int tiempoRestante = 11;
    private bool juegoIniciado = false;
    private Text contadorTexto;
    private Vector3[] posicionesIniciales;

    private void Awake()
    {
        InicializarPosicionesAltar();
    }

    void Start()
    {

        contador.onClick.AddListener(ComenzarJuego);

        posicionesIniciales = new Vector3[animales.Length];
        /*for (int i = 0; i<animales.Length; i++)
        {
            posicionesIniciales[i] = animales[i].transform.localPosition;
        }*/

        PosicionarAnimalesAleatoriamente();
    }

    public void ComenzarJuego()
    {
        if (!juegoIniciado)
        {
            juegoIniciado = true;
            InvokeRepeating("ActualizarContador", 0f, 1f);
        }
    }

    private void ActualizarContador()
    {
        tiempoRestante--;

        if (tiempoRestante>0)
        {
            contador.GetComponentInChildren<Text>().text = tiempoRestante.ToString();
        }
        else
        {
            CancelInvoke("ActualizarContador");
            contador.gameObject.SetActive(false);

            for (int i = 0; i<animales.Length; i++)
            {
                animales[i].transform.localPosition = posicionesIniciales[i];
            }
        }
    }

    private void InicializarPosicionesAltar()
    {
        posicionesAltar = new Vector3[]
        {
            new Vector3(-3.67000008f,0.0529999994f,-3.48000002f),
            new Vector3(-1.26999998f,0.0529999994f,-3.48000002f),
            new Vector3(1.11000001f,0.0529999994f,-3.48000002f),
            new Vector3(3.54999995f,0.0529999994f,-3.48000002f),
        };
    }

    private void PosicionarAnimalesAleatoriamente()
    {
        List<int> posicionesDisponibles = new List<int>();

        for (int i=0; i<posicionesAltar.Length; i++)
        {
            posicionesDisponibles.Add(i);
        }

        //Posicionamos aleatoriamente a cada animal en uno de los 4 altares
        for (int i=0; i<animales.Length; i++)
        {
            // Guardamos la posición inicial del animal
            posicionesIniciales[i] = animales[i].transform.localPosition;

            if (posicionesDisponibles.Count ==0)
            {
                Debug.LogError("No hay suficientes posiciones disponibles para todos los animales.");
                break;
            }

            int indiceAleatorio = Random.Range(0, posicionesDisponibles.Count);
            int indicePosicion = posicionesDisponibles[indiceAleatorio];
            posicionesDisponibles.RemoveAt(indiceAleatorio);

            //Asignamos el animal al altar
            animales[i].transform.localPosition = posicionesAltar[indicePosicion];

        }
    }
}
