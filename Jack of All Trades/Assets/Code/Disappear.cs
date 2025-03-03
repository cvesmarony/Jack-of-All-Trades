using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{

    public Dialogue DialogueBox;
    public GameObject HackerPortrait; 
    public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (DialogueBox != null && DialogueBox.dialogueCompleted)
        {
            HackerPortrait.SetActive(false); 
            Text.SetActive(false);
        }
    }
}

