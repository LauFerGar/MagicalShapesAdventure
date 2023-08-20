using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionFigura : MonoBehaviour
{
    private bool isSelected = false;
    private Material originalMaterial;
    public Material selectedMaterial;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            GetComponent<Renderer>().material = selectedMaterial;
            DialogoJuegoBuscar dialogoJuegoB = FindObjectOfType<DialogoJuegoBuscar>();

            if (dialogoJuegoB != null)
            {
                dialogoJuegoB.VerificarSeleccion(gameObject);
                //Debug.Log(gameObject);
            }
        }
        else
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
