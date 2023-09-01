using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionAnimal : MonoBehaviour
{
    
    
    private void OnMouseDown()
    {
        AnimacionBoton comparacionAnimalesSonidos = FindAnyObjectByType<AnimacionBoton>();

        if (comparacionAnimalesSonidos!=null)
        {
            comparacionAnimalesSonidos.VerificarAnimalSonido(gameObject);
        }
    }

}