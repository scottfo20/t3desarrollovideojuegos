using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Reintentar : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReiniciarJuego()
    {
        // Obtiene el nombre de la escena actual
        string escenaActual = SceneManager.GetActiveScene().name;

        // Vuelve a cargar la escena actual
        SceneManager.LoadScene("SampleScene");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
