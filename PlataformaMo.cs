using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMo : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;  // Puntos de movimiento
    private int currentWaypointIndex = 0;  // Índice actual de waypoint
    [SerializeField] private float speed = 2f;  // Velocidad de movimiento
    //[SerializeField] private float detectionRadius = 0.5f; // Radio de detección del jugador
    [SerializeField] private LayerMask playerLayer;  // Capa que representa al jugador

    private GameObject player;  // Referencia al jugador
    private bool isPlayerOnPlatform = false;  

    private void Update()
    {
        MovePlatform();

        if (isPlayerOnPlatform && player != null)
        {
            // Si el jugador está sobre la plataforma, se mueve con ella
            player.transform.position = new Vector2(transform.position.x, player.transform.position.y);
        }
    }

    private void MovePlatform()
    {
        // Movimiento de la plataforma entre waypoints
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}


