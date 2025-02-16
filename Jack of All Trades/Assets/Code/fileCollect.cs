using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fileCollect : MonoBehaviour
{
    private bool isCollected = false; // To prevent multiple triggers on the same object

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollected && other.CompareTag("Player")) 
        {
            isCollected = true; 

            FinanceGuy playerScript = other.GetComponent<FinanceGuy>();
            if (playerScript != null)
            {
                playerScript.AddScore(1); // Increase score by 1
            }

            Destroy(gameObject); 
        }
    }
}
