using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarPosiciones : MonoBehaviour
{
    //Creamos Arrays para almacenar las figuras creadas en el Tablero
    public GameObject[] figuras;
    //Creamos Array para almacenar las posiciones del tablero
    private Vector3[] posicionesTablero;
    //Creamos Array para poner las posiciones que se van ocupando cuando ponemos las figuras en el tablero
    private bool[] posicionesOcupadas;

    private void Start()
    {
        InicializarPosicionesTablero();
        InicializarPosicionesOcupadas();

        foreach (GameObject figura in figuras)
        {
            AsignarPosicionAleatoria(figura);
        }
    }

    private void InicializarPosicionesTablero()
    {
        // Inicializamos las 16 posiciones del tablero
        posicionesTablero = new Vector3[]
        {
            new Vector3(-1.86899996f,0.505999982f,1.421f), // Fila 1 :: Columna 1
            new Vector3(-0.672999978f,0.505999982f,1.421f), // Fila 1 :: Columna 2
            new Vector3(0.605000019f,0.505999982f,1.421f), // Fila 1 :: Columna 3
            new Vector3(1.89699996f,0.505999982f,1.421f), // Fila 1 :: Columna 4

            new Vector3(-1.86899996f,0.334299982f,0.565999985f), // Fila 2 :: Columna 1 
            new Vector3(-0.672999978f,0.334299982f,0.565999985f), // Fila 2 :: Columna 2
            new Vector3(0.605000019f,0.505999982f,0.565999985f), // Fila 2 :: Columna 3
            new Vector3(1.89699996f,0.505999982f,0.565999985f), // Fila 2 :: Columna 4

            new Vector3(-1.86899996f,0.505999982f,-0.280999988f), // Fila 3 :: Columna 1 
            new Vector3(-0.672999978f,0.505999982f,-0.280999988f), // Fila 3 :: Columna 2
            new Vector3(0.605000019f,0.334300011f,-0.280999988f), // Fila 3 :: Columna 3
            new Vector3(1.89699996f,0.334299982f,-0.280999988f), // Fila 3 :: Columna 4

            new Vector3(-1.86899996f,0.334299982f,-1.13599956f), // Fila 4 :: Columna 1
            new Vector3(-0.672999978f,0.334299982f,-1.13599956f), // Fila 4 :: Columna 2
            new Vector3(0.605000019f,0.334299982f,-1.13599944f), // Fila 4 :: Columna 3
            new Vector3(1.89699996f,0.334299982f,-1.13599956f), // Fila 4 :: Columna 3
        };
    }

    private void InicializarPosicionesOcupadas()
    {
        // Inicializamos el registro de posiciones ocupadas
        posicionesOcupadas = new bool[posicionesTablero.Length];
    }

    private void AsignarPosicionAleatoria(GameObject figura)
    {
        int index = Random.Range(0, posicionesTablero.Length);

        // Verificamos si la posición ya está ocupada
        if (!PosicionOcupada(index))
        {
            figura.transform.localPosition = posicionesTablero[index];
            MarcarPosicionOcupada(index);
        }
        else
        {
            // Intenta otra vez si la posición está ocupada
            AsignarPosicionAleatoria(figura);
        }
    }

    private bool PosicionOcupada(int index)
    {
        return posicionesOcupadas[index];
    }

    private void MarcarPosicionOcupada(int index)
    {
        posicionesOcupadas[index] = true;
    }
}
