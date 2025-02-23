using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boundaries : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private float playerWidth, playerHeight;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;
        float halfHeight = cam.orthographicSize;


        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            playerWidth = sr.bounds.extents.x;
            playerHeight = sr.bounds.extents.y;
        }
        else
        {
            playerWidth = 0.5f;
            playerHeight = 0.5f;
        }


        minX = -halfWidth + playerWidth;
        maxX = halfWidth - playerWidth;
        minY = -halfHeight + playerHeight;
        maxY = halfHeight - playerHeight;
    }


    void FixedUpdate()
    {
        // Get the player's current position
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(rb.position.x, minX, maxX),
            Mathf.Clamp(rb.position.y, minY, maxY)
        );


        // If the player is out of bounds, reset their position
        if (rb.position.x < minX || rb.position.x > maxX)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement
        }
        if (rb.position.y < minY || rb.position.y > maxY)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Stop vertical movement
        }


        rb.position = clampedPosition; // Constrain position within boundaries
    }
}