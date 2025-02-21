using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaries : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private float playerWidth, playerHeight;

    void Start()
    {
        Camera cam = Camera.main;

        // Get half screen size in world units
        float halfWidth = cam.orthographicSize * cam.aspect;
        float halfHeight = cam.orthographicSize;

        // Get player's size (assuming a SpriteRenderer or Collider2D)
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            playerWidth = sr.bounds.extents.x; // Half width
            playerHeight = sr.bounds.extents.y; // Half height
        }
        else
        {
            // Default size if no SpriteRenderer found
            playerWidth = 0.5f;
            playerHeight = 0.5f;
        }

        // Set boundaries, subtracting player's size to prevent clipping
        minX = -halfWidth + playerWidth;
        maxX = halfWidth - playerWidth;
        minY = -halfHeight + playerHeight;
        maxY = halfHeight - playerHeight;
    }

    void Update()
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}