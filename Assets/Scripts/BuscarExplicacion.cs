using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuscarExplicacion : MonoBehaviour
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
                textoDialogo.text = "En este juego, deberás buscar la figura con el color asignado que te indica el panel superior del juego.";
                break;

            case 2:
                textoDialogo.text = "Tienes que estar atent@ ya que cuando aciertes la figura, ¡todas se moveran de sitio de nuevo!";
                break;
            case 3:
                textoDialogo.text = "Para comenzar a jugar, deberás buscar la imagen que se muestra a la izquierda y ponerla delante de la cámara de tu tablet o móvil.";
                break;
            case 4:
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

        textoDialogo.text = "Bienvenido al juego de Buscar la figura correcta.";

    }


}

