using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vuforia;

public class ArrastrarFigura : MonoBehaviour
{
    private bool isDragging = false; //Indica si la figura está siendo arrastrada
    private Vector3 offset = Vector3.zero; //Desplazamiento entre el toque inicial y el centro del objeto
    private Camera arCamera;
    private Vector3 initialPosition;

    void Start()
    {
        arCamera = Camera.main;
        initialPosition = transform.localPosition; //Guardamos la posición inicial de la figura
        Debug.Log(initialPosition);
    }
    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetTouchPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetTouchPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        transform.localPosition = initialPosition; //La figura vuelve a la posición inicial cuando se suelta
        //Debug.Log(transform.position);
    }

    Vector3 GetTouchPosition()
    {
        Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
