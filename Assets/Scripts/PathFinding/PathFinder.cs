using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class implements main functions that help each enemy object find its path.
 */
public class PathFinder : MonoBehaviour
{
    // Store the start position.
    [SerializeField] private Vector2Int startPos;
    // Store the destination position.
    [SerializeField] private Vector2Int destPos;

    // Getter functions for start and destination positions.
    public Vector2Int StartPos => startPos;
    public Vector2Int DestPos => destPos;
    
    // Store the start Node and destination Node.
    private Node startNode;
    private Node destNode;
    
    // Store one Node we want to search neighbors for.
    private Node currSearchNode;
    
    // A Vector2Int array indicates 4 directions.
    private readonly Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};

    // Store the GridManager ref.
    private GridManager gridManager;
    
    // Store the grid ref.
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    
    // Store the dictionary of all visited nodes.
    private Dictionary<Vector2Int, Node> visitedDict = new Dictionary<Vector2Int, Node>();
    
    // Store the queue of Nodes indicates the frontier.
    private Queue<Node> frontier = new Queue<Node>();

    private void Awake()
    {
        // Find the GridManager ref.
        gridManager = FindObjectOfType<GridManager>();
        
        // If grid manager exists, then set the grid.
        if (gridManager != null)
        {
            // Get the grid ref.
            grid = gridManager.Grid;
            
            // Set the start and destination Node objects.
            startNode = grid[startPos];
            destNode = grid[destPos];

        }
    }

    // Start is called before the first frame update
    void Start()
    {

        GetNewPath();
    }

    /**
     *  Scan current search node's all neighbor Nodes and help BFS process.
     */
    private void ExploreNeighbors()
    {
        // Create a List of Node.
        List<Node> neighbors = new List<Node>();
        
        // For each possible direction.
        foreach (Vector2Int direction in directions)
        {
            // Get the possible neighbor's position.
            Vector2Int neighborPos = currSearchNode.position + direction;
            
            // Check if the Node object exists in this position.
            if (gridManager.GetNode(neighborPos) != null)
            {
                // Add the Node to the neighbor list.
                neighbors.Add(grid[neighborPos]);
            }
        }
        
        // For each possible neighbor Node.
        foreach (Node neighborNode in neighbors)
        {

            // If this neighbor Node is walkable and is not visited before.
            if (!visitedDict.ContainsKey(neighborNode.position) && neighborNode.isWalkable)
            {
                // Add this neighbor Node to both frontier queue and visited dictionary.
                frontier.Enqueue(neighborNode);
                visitedDict.Add(neighborNode.position, neighborNode);
                
                // Attach neighbor Node to current search Node.
                neighborNode.connectedNode = currSearchNode;
            }
        }
        
        
    }
    
    /**
     * The main implementation of Breadth First Search.
     */
    private void BFS(Vector2Int pos)
    {
        //startNode.isWalkable = true;
        //destNode.isWalkable = true;
        
        // For each time of BFS, first clear last time's storage.
        frontier.Clear();
        visitedDict.Clear();
        
        // Set the running flag.
        bool isRunning = true;
        
        // Add start Node to both frontier queue and visited dictionary.
        frontier.Enqueue(grid[pos]);
        visitedDict.Add(pos, grid[pos]);
        
        // If frontier still has Node(s) and the path is not found yet.
        while (frontier.Count > 0 && isRunning)
        {

            // Get this turn's search Node.
            currSearchNode = frontier.Dequeue();
            // Change its isVisited flag to change the color for debugging.
            currSearchNode.isVisited = true;
            
            // If the current Node is the destination Node then just break the loop.
            if (currSearchNode.position == destPos)
            {
                isRunning = false;
            }
            
            // Explore current search Node's neighbors.
            ExploreNeighbors();
        }
    }

    /**
     * Build and return the final path(from start to destination).
     */
    private List<Node> BuildPath()
    {
        // Create a List of Nodes indicates the final path.
        List<Node> path = new List<Node>();
        
        // Set the current Node as the dest Node.
        Node currNode = destNode;

        // Add current Node to the final path.
        path.Add(currNode);
        // Set current Node's path flag.
        currNode.isPath = true;
    
        // If each current Node's connected Node exists.
        while (currNode.connectedNode != null)
        {
            // Reset current Node as its connected Node.
            currNode = currNode.connectedNode;
            // Add current Node to the final path.
            path.Add(currNode);
            // Set current Node's path flag.
            currNode.isPath = true;
        }
        
        // Reverse the final path(now it is from start to destination).
        path.Reverse();
        
        return path;
    }

    /**
     * Try to get a new path.
     */
    public List<Node> GetNewPath()
    {
        return GetNewPath(startPos);
    }

    /**
     * Overloaded version of GetNewPath which uses an input Vector2Int as the start position.
     */
    public List<Node> GetNewPath(Vector2Int pos)
    {
        // Reset all Nodes in the grid manager.
        gridManager.ResetNodes();
        
        // Start BFS.
        BFS(pos);
        
        // After BFS, build the final path.
        return BuildPath();
    }

    /**
     * Check if a possible Node will block the current path or not.
     */
    public bool WillBlockPath(Vector2Int pos)
    {
        // First check if grid contains the Node on input position.
        if (grid.ContainsKey(pos))
        {
            // Fetch this Node's current walkable flag.
            bool previousState = grid[pos].isWalkable;
            
            // Set this Node's current walkable flag to false.
            grid[pos].isWalkable = false;
            // Try to find the new path.
            List<Node> newPath = GetNewPath();
            
            // Revert this Node's walkable flag.
            grid[pos].isWalkable = previousState;
            
            // Check if the new path exists.
            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    /**
     * Calls the RecalculatePath method on each Enemy object(ObjectPool object's child).
     */
    public void NotifyReceiver()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
    
}
