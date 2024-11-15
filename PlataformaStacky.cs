using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaStacky : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Jugador")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Jugador")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
