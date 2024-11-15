using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectoGame : MonoBehaviour
{
    [SerializeField] private AudioSource finishSoundEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto con el que el jugador colidió es la copa
        if (other.CompareTag("Copa"))
        {
            finishSoundEffect.Play();
            // Llamamos a la corutina para cambiar la escena después de 2 segundos
            StartCoroutine(ChangeSceneAfterDelay(2f));
        }
    }

    // Corutina que cambia la escena después de un retraso
    private IEnumerator ChangeSceneAfterDelay(float delay)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(delay);
        // Cambiar a la escena "GameOver"
        SceneManager.LoadScene("GameOver");
    }
}
