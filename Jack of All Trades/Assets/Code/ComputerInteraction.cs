using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ComputerInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.Space;
    public Slider taskProgressSlider;
    public Text scoreText;

    public Animator anim;

    private float taskDuration = 3f;
    private float taskProgress = 0f;
    private bool isInteracting = false;
    private int data = 0;
    private GameObject currentComputer;
    private HashSet<GameObject> completedComputers = new HashSet<GameObject>();

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (taskProgressSlider == null || scoreText == null)
        {
            Debug.LogError("FORGOT TO ASSIGN UI IN INSPECTOR!");
            return;
        }

        if (isInteracting && Input.GetKey(interactionKey))
        {
            taskProgress += Time.deltaTime;
            taskProgressSlider.value = taskProgress / taskDuration;
            // play hacking animation
            anim.SetBool("Hack", true);

            if (taskProgress >= taskDuration) // timingt he space press
            {
                CompleteTask();
            }
        }
        else if (Input.GetKeyUp(interactionKey))
        {
            taskProgress = 0f;
            taskProgressSlider.value = 0f;
            // stop hacking animation
            anim.SetBool("Hack", false);
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
            // stop hacking animation
            anim.SetBool("Hack", false);
        }
    }

//     void CompleteTask()
// {
//     if (currentComputer != null)
//     {
//         // add score
//         data++;
//         scoreText.text = "Data: " + data;

//         // Get the child objects with the sprite renderers
//         Transform incompleteSprite = currentComputer.transform.Find("IncompleteSprite");
//         Transform completedSprite = currentComputer.transform.Find("CompletedSprite");

//         // If both sprites are found, switch them
//         if (incompleteSprite != null) 
//             incompleteSprite.gameObject.SetActive(false); // Hide incomplete sprite
//         if (completedSprite != null) 
//             completedSprite.gameObject.SetActive(true); // Show completed sprite

//         // Mark the current computer as completed, so it cant be interacted with again
//         completedComputers.Add(currentComputer);

//         // Disable collider so player can't trigger it again
//         currentComputer.GetComponent<Collider2D>().enabled = false; 

//         // Reset interaction
//         taskProgress = 0f;
//         taskProgressSlider.value = 0f;
//         isInteracting = false;
//         currentComputer = null;
//     }
// }


void CompleteTask()
{
    if (currentComputer != null)
    {
        // Add to global data count in GameHandler
        GameHandler.gotData++;
        GameHandler.instance.updateStatsDisplay(); // Update UI from GameHandler

        // Get the child objects with the sprite renderers
        Transform incompleteSprite = currentComputer.transform.Find("IncompleteSprite");
        Transform completedSprite = currentComputer.transform.Find("CompletedSprite");

        // If both sprites are found, switch them
        if (incompleteSprite != null) 
            incompleteSprite.gameObject.SetActive(false); // Hide incomplete sprite
        if (completedSprite != null) 
            completedSprite.gameObject.SetActive(true); // Show completed sprite

        // Mark the current computer as completed, so it can't be interacted with again
        completedComputers.Add(currentComputer);

        // Disable collider so player can't trigger it again
        currentComputer.GetComponent<Collider2D>().enabled = false; 

        // Reset interaction
        taskProgress = 0f;
        taskProgressSlider.value = 0f;
        isInteracting = false;
        currentComputer = null;
    }
}


}
