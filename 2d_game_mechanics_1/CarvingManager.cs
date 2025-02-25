using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;  // Add this to fix the error

public class CarvingManager : MonoBehaviour
{
    public GameObject carvingTool;  // The carving tool (chisel)
    public GameObject woodBlock;  // The wood block where we carve
    public GameObject crescentMoonOutline;  // The crescent moon shape
    public Text feedbackText;  // The UI text to display feedback


    private bool isCarvingComplete = false;
    private bool isCarvedOutside = false;

    // For simplicity, you could track if we've carved enough to finish the task
    private HashSet<Vector2> carvedPositions = new HashSet<Vector2>(); // Set of positions that have been carved

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
        // Check if the carving tool is inside the moon outline
        if (IsCarvingInsideMoonOutline())
        {
            // Simulate carving by changing the wood color to gray (you can add better effects here)
            woodBlock.GetComponent<SpriteRenderer>().color = Color.gray;

            // Record this position as a carved spot (we add the position of the carving tool)
            Vector2 carvingPosition = carvingTool.transform.position;
            if (!carvedPositions.Contains(carvingPosition))
            {
                carvedPositions.Add(carvingPosition);
            }

            // Check if the player carved outside the outline
            if (!IsCarvingInsideMoonOutline())
            {
                isCarvedOutside = true;
            }

            // If carving is complete (enough area carved), show the "Well Done!" message
            if (IsCarvingComplete())
            {
                CompleteCarving();
            }
        }
    }

    bool IsCarvingInsideMoonOutline()
    {
        // Use a 2D Collider (e.g., BoxCollider2D) on the crescentMoonOutline to check if the carving tool is within it
        Collider2D moonCollider = crescentMoonOutline.GetComponent<Collider2D>();

        // Check if the carving tool's position is inside the moon's collider area
        if (moonCollider != null)
        {
            return moonCollider.OverlapPoint(carvingTool.transform.position);
        }

        return false;  // Default return if no collider is attached
    }

    bool IsCarvingComplete()
    {
        // Check if a sufficient number of carving positions are covered.
        return carvedPositions.Count > 50;  // Adjust this threshold as needed
    }

    void CompleteCarving()
    {
        isCarvingComplete = true;
        feedbackText.text = "WELL DONE!";  // Show the "Well Done!" message
    }

    void RestartGame()
    {
        feedbackText.text = "Try Again!";  // Show the "Try Again!" message
        isCarvingComplete = false;
        isCarvedOutside = false;

        // Reset the wood block's color (or you can reset other states)
        woodBlock.GetComponent<SpriteRenderer>().color = Color.white;  // Reset to original color

        // Clear the carved positions
        carvedPositions.Clear();
    }
}
