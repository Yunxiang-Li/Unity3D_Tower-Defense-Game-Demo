using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents each Tile object's behaviors.
 */
public class Tile : MonoBehaviour
{
    // Store a bool flag indicates whether we can place a tower on a tile or not.
    [SerializeField] private bool isPlaceable;
    // Store a Tower prefab.
    [SerializeField] private Tower towerPrefab;
    
    // A Vector2Int indicates the Tile's game position.
    private  Vector2Int position = new Vector2Int();
    
    // Get the value of isPlaceable.
    public bool IsPlaceable => isPlaceable;

    // Store the grid manager ref.
    private GridManager gridManager;
    
    // Store the pathFinder ref.
    private PathFinder pathFinder;

    private void Awake()
    {
        // Set the grid manager and path finder ref.
        gridManager = FindObjectOfType<GridManager>();

        pathFinder = FindObjectOfType<PathFinder>();
    }
    
    // Start is called before the first frame update
    void Start()
    {

        if (gridManager != null)
        {
            // Get each Tile's game position.
            position = gridManager.GetPosFromWorld(transform.position);
            
            // If this tile is not placeable.
            if (!isPlaceable)
            {
                // Let grid manager block the relative Node.
                gridManager.BlockNode(position);
            }
        }

    }

    // Called when user press the mouse button on a collider(a Tile object here). 
    private void OnMouseDown()
    {
        // If this position's Node is walkable and will not block the path. 
        if (gridManager.GetNode(position).isWalkable && !pathFinder.WillBlockPath(position))
        {
            // Try to use tower prefab to create one tower game object.
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            
            // If instantiate successfully.
            if (isSuccessful)
            {
                // Block this Node.
                gridManager.BlockNode(position);
                // Find a new path for each enemy object.
                pathFinder.NotifyReceiver();
            }
        }
    }
}
