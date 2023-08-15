using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionFigura : MonoBehaviour
{
    private bool isSelected = false;
    private Material cubeMaterial;
    private Material originalMaterial;
    public Material selectedMaterial;  // Asigna el nuevo material en el Inspector

    private void Start()
    {
        cubeMaterial = GetComponent<Renderer>().material;
        originalMaterial = cubeMaterial;
    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            cubeMaterial = selectedMaterial;
        }
        else
        {
            cubeMaterial = originalMaterial;
        }

        GetComponent<Renderer>().material = cubeMaterial;
    }
}
