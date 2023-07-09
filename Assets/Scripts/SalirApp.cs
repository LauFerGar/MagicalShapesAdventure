using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalirApp : MonoBehaviour
{
    private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos la referencia al componente Button del botón de salida
        exitButton = GetComponent<Button>();

        // Asignamos el método CloseApplication() al evento OnClick del botón
        exitButton.onClick.AddListener(CloseApplication);
    }

    private void CloseApplication()
    {
        // Cierra la aplicación
        Application.Quit();
    }
}
