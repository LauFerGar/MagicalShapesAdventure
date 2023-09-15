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
                textoDialogo.text = "En este juego, deberás pulsar el boton de PLAY que encontraras en la pradera con los animales y escuchar atentamente el sonido del animal";
                break;

            case 2:
                textoDialogo.text = "Una vez que identifiques de qué animal se trata, debes buscar dicho animal en la pradera y pulsar sobre él";
                break;
            case 3:
                textoDialogo.text = "Si es el animal correcto, aparecerá un mensaje en el cuadro indicando el nombre del animal que has acertado";
                break;
            case 4:
                textoDialogo.text = "Para comenzar a jugar, deberás buscar la imagen que se muestra a la izquierda y ponerla delante de la cámara de tu tablet o móvil.";
                break;
            case 5:
                textoDialogo.text = "Una vez que la tengas delante, pulsa en el botón jugar para comenzar la partida. ¡Mucha suerte!";
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

        textoDialogo.text = "Bienvenido al juego de identificación de Sonidos";

    }
}
