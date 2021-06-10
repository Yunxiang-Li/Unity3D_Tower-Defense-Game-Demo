using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents all enemy objects' movement behaviors.
 */
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{   
    // Store a List of Node objects which indicates a enemy object's movement path.
    private List<Node> path = new List<Node>();
    // Store the enemy object's move speed.
    [SerializeField] [Range(0f, 5f)] private float moveSpeed = 1f;
    // Store the enemy ref;
    private Enemy enemy;
    
    // Store the grid manager ref.
    private GridManager gridManager;
    // Store the path finder ref.
    private PathFinder pathFinder;
    
    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        // Set each enemy object's start position.
        SetStartPos();
        
        // Find the path from start position.
        RecalculatePath(true);
    }

    private void Awake()
    {
        // Initialize all needed refs.
        enemy = FindObjectOfType<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the enemy ref.
        enemy = FindObjectOfType<Enemy>();
    }

    /**
     * A coroutine function follow the enemy object's given movement path.
     */
    private IEnumerator FollowPath()
    {
        // For each Node object in the given List.
        for (int i = 1; i < path.Count; ++i)
        {
            // Set the enemy object's current turn's start position.
            Vector3 startPos = transform.position;
            // Set the enemy object's current turn's destination.
            Vector3 endPos = gridManager.GetWorldFromPos(path[i].position);
                
            // Initialize the lerp percent to 0.
            float travelPercent = 0f;
            
            // Set the enemy object always facing the next Node's position.
            transform.LookAt(endPos);
            
            // If current turn is not end.
            while (travelPercent < 1f)
            {
                // Update the lerp percent according to per frame time and enemy object's move speed.
                travelPercent += Time.deltaTime * moveSpeed;
                // Use lerp function to smooth the enemy movement from current Node's position to next Node's position.
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                // Delay for each frame.
                yield return new WaitForEndOfFrame();
            }
        }
        
        // Handle things more than following the path.
        FinishPath();
    }
    
    /**
     * De-active each enemy which finished the path and stole certain balance.
     */
    private void FinishPath()
    {
        // If enemy move through the whole path then de-active the enemy.
        gameObject.SetActive(false);
        
        // Steal player a certain balance from the bank.
        enemy.StealBalance();
    }

    /**
     * Recalculate current enemy's path.
     */
    private void RecalculatePath(bool shouldReset)
    {
        // Initialize the next position for pathfinding.
        Vector2Int pos = new Vector2Int();
        
        // If we need to reset all previous steps.
        if (shouldReset)
        {
            // Reset to start position.
            pos = pathFinder.StartPos;
        }
        else
        {
            // Continue from current position.
            pos = gridManager.GetPosFromWorld(transform.position);
        }
        
        StopAllCoroutines();
        
        // Clear previous path.
        path.Clear();
        
        // Find a new path.
        path = pathFinder.GetNewPath(pos);
        
        // Call the FollowPath coroutine function.
        StartCoroutine(FollowPath());
    }

    /**
     * Set each enemy's start position.
     */
    private void SetStartPos()
    {
        if (path != null)
            transform.position = gridManager.GetWorldFromPos(pathFinder.StartPos);
    }
}
