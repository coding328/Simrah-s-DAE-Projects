using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CarvingManager : MonoBehaviour
{
    public GameObject carvingTool;  // The carving tool (chisel)
    public GameObject woodBlock;  // The wood block where we carve
    public GameObject crescentMoonOutline;  // The crescent moon shape
    public TextMeshProUGUI feedbackText;  // The TextMeshProUGUI component for feedback

    private bool isCarvingComplete = false;
    private bool isCarvedOutside = false;

    // HashSet to track carved positions
    private HashSet<Vector2> carvedPositions = new HashSet<Vector2>();
    private HashSet<Vector2> previousCarvedPositions = new HashSet<Vector2>();

    private bool isCarvingStarted = false;  // Track if carving has started

    void Start()
    {
        feedbackText.text = "";  // Start with no feedback
    }

    void Update()
    {
        if (isCarvingComplete) return;  // Don't do anything if the game is complete

        // Move the carving tool to where the mouse is
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        carvingTool.transform.position = mousePosition;

        // Carve when the mouse is pressed
        if (Input.GetMouseButton(0))  // Left click to carve
        {
            Carve();
        }

        // Restart the game if carving is outside the moon shape
        if (isCarvedOutside)
        {
            RestartGame();
        }
    }

    void Carve()
    {
        // Check if the carving tool is inside the wood block
        if (IsCarvingInsideWood())
        {
            if (!isCarvingStarted)  // The first time the chisel touches the wood
            {
                feedbackText.text = "Good Start!";  // Display "Good Start!" when the chisel touches the wood
                isCarvingStarted = true;
            }

            // Record this position as a carved spot
            Vector2 carvingPosition = carvingTool.transform.position;
            if (!carvedPositions.Contains(carvingPosition))
            {
                carvedPositions.Add(carvingPosition);
                // Optionally, you could apply a texture effect or color to the wood block here
            }

            // Check if the player carved outside the outline
            if (IsTouchingMoonOutline())
            {
                isCarvedOutside = true;
            }

            // If carving is complete (enough area carved), show the "Well Done!" message
            if (IsCarvingComplete())
            {
                CompleteCarving();
            }
        }
        else
        {
            // If chisel touches the outline of the crescent moon, restart the game
            if (IsTouchingMoonOutline())
            {
                feedbackText.text = "Try Again!";  // Show "Try Again!"
                RestoreCarvingProgress();  // Restore the carved state
                return;  // Exit the function so we don't continue carving
            }
        }
    }

    bool IsCarvingInsideWood()
    {
        // Check if the carving tool is touching the wood block
        Collider2D woodCollider = woodBlock.GetComponent<Collider2D>();

        // Return true if the carving tool is inside the wood block collider
        return woodCollider != null && woodCollider.OverlapPoint(carvingTool.transform.position);
    }

    bool IsTouchingMoonOutline()
    {
        // Check if the carving tool is touching the crescent moon outline
        Collider2D moonCollider = crescentMoonOutline.GetComponent<Collider2D>();
        return moonCollider != null && moonCollider.OverlapPoint(carvingTool.transform.position);
    }

    bool IsCarvingComplete()
    {
        // Check if enough area is carved. For now, just check if we've carved at least 50 positions
        return carvedPositions.Count > 50;  // Adjust this threshold as needed
    }

    void CompleteCarving()
    {
        isCarvingComplete = true;
        feedbackText.text = "WELL DONE!";  // Show the "Well Done!" message
    }

    void RestoreCarvingProgress()
    {
        // Restore previous carved positions
        carvedPositions = new HashSet<Vector2>(previousCarvedPositions);

        // Reset the wood block's color (or you can reset other states)
        woodBlock.GetComponent<SpriteRenderer>().color = Color.white;  // Reset to original color

        // Redraw the carved positions (if needed, you could create a texture effect for the carved area)
        foreach (var pos in carvedPositions)
        {
            // You could add some carving effect here, like changing the wood color to gray for carved areas
            woodBlock.GetComponent<SpriteRenderer>().color = Color.gray; // Or apply carving effect here
        }
    }

    void RestartGame()
    {
        feedbackText.text = "Try Again!";  // Show the "Try Again!" message
        isCarvingComplete = false;
        isCarvedOutside = false;

        // Save current carved state before resetting
        previousCarvedPositions = new HashSet<Vector2>(carvedPositions);
        carvedPositions.Clear();

        // Reset the wood block's color (or you can reset other states)
        woodBlock.GetComponent<SpriteRenderer>().color = Color.white;  // Reset to original color
    }
}
