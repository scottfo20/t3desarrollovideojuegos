using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform leftPoint;  // Punto más a la izquierda
    [SerializeField] private Transform rightPoint; // Punto más a la derecha
    private bool movingRight = true; // Determina si el enemigo se mueve hacia la derecha

    public float speed = 3f; // Velocidad de movimiento

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    void Start()
    {
        // Obtener el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento de izquierda a derecha
        MoveHorizontal();

        // Verificar si el enemigo ha llegado a uno de los puntos y cambiar la dirección
        if (transform.position.x <= leftPoint.position.x)
        {
            movingRight = true; // Cambia de dirección hacia la derecha
            FlipSprite(); // Cambia la dirección del sprite
        }
        else if (transform.position.x >= rightPoint.position.x)
        {
            movingRight = false; // Cambia de dirección hacia la izquierda
            FlipSprite(); // Cambia la dirección del sprite
        }
    }

    // Mueve al enemigo de izquierda a derecha
    private void MoveHorizontal()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPoint.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPoint.position, speed * Time.deltaTime);
        }
    }

    // Cambia la dirección del sprite (volteándolo)
    private void FlipSprite()
    {
        // Invertir la escala en el eje X para cambiar la dirección del sprite
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // Detectar la colisión con el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Eliminar el enemigo cuando colisiona con el jugador
            Destroy(gameObject);
        }
    }
}
