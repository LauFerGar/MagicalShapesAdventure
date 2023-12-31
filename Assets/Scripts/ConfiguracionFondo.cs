using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguracionFondo : MonoBehaviour
{
    // Start is called before the first frame update
    public Image FondoApp, FondoJuegos;

    void Start()
    {
        // Obtener la resolución de pantalla actual del dispositivo
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Ajusta la escala del CanvasScaler
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(screenWidth, screenHeight);

        FondoApp.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
        FondoJuegos.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
    }

    
}
