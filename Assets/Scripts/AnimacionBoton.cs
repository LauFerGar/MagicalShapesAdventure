using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionBoton : MonoBehaviour
{
    private Vector3 posicionInicial;
    private float velocidad = 100.0f;
    private bool pulsado = false;
    private AudioSource[] sonidos;
    public GameObject[] animalesOK;
    private int indiceActual = 0;
    private int indiceSonidos = 0;
    private List<AudioClip> audiosSeleccionados = new List<AudioClip>();

    private void Start()
    {
        posicionInicial = transform.localPosition;

        sonidos = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource sonido in sonidos)
        {
            if (sonido.clip!=null)
            {
                string nombreClip = sonido.clip.name;
                //Debug.Log("Sonido del animal: " + nombreClip);
            }
            else
            {
                Debug.Log("El AudioSource no tiene un Clip con sonido de animal");
            }
            
        }

        /*foreach (GameObject animal in animalesOK)
        {
            Debug.Log("El animal " + animal.name + " ha llegado al script AnimacionBoton");
        }*/

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

        /*foreach (AudioClip clip in audiosSeleccionados)
        {
            Debug.Log("Audio seleccionado para juego: " + clip.name);
        }*/
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
        ReproducirSonido();
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
        Debug.Log("Se ha pulsado en el " + animalSeleccionado + ". El animal a comparar es: " + animalesOK[indiceActual]);

        if (audiosSeleccionados[indiceSonidos].name == animalSeleccionado.name)
        {
            Debug.Log("Se ha pulsado en el " + animalSeleccionado + ". El animal a comparar es: " + animalesOK[indiceActual] + "y su sonido es: " + audiosSeleccionados[indiceSonidos].name);
            indiceSonidos++;
        }
        else
        {
            Debug.Log("Ese no es el animal. Pulsa de nuevo");
        }
    }
}
