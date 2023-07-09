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
        // Obtenemos la referencia al componente Button del bot�n de salida
        exitButton = GetComponent<Button>();

        // Asignamos el m�todo CloseApplication() al evento OnClick del bot�n
        exitButton.onClick.AddListener(CloseApplication);
    }

    private void CloseApplication()
    {
        // Cierra la aplicaci�n
        Application.Quit();
    }
}
