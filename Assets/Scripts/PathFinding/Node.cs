using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents a node(tile) in the game graph.
 */
[System.Serializable]
public class Node
{
    // A Vector2Int indicates the current node's position.
    public Vector2Int position;

    // A flag indicates whether a node can be walked to.
    public bool isWalkable;
    
    // A flag indicates whether a node is already visited.
    public bool isVisited;

    // A flag indicates whether a node is included in the current path.
    public bool isPath;

    // A Node object indicates current Node's connected node.
    public Node connectedNode;
    
    // Constructor.
    public Node(Vector2Int position, bool isWalkable)
    {
        this.position = position;
        this.isWalkable = isWalkable;
    }
}
