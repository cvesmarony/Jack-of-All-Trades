using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinanceGuy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 2f;
    public Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Listen for player input to move the object: 
    void FixedUpdate()
    {
        // if you move diagonally (2 inputs) you will go faster
        // can change this to be the same speed somehow...
        movement.x = Input.GetAxisRaw ("Horizontal");
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        movement.y = Input.GetAxisRaw ("Vertical");
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

     // Makes objects with the tag "files" disappear on contact
     // can change files to be tasks later!
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "files"){
            Destroy(other.gameObject);
        }
    }
}
