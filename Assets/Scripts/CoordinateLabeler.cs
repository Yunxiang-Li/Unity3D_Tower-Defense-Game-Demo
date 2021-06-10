using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * This class uses TextMeshPro component to display each Tile's custom position before game starts.
 */
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{   
    // Store colors for default/blocked/visited/path tiles.
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.grey;
    [SerializeField] private Color visitedColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0.15f, 0f);
    
    // Store the grid manager ref.
    private GridManager gridManager;
    // Store the TextMeshPro component;
    private TextMeshPro label;

    // Use Vector2Int to store each Tile's custom position.
    private Vector2Int position;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        label = gameObject.GetComponent<TextMeshPro>();
        label.enabled = false;
        // Run once to display each Tile's custom position after the game starts.
        DisplayCurrentCoordinates();
        
        // Set the grid manager ref.
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Application.isPlaying)
        {
            // Ready to set and display each Tile object's custom coordinates before the game starts.
            DisplayCurrentCoordinates();
            SetEachTileName();
            
            label.enabled = true;
        }

        // Set each tile's color.
        SetLabelColor();
        
        // Allow user to display or un-display all tiles' labels.
        ToggleLabels();
    }

    /**
     * Display each Tile's custom position.
     */
    private void DisplayCurrentCoordinates()
    {
        if (gridManager == null)
            return;
        
        // Set each TextMeshPro component's parent object(Tile object)'s position.
        position.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        // We think as (x, y) but we are actually use (x, z) in game.
        position.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        
        // Set the TextMeshPro component's text contents.
        label.text = position.x + ", " + position.y;
    }

    /**
     * Set each Tile object's name according to its custom coordinates.
     */
    private void SetEachTileName()
    {
        transform.parent.name = position.ToString();
    }

    /**
     * Set each tile's color .
     */
    private void SetLabelColor()
    {
        // Check if grid manager ref exists.
        if (gridManager == null) return;
        
        // Get each Node object according to its position.
        Node node = gridManager.GetNode(position);
        
        // Check if Node exists.
        if (node == null) return;

        // First check if that Node is blocked.
        if (!node.isWalkable)
            label.color = blockedColor;
        // Then check if that Node is included in current path.
        else if (node.isPath)
            label.color = pathColor;
        // Next check if that Node is visited before.
        else if (node.isVisited)
            label.color = visitedColor;
        // All other Nodes are just default color.
        else
            label.color = defaultColor;
    }

    /**
         * Allow user to press C key to active/de-active each tile object's TextMeshPro component. 
         */
    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
            label.enabled = !label.IsActive();
    }
}
