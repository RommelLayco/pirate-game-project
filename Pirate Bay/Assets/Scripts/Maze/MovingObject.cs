using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingObject : MonoBehaviour {

    private bool fading = false;

    public float moveTime = 0.1f;           
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;               
    private float inverseMoveTime;
    protected bool moving = false;
    private int combatChance = 0;
    protected int gold = 0;

    protected List<Vector3> collectedGold = new List<Vector3>();

    // Use this for initialization
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

        //ensure collected gold is disabled
        if (GameManager.getInstance().inMaze)
        {
            collectedGold = GameManager.getInstance().collectedgold;
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Gold"))
            {
                if(collectedGold.Contains(g.transform.position))
                {
                    g.SetActive(false);
                }
            } //end disbaling collected gold

            gold = GameManager.getInstance().mazeGold;
        }
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
        moving = false;
    }


    //calculate destination vector to move towards it
    virtual protected void Move(Vector3 goal)
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
                CombatBattle();
                if (fading)
                    return;
                moving = true;
                StartCoroutine(SmoothMovement(end));                
            }
        }
        //move till we are blocked
        else
        {
            Vector3 newGoal = GetNewPosition(start, hit.transform.position);

            //Move to destination
            //only move if no othr co routine running
            if (!moving)
            {
                CombatBattle();
                moving = true;
                StartCoroutine(SmoothMovement(newGoal));
                moving = false;
            }
        }
     

    }

    //moves the avatar till we are blocked
    Vector3 GetNewPosition(Vector3 start, Vector3 hit)
    {
		Vector3 newPosition;
        
        
		Vector3 direction = hit - start;
		Vector3 directionNormalized = direction.normalized;

		newPosition = hit - directionNormalized;
        
		
        return newPosition;
    }

   

    // Update is called once per frame
    void Update () {
	
	}

    void CombatBattle()
    {
        if(Random.Range(1,101) < combatChance)
        {
            combatChance = 0;
            //remember player position
            GameManager.getInstance().playerPos = transform.position;

            //remeber the collected gold and the value
            GameManager.getInstance().collectedgold = collectedGold;
            GameManager.getInstance().mazeGold = gold;

            GameObject g = GameObject.Find("FadeObj");
            if (g != null)
            {
                g.GetComponent<FadeScript>().FadeToLevel("combat");
                fading = true;
            }
            else
                Application.LoadLevel("combat");
        }
        else
        {
            combatChance += 2;
        }
    }
}
