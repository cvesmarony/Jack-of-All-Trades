using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fileCollect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the Player is tagged correctly
        {
            Destroy(gameObject); // remove the item when touched
        }
    }
}
