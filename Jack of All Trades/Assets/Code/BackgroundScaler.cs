using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Camera cam = Camera.main;

        // Get the target 16:9 aspect ratio
        float targetAspect = 16f / 9f;
        float screenAspect = (float)Screen.width / Screen.height;

        // Get world height based on camera
        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * screenAspect;

        // Get the size of the sprite
        Vector2 spriteSize = sr.sprite.bounds.size;

        // Scale the background proportionally to fit 16:9
        float scaleX = worldWidth / spriteSize.x;
        float scaleY = worldHeight / spriteSize.y;
        float scale = Mathf.Max(scaleX, scaleY); // Ensure no black bars

        transform.localScale = new Vector3(scale, scale, 1f);
    }
}

