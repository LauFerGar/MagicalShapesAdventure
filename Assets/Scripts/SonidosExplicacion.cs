using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonidosExplicacion : MonoBehaviour
{
    public int contador = 0;
    public Text textoDialogo;
    public Button comenzar, menu;

    public void ExplicarJuegoBuscar()
    {
        contador++;

        menu.onClick.AddListener(FinExplicacion);

        switch (contador)
        {
            case 1:
                textoDialogo.text = "En este juego, deber�s pulsar el boton de PLAY que encontraras en la pradera con los animales y escuchar atentamente el sonido del animal";
                break;

            case 2:
                textoDialogo.text = "Una vez que identifiques de qu� animal se trata, debes buscar dicho animal en la pradera y pulsar sobre �l";
                break;
            case 3:
                textoDialogo.text = "Si es el animal correcto, aparecer� un mensaje en el cuadro indicando el nombre del animal que has acertado";
                break;
            case 4:
                textoDialogo.text = "Para comenzar a jugar, deber�s buscar la imagen que se muestra a la izquierda y ponerla delante de la c�mara de tu tablet o m�vil.";
                break;
            case 5:
                textoDialogo.text = "Una vez que la tengas delante, pulsa en el bot�n jugar para comenzar la partida. �Mucha suerte!";
                comenzar.gameObject.SetActive(true);
                comenzar.onClick.AddListener(FinExplicacion);
                break;
        }
    }

    private void FinExplicacion()
    {
        comenzar.onClick.RemoveListener(FinExplicacion);
        menu.onClick.RemoveListener(FinExplicacion);

        contador = 0;

        textoDialogo.text = "Bienvenido al juego de identificaci�n de Sonidos";

    }
}
