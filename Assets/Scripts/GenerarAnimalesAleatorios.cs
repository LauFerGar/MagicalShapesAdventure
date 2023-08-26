using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarAnimalesAleatorios : MonoBehaviour
{
    public Transform grass;
    private GameObject[] todosLosAnimales;
    
    // Start is called before the first frame update
    void Start()
    {
        todosLosAnimales = new GameObject[grass.childCount];
        for (int i = 0; i < grass.childCount; i++)
        {
            todosLosAnimales[i] = grass.GetChild(i).gameObject;
        }
        
        GameObject[] animalesAleatorios = GenerarAnimalesAleatoriosArray(todosLosAnimales, 1);

        foreach (GameObject animal in todosLosAnimales)
        {
            bool activar = ArrayContains(animal, animalesAleatorios);
            animal.SetActive(activar);
        }

        foreach (GameObject animal in animalesAleatorios)
        {
            Debug.Log("Animal generado: " + animal.name);
        }

    }

    private GameObject[] GenerarAnimalesAleatoriosArray(GameObject[] todosLosAnimales, int cantidad)
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

    private bool ArrayContains(GameObject target, GameObject[] array)
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

