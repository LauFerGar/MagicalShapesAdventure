using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoMemoria : MonoBehaviour
{
    public Vector3[] posicionesAltar;
    public GameObject[] animales;
    public GameObject[] altares;
    public Button contador;
    public bool haFallado = false;
    public bool finalizado = false;
    public int nivel = 1;

    public Material materialAltarAcierto;
    public Material materialAltarError;

    private int tiempoRestante = 11;
    private bool juegoIniciado = false;
    private Vector3[] posicionesIniciales;
    private GameObject animalSeleccionadoEnAltar;
    private bool animalesEnAltar = true;
    private int animalesCorrectos = 0;
    private List<GameObject> animalesEnJuego = new List<GameObject>();
    private List<int> indicesSeleccionados = new List<int>();

    private void Awake()
    {
        InicializarPosicionesAltar();
    }

    void Start()
    {
        Collider tableroBoxCollider = GetComponent<BoxCollider>();
        Collider tableroMeshCollider = GetComponent<MeshCollider>();
        if (tableroBoxCollider != null && tableroMeshCollider != null)
        {
            tableroBoxCollider.enabled = false;
            tableroMeshCollider.enabled = false;
        }

        contador.onClick.AddListener(ComenzarJuego);

        posicionesIniciales = new Vector3[animales.Length];

        //Nos aseguramos que existen 8 animales en el tablero
        if (animales.Length < 8)
        {
            Debug.LogError("No hay suficientes animales disponibles");
            return;
        }

        foreach (GameObject animal in animales)
        {
            animal.SetActive(false);
        }

        for (int i = 0; i<4; i++)
        {
            //int indiceAleatorio = Random.Range(0, animales.Length);
            int indiceAleatorio;

            do
            {
                indiceAleatorio = Random.Range(0, animales.Length);
            }
            while (indicesSeleccionados.Contains(indiceAleatorio));

            GameObject animalSeleccionado = animales[indiceAleatorio];

            //Debug.Log("Animal Seleccionado: " + animalSeleccionado.name);

            animalSeleccionado.SetActive(true);

            //Añadimos el animal seleccionado
            animalesEnJuego.Add(animalSeleccionado);
            indicesSeleccionados.Add(indiceAleatorio);
        }

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

        if (tiempoRestante > 0)
        {
            contador.GetComponentInChildren<Text>().text = tiempoRestante.ToString();
        }
        else
        {
            CancelInvoke("ActualizarContador");
            contador.gameObject.SetActive(false);

            for (int i = 0; i < animalesEnJuego.Count; i++)
            {
                /*animales[i].transform.localPosition = posicionesIniciales[i];
                animales[i].GetComponent<BoxCollider>().enabled = true;*/

                animalesEnJuego[i].transform.localPosition = animalesEnJuego[i].GetComponent<AnimalSeleccionadoMemoria>().posicionOriginal;
                animalesEnJuego[i].GetComponent<BoxCollider>().enabled = true;
            }

            for (int i = 0; i < altares.Length; i++)
            {
                altares[i].GetComponent<BoxCollider>().enabled = true;
            }

            animalesEnAltar = false;
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

        for (int i = 0; i < posicionesAltar.Length; i++)
        {
            posicionesDisponibles.Add(i);
        }

        //Posicionamos aleatoriamente a cada animal en uno de los 4 altares
        for (int i = 0; i < animalesEnJuego.Count; i++)
        {

            if (posicionesDisponibles.Count == 0)
            {
                Debug.LogError("No hay suficientes posiciones disponibles para todos los animales.");
                break;
            }

            int indiceAleatorio = Random.Range(0, posicionesDisponibles.Count);
            int indicePosicion = posicionesDisponibles[indiceAleatorio];
            posicionesDisponibles.RemoveAt(indiceAleatorio);

            //Asignamos el animal al altar
            animalesEnJuego[i].transform.localPosition = posicionesAltar[indicePosicion];
            animalesEnJuego[i].GetComponent<AnimalSeleccionadoMemoria>().posicionAltarQueHaTocado = posicionesAltar[indicePosicion];
        }
    }

    private void Update()
    {
        Collider tableroBoxCollider = GetComponent<BoxCollider>();
        Collider tableroMeshCollider = GetComponent<MeshCollider>();
        if (!haFallado)
        {
            tableroBoxCollider.enabled = false;
            tableroMeshCollider.enabled = false;
        }
        else
        {
            tableroBoxCollider.enabled = true;
            tableroMeshCollider.enabled = true;
        }

        if (animalesEnAltar)
        {
            for (int i = 0; i < animales.Length; i++)
            {
                animales[i].GetComponent<BoxCollider>().enabled = false;
            }
            for (int i = 0; i < altares.Length; i++)
            {
                altares[i].GetComponent<BoxCollider>().enabled = false;
            }
        }

        // Verifica si el usuario hizo clic en el tablero.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Si el dedo golpea el tablero, encuentra y devuelve el animal seleccionado en el altar.
                if (hit.collider.gameObject == gameObject && finalizado == false)
                {
                    DevolverAnimalSeleccionadoAlTablero();
                }
            }
        }

    }

    private void DevolverAnimalSeleccionadoAlTablero()
    {
        //animalSeleccionadoEnAltar.GetComponent<AnimalSeleccionadoMemoria>().DevolverAlTablero();

        AnimalSeleccionadoMemoria animalSeleccionado = animalSeleccionadoEnAltar.GetComponentInChildren<AnimalSeleccionadoMemoria>();
        if (animalSeleccionado != null && animalSeleccionado.enElAltar)
        {
            animalSeleccionado.DevolverAlTablero();
        }
    }

    public void AnimalSeleccionadoEnAltar(GameObject animal)
    {
        animalSeleccionadoEnAltar = animal;
    }

    public void AnimalColocadoEnAltarCorrecto()
    {
        animalesCorrectos++;

        //Debug.Log( animalesCorrectos + " animales correctos");

        if (animalesCorrectos == animalesEnJuego.Count)
        {
            for (int i = 0; i < animalesEnJuego.Count; i++)
            {
                animalesEnJuego[i].GetComponent<BoxCollider>().enabled = false;
            }

            finalizado = true;
            Debug.Log("¡Has colocado todos los animales en el altar correcto! Juego terminado.");
        }

        //Verificamos si el juego ha finalizado para volcer a mostrar el cartel indicando que se puede comenzar el siguiente nivel
        if (finalizado)
        {
            NuevoNivel();
            haFallado = false;
        }
    }

    private void NuevoNivel()
    {
        finalizado = false;
        juegoIniciado = false;
        tiempoRestante = 11;
        animalesCorrectos = 0;
        nivel++;

        if (nivel < 3)
        {
            contador.gameObject.SetActive(true);
            contador.GetComponentInChildren<Text>().text = "Next Lvl";

            foreach (GameObject animal in animales)
            {
                animal.GetComponent<Renderer>().enabled = false;
            }

            for (int i = 0; i < 4; i++)
            {
                int indiceAleatorio = Random.Range(0, animales.Length);
                GameObject animalSeleccionado = animales[indiceAleatorio];

                //Añadimos el animal seleccionado
                animalesEnJuego.Add(animalSeleccionado);
            }

            PosicionarAnimalesAleatoriamente();

            contador.onClick.RemoveListener(ComenzarJuego);
            contador.onClick.AddListener(ComenzarJuego);
        }
        else
        {
            contador.gameObject.SetActive(true);
            contador.GetComponentInChildren<Text>().text = "Finalizado";

            contador.interactable = false;
        }
    }
}
