using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoMemoria : MonoBehaviour
{
    public Vector3[] posicionesAltar;
    public GameObject animal;
    public Button contador;

    private int tiempoRestante = 11;
    private bool juegoIniciado = false;
    private Text contadorTexto;
    private Vector3 posicionInicialGallina;

    private void Awake()
    {
        InicializarPosicionesAltar();
    }

    void Start()
    {

        contador.onClick.AddListener(ComenzarJuego);
        posicionInicialGallina = animal.GetComponent<Transform>().localPosition;

        animal.transform.localPosition = posicionesAltar[0];

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
            animal.transform.localPosition = posicionInicialGallina;
        }
    }

    private void InicializarPosicionesAltar()
    {
        posicionesAltar = new Vector3[]
        {
            new Vector3(-3.73799992f,0.0909999982f,-3.38000011f),
            new Vector3(-1.27999997f,0.0909999982f,-3.38000011f),
            new Vector3(1.16999996f,0.0909999982f,-3.38000011f),
            new Vector3(3.66000009f,0.0909999982f,-3.38000011f),
        };
    }
}
