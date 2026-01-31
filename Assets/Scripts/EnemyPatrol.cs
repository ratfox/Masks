using UnityEngine;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    public List<Transform> waypoints; // Drag your 4 points here
    public float waitTime = 1f;       // How long to stay at a point
    
    private int currentTargetIndex;
    private bool isWaiting = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Start by heading to a random point
        currentTargetIndex = Random.Range(0, waypoints.Count);
    }

    void Update()
    {
        if (isWaiting) return;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        Transform target = waypoints[currentTargetIndex];
        
        // Move towards the target
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // Update Animation (Reuse your player's Speed parameter!)
        if (anim != null) anim.SetFloat("Speed", 1f);

        // Check if we reached the point
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtPoint());
        }

        // Flip sprite based on direction
        if (target.position.x > transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    System.Collections.IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        if (anim != null) anim.SetFloat("Speed", 0f); // Idle animation

        yield return new WaitForSeconds(waitTime);

        // Pick a NEW random point that isn't the one we are currently at
        int nextPoint = currentTargetIndex;
        while (nextPoint == currentTargetIndex)
        {
            nextPoint = Random.Range(0, waypoints.Count);
        }
        
        currentTargetIndex = nextPoint;
        isWaiting = false;
    }
}