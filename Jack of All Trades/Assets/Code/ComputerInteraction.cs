using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ComputerInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.Space;
    public Slider taskProgressSlider;
    public Text scoreText;

    private float taskDuration = 3f;
    private float taskProgress = 0f;
    private bool isInteracting = false;
    private int score = 0;
    private GameObject currentComputer;
    private HashSet<GameObject> completedComputers = new HashSet<GameObject>();

    void Update()
    {
        if (taskProgressSlider == null || scoreText == null)
        {
            Debug.LogError("UI Elements not assigned in the Inspector!");
            return;
        }

        if (isInteracting && Input.GetKey(interactionKey))
        {
            taskProgress += Time.deltaTime;
            taskProgressSlider.value = taskProgress / taskDuration;

            if (taskProgress >= taskDuration)
            {
                CompleteTask();
            }
        }
        else if (Input.GetKeyUp(interactionKey))
        {
            taskProgress = 0f;
            taskProgressSlider.value = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Computer") && !completedComputers.Contains(other.gameObject))
        {
            isInteracting = true;
            currentComputer = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Computer"))
        {
            isInteracting = false;
            currentComputer = null;
            taskProgress = 0f;
            taskProgressSlider.value = 0f;
        }
    }

    void CompleteTask()
{
    if (currentComputer != null)
    {
        // Increment score
        score++;
        scoreText.text = "Score: " + score;

        // Get the child objects with the sprite renderers
        Transform incompleteSprite = currentComputer.transform.Find("IncompleteSprite");
        Transform completedSprite = currentComputer.transform.Find("CompletedSprite");

        // If both sprites are found, switch them
        if (incompleteSprite != null) 
            incompleteSprite.gameObject.SetActive(false); // Hide incomplete sprite
        if (completedSprite != null) 
            completedSprite.gameObject.SetActive(true); // Show completed sprite

        // Mark the current computer as completed, so it won't be interacted with again
        completedComputers.Add(currentComputer);

        // Disable further interaction with this computer
        currentComputer.GetComponent<Collider2D>().enabled = false; // Disable collider so player can't trigger it again

        // Reset interaction
        taskProgress = 0f;
        taskProgressSlider.value = 0f;
        isInteracting = false;
        currentComputer = null;
    }
}

}
