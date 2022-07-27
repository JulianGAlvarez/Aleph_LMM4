using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SunAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public Transform enemyGFX;
    private float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        scaleX = enemyGFX.transform.localScale.x;
        
    }

    void UpdatePath()
    {
        //Update the Path creation

        if(seeker.IsDone())
           seeker.StartPath(rb.position, -target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If there is NO Path
        if(path == null)
            return;
        //If we reached the end of the Path
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        //Moves the Sun towards the Player

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        //Changes the Sun´s Orientation
        if(force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-scaleX, scaleX, scaleX);

        } else if(force.x <= -0.01f) {

            enemyGFX.localScale = new Vector3(scaleX, scaleX, scaleX);

        }
    }
}
