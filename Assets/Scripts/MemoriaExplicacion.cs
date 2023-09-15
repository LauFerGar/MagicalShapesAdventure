using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriaExplicacion : MonoBehaviour
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
                textoDialogo.text = "En este juego, deber�s memorizar la posicion de los 4 animales que aparecer�n en los 4 altares";
                break;

            case 2:
                textoDialogo.text = "Para ello, tienes 10 segundos desde que pulses el boton de COMENZAR.";
                break;
            case 3:
                textoDialogo.text = "Una vez trascurra ese tiempo. deber�s seleccionar primero el altar y luego el animal que recuerdes que deber�a ir ah�";
                break;
            case 4:
                textoDialogo.text = "AVISO: tienes un total de 4 oportunidades en todo el juego para fallar, por lo que antes de seleccionarlo tienes que estar seguro de que es ese.";
                break;
            case 5:
                textoDialogo.text = "Para comenzar a jugar, deber�s buscar la imagen que se muestra a la izquierda y ponerla delante de la c�mara de tu tablet o m�vil.";
                break;
            case 6:
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

        textoDialogo.text = "Bienvenido al juego de Memoria con animales";

    }
}
