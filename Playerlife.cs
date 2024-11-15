using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Playerlife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private TextMeshProUGUI vidasText;
    [SerializeField] private Transform[] checkpoints; // Array de puntos de checkpoint
    private static int deathCount = 0;
    public int maxDeaths = 5;
    private int lastCheckpointIndex = -1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        UpdateVidasText();

        if (lastCheckpointIndex >= 0 && lastCheckpointIndex < checkpoints.Length)
        {
            transform.position = checkpoints[lastCheckpointIndex].position;
            rb.bodyType = RigidbodyType2D.Dynamic; // Establecer Rigidbody como dinámico al revivir sin ninguna animación
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            SetCheckpoint(other.transform);
        }
    }

    private void Die()
    {
        if (rb.bodyType == RigidbodyType2D.Static) return; // Evitar múltiples llamadas a Die si ya está muerto
        
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        // anim.SetTrigger("Muerte"); // Comentado para evitar la animación de muerte al revivir
        deathCount++;
        Debug.Log("Muertes: " + deathCount);
        UpdateVidasText();

        if (deathCount >= maxDeaths)
        {
            GameOver();
        }
        else
        {
            RestartFromCheckpoint(); // Reiniciar desde el checkpoint sin retraso
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        deathCount = 0;
        lastCheckpointIndex = -1;
        SceneManager.LoadScene("GameOver"); // Asegúrate de tener una escena llamada "GameOver"
    }

    private void RestartFromCheckpoint()
    {
        if (lastCheckpointIndex >= 0 && lastCheckpointIndex < checkpoints.Length)
        {
            transform.position = checkpoints[lastCheckpointIndex].position;
            rb.bodyType = RigidbodyType2D.Dynamic; // Establecer Rigidbody como dinámico al revivir sin ninguna animación
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SetCheckpoint(Transform checkpointTransform)
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i] == checkpointTransform)
            {
                lastCheckpointIndex = i;
                Debug.Log("Checkpoint alcanzado: " + lastCheckpointIndex);
                break;
            }
        }
    }

    private void UpdateVidasText()
    {
        int vidasRestantes = maxDeaths - deathCount;
        if (vidasText != null)
        {
            vidasText.text = "Vidas: " + vidasRestantes;
        }
    }

    // Método llamado por Animation Event
    public void RestartLevel()
    {
        RestartFromCheckpoint();
    }
}
