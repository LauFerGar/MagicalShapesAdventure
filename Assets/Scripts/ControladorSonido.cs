using UnityEngine;
using UnityEngine.UI;

public class ControladorSonido : MonoBehaviour
{
    private  AudioSource audioSource;

    public Canvas canvas;
    public Text textoBoton;
    public Button boton;
    public bool sonidoActivado = true;
    public bool reproduciendo;

    private void Start()
    {
        audioSource = canvas.GetComponent<AudioSource>();
        if (canvas.GetComponent<AudioSource>().enabled == true)
        {
            float rNormalizado = 255 / 255f;
            float gNormalizado = 255 / 255f;
            float bNormalizado = 255 / 255f;

            Color nuevoColor = new Color(rNormalizado, gNormalizado, bNormalizado);

            textoBoton.text = "ON";
            boton.GetComponent<Image>().color = nuevoColor;

            audioSource.enabled = true;
            reproduciendo = true;
        }
        else
        {
            float rNormalizado = 121 / 255f;
            float gNormalizado = 121 / 255f;
            float bNormalizado = 121 / 255f;

            Color nuevoColor = new Color(rNormalizado, gNormalizado, bNormalizado);

            textoBoton.text = "OFF";
            boton.GetComponent<Image>().color = nuevoColor;

            audioSource.enabled = false;
            reproduciendo = false;
        }
    }

    public void ActivarDesactivarSonido() 
    {
        if (canvas.GetComponent<AudioSource>().enabled == true) 
        {
            float rNormalizado = 121 / 255f;
            float gNormalizado = 121 / 255f;
            float bNormalizado = 121 / 255f;

            Color nuevoColor = new Color(rNormalizado, gNormalizado, bNormalizado);

            textoBoton.text = "OFF";
            boton.GetComponent<Image>().color = nuevoColor;

            audioSource.enabled = false;
            reproduciendo = false;
        }
        else
        {
            float rNormalizado = 255 / 255f;
            float gNormalizado = 255 / 255f;
            float bNormalizado = 255 / 255f;

            Color nuevoColor = new Color(rNormalizado, gNormalizado, bNormalizado);

            textoBoton.text = "ON";
            boton.GetComponent<Image>().color = nuevoColor;

            audioSource.enabled = true;
            reproduciendo = true;
        }

    }

    private void Update()
    {
        if (!canvas.GetComponent<AudioSource>().isPlaying && reproduciendo)
        {
            ActivarDesactivarSonido();
        }
        else if (canvas.GetComponent<AudioSource>().isPlaying && !reproduciendo)
        {
            ActivarDesactivarSonido();
        }
    }
}