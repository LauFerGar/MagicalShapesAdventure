using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarAnimalesAleatorios : MonoBehaviour
{
    public Transform grass;
    public GameObject play;
    public int numAdivinar = 6;

    public GameObject[] todosLosAnimales;
    private AnimacionBoton animacionBoton;
    public Vector3[] posicionesAnimales;
    private int indicePosicion = 0;

    private void Awake()
    {
        todosLosAnimales = new GameObject[grass.childCount];
        for (int i = 0; i < grass.childCount; i++)
        {
            todosLosAnimales[i] = grass.GetChild(i).gameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InicializarPosicionesPlano();
        animacionBoton = play.GetComponent<AnimacionBoton>();
        
        
        GameObject[] animalesAleatorios = GenerarAnimalesAleatoriosArray(todosLosAnimales, numAdivinar);

        animacionBoton.animalesOK = animalesAleatorios;

        foreach (GameObject animal in todosLosAnimales)
        {
            bool activar = ArrayContains(animal, animalesAleatorios);
            animal.SetActive(activar);

            if (activar)
            {
                animal.transform.localPosition = posicionesAnimales[indicePosicion];
                indicePosicion++;
            }
        }

    }

    public GameObject[] GenerarAnimalesAleatoriosArray(GameObject[] todosLosAnimales, int cantidad)
    {
        if (cantidad<=0 || cantidad > todosLosAnimales.Length)
        {
            Debug.LogWarning("Cantidad invalida de animales a generar.");
            return new GameObject[0];
        }

        List<GameObject> animalesAletorios = new List<GameObject>(todosLosAnimales);
        animalesAletorios.Shuffle();

        return animalesAletorios.GetRange(0, cantidad).ToArray();
    }

    public bool ArrayContains(GameObject target, GameObject[] array)
    {
        foreach (GameObject item in array)
        {
            if (item == target)
            {
                return true;
            }
        }
        return false;
    }

    public void InicializarPosicionesPlano()
    {
        posicionesAnimales = new Vector3[]
        {
            new Vector3(-2.42000008f,0.0320000015f,2.77999997f), //Vaca
            new Vector3(-2.67000008f,0,0), //Caballo
            new Vector3(3.36999989f,0,3.19000006f), //Elefante
            new Vector3(-2.59899998f,0.0309999995f,-2.98000002f), //Rana
            new Vector3(2.63000011f,0.0839999989f,-1.96700001f), //Pato
            new Vector3(1.76999998f,0.0149999997f,0.239999995f) //Perro
        };
    }

    public void JuegoFinalizado()
    {
        indicePosicion = 0;

        GameObject[] animalesAleatorios = GenerarAnimalesAleatoriosArray(todosLosAnimales, numAdivinar);

        animacionBoton.animalesOK = animalesAleatorios;

        foreach (GameObject animal in todosLosAnimales)
        {
            bool activar = ArrayContains(animal, animalesAleatorios);
            animal.SetActive(activar);

            if (activar)
            {
                animal.transform.localPosition = posicionesAnimales[indicePosicion];
                indicePosicion++;
            }
        }

    }

}
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

