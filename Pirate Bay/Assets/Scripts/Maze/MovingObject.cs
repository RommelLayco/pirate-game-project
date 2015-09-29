﻿using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    public float moveTime = 0.1f;           
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;               
    private float inverseMoveTime;
    private bool moving = false;


    // Use this for initialization
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end)
    {
        
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }


    //calculate destination vector to move towards it
    protected void Move(Vector3 goal)
    {
        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = goal;

        //Dectect an object that block like a wall
        RaycastHit2D hit;
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        //Only Move if nothing is blocking else do nothing
        if (hit.transform == null)
        {
            //Move to destination
            //only move if no othr co routine running
            if (!moving)
            {
                
                moving = true;
                Debug.Log(moving);
                StartCoroutine(SmoothMovement(end));
                moving = false;
            }
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}