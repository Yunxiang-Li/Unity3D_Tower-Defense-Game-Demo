using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class helps us manager the game grid which consists of multiple Node objects.
 */
public class GridManager : MonoBehaviour
{
    // Store the size(x size and y size) of the grid.
    [SerializeField] private Vector2Int gridSize;
    
    [Tooltip("unityGridSize is just Unity's snap setting")]
    [SerializeField] private int unityGridSize = 10;
    
    // Get function of unityGridSize property.
    public int UnityGridSize => unityGridSize;
    
    // Store the Vector2Int Node key value pairs as a Dictionary indicates the game grid.
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    // Getter method for the whole grid.
    public Dictionary<Vector2Int, Node> Grid => grid;
    
    private void Awake()
    {
        CreateGrid();
    }
    
    /**
     * Get a certain Node object if it exists.
     */
    public Node GetNode(Vector2Int pos)
    {
        // Check for Node existence.
        if (grid.ContainsKey(pos)) 
            return grid[pos];
        return null;
    }
    
    /**
     * Create the game grid according to grid size.
     */
    private void CreateGrid()
    {
        // For each column.
        for (int x = 0; x < gridSize.x; ++x)
        {
            // For each row.
            for (int y = 0; y < gridSize.y; ++y)
            {
                // Create each Node's position.
                Vector2Int eachPos = new Vector2Int(x, y);
                // Add each Node's position and Node itself into the Dictionary.
                grid.Add(eachPos, new Node(eachPos, true));
            }
        }
    }

    /**
     * Block one Node within the gid(Set that Node's walkable flag to be false).
     */
    public void BlockNode(Vector2Int pos)
    {
        // First check if one Node exists in input position.
        if (grid.ContainsKey(pos))
        {
            // Set that Node's walkable flag to be false
            grid[pos].isWalkable = false;
        }
    }

    /**
     * Transform a unity game world position to a tile position.
     */
    public Vector2Int GetPosFromWorld(Vector3 position)
    {
        Vector2Int pos = new Vector2Int
        {
            // Set each tile's game position.
            // We think as (x, y) but we are actually use (x, z) in game.
            x = Mathf.RoundToInt(position.x / unityGridSize), y = Mathf.RoundToInt(position.z / unityGridSize)
        };
        
        return pos;
    }
    
    /**
     * Transform a tile position to a unity game world position.
     */
    public Vector3 GetWorldFromPos(Vector2Int pos)
    {
        Vector3 worldPos = new Vector3
        {
            // Set each tile's world position.
            // We think as (x, y) but we are actually use (x, z) in game.
            x = pos.x * unityGridSize, z = pos.y * unityGridSize
        };

        return worldPos;
    }

    /**
     * Reset each Node's condition within the grid.
     */
    public void ResetNodes()
    {
        // Reset each Node's visited flag, path flag and connected Node.
        foreach (KeyValuePair<Vector2Int, Node> pair in grid)
        {
            pair.Value.connectedNode = null;
            pair.Value.isVisited = false;
            pair.Value.isPath = false;
        }
    }
}
