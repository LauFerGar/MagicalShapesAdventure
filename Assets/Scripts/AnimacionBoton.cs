using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionBoton : MonoBehaviour
{
    private Vector3 posicionInicial;
    private float velocidad = 100.0f;
    private bool pulsado = false;

    private void Start()
    {
        posicionInicial = transform.localPosition;
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
        AudioSource sonidoAnimal = GetComponent<AudioSource>();
        sonidoAnimal.Play();
    }
}
