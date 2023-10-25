using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionBoton : MonoBehaviour
{
    private Vector3 posicionInicial;
    private float velocidad = 100.0f;
    private bool pulsado = false;
    private AudioSource[] sonidos;
    
    public GameObject[] animalesOK;
    public Button pausaSonido, juegoBuscar, juegoSonidos, juegoMemoria, volver, botonDialogo;
    public Image fondo;
    public Text dialogo;
    public Canvas canvas;

    private int adivinar;
    private int indiceSonidos = 0;
    private List<AudioClip> audiosSeleccionados = new List<AudioClip>();
    private GameObject[] todos;

    private void Start()
    {
        posicionInicial = transform.localPosition;

        sonidos = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource sonido in sonidos)
        {
            if (sonido.clip!=null)
            {
                string nombreClip = sonido.clip.name;
            }
            else
            {
                Debug.Log("El AudioSource no tiene un Clip con sonido de animal");
            }
            
        }

        adivinar = new GenerarAnimalesAleatorios().numAdivinar;

        ObtenerSonidosAnimalesSeleccionados(animalesOK);
    }

    public void ObtenerSonidosAnimalesSeleccionados(GameObject[] animalesGenerados)
    {
        foreach (GameObject animal in animalesGenerados)
        {
            string nombreAnimal = animal.name;

            foreach (AudioSource sonido in sonidos)
            {
                if (sonido.clip.name == nombreAnimal)
                {
                    audiosSeleccionados.Add(sonido.clip);
                    break;
                }
            }

        }
    }


    private void Update()
    {
        if (pulsado)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(posicionInicial.x, 1.0f, posicionInicial.z), velocidad * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, posicionInicial, velocidad * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        pulsado = true;
        if (indiceSonidos != adivinar)
        {
            ReproducirSonido();
        }
        else
        {
            FinJuego();
        }
    }

    private void OnMouseUp()
    {
        pulsado = false;
    }

    private void ReproducirSonido()
    {
        if (indiceSonidos < audiosSeleccionados.Count)
        {
            foreach (AudioSource sonido in sonidos)
            {
                if (audiosSeleccionados[indiceSonidos] == sonido.clip)
                {
                    sonido.Play();
                }
            }
        }
    }

    public void VerificarAnimalSonido(GameObject animalSeleccionado)
    {
        //Debug.Log("Se ha pulsado en el " + animalSeleccionado + ". El animal a comparar es: " + animalesOK[indiceActual]);

        if (audiosSeleccionados[indiceSonidos].name == animalSeleccionado.name)
        {
            //Debug.Log("Se ha pulsado en el " + animalSeleccionado + ". El animal a comparar es: " + animalesOK[indiceActual] + "y su sonido es: " + audiosSeleccionados[indiceSonidos].name);
            if (indiceSonidos == adivinar)
            {
                dialogo.text = "Has acertado el último. Juego Finalizado";
            }
            else
            {
                dialogo.text = "Has acertado el animal: " + audiosSeleccionados[indiceSonidos].name;
                indiceSonidos++;
            }
        }
        else
        {
            //Debug.Log("Ese no es el animal. Pulsa de nuevo");
            dialogo.text = "Animal incorrecto";
        }
    }


    public void FinJuego()
    {
        indiceSonidos = 0;
        audiosSeleccionados.Clear(); ;

        botonDialogo.gameObject.SetActive(false);
        pausaSonido.gameObject.SetActive(false);

        dialogo.text = "Pulsa PLAY para reproducir el sonido";

        fondo.gameObject.SetActive(true);
        juegoBuscar.gameObject.SetActive(true);
        juegoMemoria.gameObject.SetActive(true);
        juegoSonidos.gameObject.SetActive(true);
        volver.gameObject.SetActive(true);

        GenerarAnimalesAleatorios generar = FindAnyObjectByType<GenerarAnimalesAleatorios>();

        if (generar != null)
        {
            generar.JuegoFinalizado();
        }

        ObtenerSonidosAnimalesSeleccionados(animalesOK);

    }
}
