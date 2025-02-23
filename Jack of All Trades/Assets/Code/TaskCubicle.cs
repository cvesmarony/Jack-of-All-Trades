using UnityEngine;

public class TaskCubicle : MonoBehaviour
{
    public bool taskCompleted = false; // Track if the task is completed
    public Sprite completedSprite; 

    // Called when the player interacts with the cubicle and completes the task
    public void MarkTaskComplete()
    {
        if (!taskCompleted)
        {
            taskCompleted = true;
            Debug.Log("Task Completed at cubicle!");

            if (completedSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = completedSprite;
            }
            
        }
    }
}
