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
    private Vector3[] posicionesIniciales;

    private GameObject animalSeleccionadoEnAltar;

    private void Awake()
    {
        InicializarPosicionesAltar();
    }

    void Start()
    {

        contador.onClick.AddListener(ComenzarJuego);

        posicionesIniciales = new Vector3[animales.Length];

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

    private void Update()
    {
        // Verifica si el usuario hizo clic en el tablero.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Si el rayo golpea el tablero, encuentra y devuelve el animal seleccionado en el altar.
                if (hit.collider.gameObject == gameObject)
                {
                    DevolverAnimalSeleccionadoAlTablero();
                }
            }
        }
    }

    private void DevolverAnimalSeleccionadoAlTablero()
    {
        /*// Busca el animal seleccionado en el altar y lo devuelve al tablero.
        foreach (GameObject animal in animales)
        {
            AnimalSeleccionadoMemoria animalScript = animal.GetComponent<AnimalSeleccionadoMemoria>();
            if (animalScript != null && animalScript.enElAltar)
            {
                animalScript.DevolverAlTablero();
                break; // Detiene la búsqueda después de devolver el primer animal seleccionado.
            }
        }*/

        animalSeleccionadoEnAltar.GetComponent<AnimalSeleccionadoMemoria>().DevolverAlTablero();

    }

    public void AnimalSeleccionadoEnAltar(GameObject animal)
    {
        animalSeleccionadoEnAltar = animal;
    }
}
