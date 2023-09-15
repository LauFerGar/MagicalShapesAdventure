using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoMemoria : MonoBehaviour
{
    public Vector3[] posicionesAltar;
    public GameObject[] animales;
    public GameObject[] altares;
    public bool haFallado = false;
    public bool finalizado = false;
    public int nivel = 1;

    public Material materialAltarAcierto;
    public Material materialAltarError;

    public Button JuegoBuscar, JuegoSonido, JuegoMem, BotonVolver, PausaMem, contador;
    public Image FondoJuego, Error1, Error2, Error3, Error4;

    private int tiempoRestante = 11;
    private bool juegoIniciado = false;
    private Vector3[] posicionesIniciales;
    private GameObject animalSeleccionadoEnAltar;
    private bool animalesEnAltar = true;
    private int animalesCorrectos = 0;
    private List<GameObject> animalesEnJuego = new List<GameObject>();
    private List<int> indicesSeleccionados = new List<int>();
    private int numFallos = 0;



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
            int indiceAleatorio;

            do
            {
                indiceAleatorio = Random.Range(0, animales.Length);
            }
            while (indicesSeleccionados.Contains(indiceAleatorio));

            GameObject animalSeleccionado = animales[indiceAleatorio];

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
                animalesEnJuego[i].transform.localPosition = animalesEnJuego[i].GetComponent<AnimalSeleccionadoMemoria>().posicionOriginal;
                animalesEnJuego[i].GetComponent<BoxCollider>().enabled = true;
            }

            if (nivel >= 3)
            {
                int cantidadAnimalesExtra = nivel - 2;

                for (int i = 0; i < cantidadAnimalesExtra; i++)
                {
                    int indiceAleatorio;

                    do
                    {
                        indiceAleatorio = Random.Range(0, animales.Length);
                    }
                    while (indicesSeleccionados.Contains(indiceAleatorio));

                    GameObject animalSeleccionado = animales[indiceAleatorio];

                    animalSeleccionado.SetActive(true);

                    // Añadimos el animal seleccionado
                    animalesEnJuego.Add(animalSeleccionado);
                    indicesSeleccionados.Add(indiceAleatorio);

                    animalSeleccionado.transform.localPosition = animalSeleccionado.GetComponent<AnimalSeleccionadoMemoria>().posicionOriginal;
                    animalSeleccionado.GetComponent<BoxCollider>().enabled = true;
                }
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

        if (animalesCorrectos == 4)
        {
            for (int i = 0; i < animalesEnJuego.Count; i++)
            {
                animalesEnJuego[i].GetComponent<BoxCollider>().enabled = false;
            }

            finalizado = true;
            Debug.Log("¡Has colocado todos los animales en el altar correcto! Partida terminado.");
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

        if (nivel <= 2)
        {
            contador.gameObject.SetActive(true);
            contador.GetComponentInChildren<Text>().text = "Nivel " + nivel+1;

            foreach (GameObject animal in animales)
            {
                animal.SetActive(false);
            }

            animalesEnJuego.Clear();
            indicesSeleccionados.Clear();

            for (int i = 0; i < 4; i++)
            {
                int indiceAleatorio;

                do
                {
                    indiceAleatorio = Random.Range(0, animales.Length);
                }
                while (indicesSeleccionados.Contains(indiceAleatorio));

                GameObject animalSeleccionado = animales[indiceAleatorio];

                animalSeleccionado.SetActive(true);

                //Añadimos el animal seleccionado
                animalesEnJuego.Add(animalSeleccionado);
                indicesSeleccionados.Add(indiceAleatorio);
            }

            PosicionarAnimalesAleatoriamente();

            contador.onClick.RemoveListener(ComenzarJuego);
            contador.onClick.AddListener(ComenzarJuego);
        }
        else
        {          
            contador.gameObject.SetActive(true);
            contador.GetComponentInChildren<Text>().text = "Finalizado";

            contador.onClick.RemoveListener(ComenzarJuego);
            contador.onClick.AddListener(FinalizarJuego);
        }
    }

    public void falloAnimal()
    {
        numFallos++;

        switch(numFallos)
        {
            case 1:
                Error1.gameObject.SetActive(true);
                break;
            case 2:
                Error2.gameObject.SetActive(true);
                break;
            case 3:
                Error3.gameObject.SetActive(true);
                break;
            case 4:
                Error4.gameObject.SetActive(true);
                contador.gameObject.SetActive(true);
                contador.GetComponentInChildren<Text>().text = "Finalizado";

                foreach (GameObject altar in altares)
                {
                    altar.GetComponent<BoxCollider>().enabled = false;
                    altar.GetComponent<SeleccionarAltar>().RestaurarMaterial();
                }

                contador.onClick.RemoveListener(ComenzarJuego);
                contador.onClick.AddListener(FinalizarJuego);
                break;
        }

    }

    public void FinalizarJuego()
    {
        finalizado = false;
        juegoIniciado = false;
        tiempoRestante = 11;
        animalesCorrectos = 0;
        nivel = 0;
        haFallado = false;
        numFallos = 0;

        FondoJuego.gameObject.SetActive(true);
        JuegoBuscar.gameObject.SetActive(true);
        JuegoSonido.gameObject.SetActive(true);
        JuegoMem.gameObject.SetActive(true);
        BotonVolver.gameObject.SetActive(true);

        Debug.Log("Activado el menu juegos");

        contador.GetComponentInChildren<Text>().text = "Comenzar";
        contador.gameObject.SetActive(false);
        PausaMem.gameObject.SetActive(false);

        Error1.gameObject.SetActive(false);
        Error2.gameObject.SetActive(false);
        Error3.gameObject.SetActive(false);
        Error4.gameObject.SetActive(false);

        Debug.Log("Desactivado juego Memoria");

        Collider tableroBoxCollider = GetComponent<BoxCollider>();
        Collider tableroMeshCollider = GetComponent<MeshCollider>();
        if (tableroBoxCollider != null && tableroMeshCollider != null)
        {
            tableroBoxCollider.enabled = false;
            tableroMeshCollider.enabled = false;
        }

        animalesEnJuego.Clear();
        indicesSeleccionados.Clear();

        contador.onClick.RemoveListener(FinalizarJuego);
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

        for (int i = 0; i < 4; i++)
        {
            int indiceAleatorio;

            do
            {
                indiceAleatorio = Random.Range(0, animales.Length);
            }
            while (indicesSeleccionados.Contains(indiceAleatorio));

            GameObject animalSeleccionado = animales[indiceAleatorio];

            animalSeleccionado.SetActive(true);

            //Añadimos el animal seleccionado
            animalesEnJuego.Add(animalSeleccionado);
            indicesSeleccionados.Add(indiceAleatorio);
        }

        PosicionarAnimalesAleatoriamente();

    }
}
