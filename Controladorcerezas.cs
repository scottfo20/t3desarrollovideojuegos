using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controladorcerezas : MonoBehaviour
{
    private int cerezas = 0;
    private TMP_Text puntoText;
    [SerializeField] private AudioSource collectionSoundEffect;
     void Start() {
        
        puntoText = GameObject.Find("puntos").GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cereza"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cerezas++;
            puntoText.text = "Cerezas: "+ cerezas;
        }
    }
}
