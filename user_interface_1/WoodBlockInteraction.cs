using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // For interacting with UI elements like buttons

public class WoodBlockInteraction : MonoBehaviour
{
    public GameObject carvingBackground;  // The background object to hide when carving
    public GameObject crescentMoon;       // GameObject for the crescent moon area (separate collider)
    public Button restartButton;          // Reference to the restart button
    private Collider2D woodCollider;      // Collider for the wooden block
    private Collider2D crescentCollider;  // Collider for the crescent moon area

    private int spacebarPressCount = 0;   // Counter to track spacebar presses
    private const int spacebarPressGoal = 10;  // Number of presses required to hide the background

    void Start()
    {
        // Get the colliders for the wooden block and crescent moon area
        woodCollider = GetComponent<Collider2D>();  // Assuming this script is attached to the wood block
        crescentCollider = crescentMoon.GetComponent<Collider2D>();  // Assuming crescent moon is a separate GameObject

        // Ensure the restart button is hidden initially
        restartButton.gameObject.SetActive(false);  // Hide the restart button at the start
    }

    void Update()
    {
        // Check if the mouse is over the wood block or crescent moon area
        bool isMouseOverWoodBlock = IsMouseOverCollider(woodCollider);
        bool isMouseOverCrescent = IsMouseOverCollider(crescentCollider);

        // When the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Increment the spacebar press counter
            spacebarPressCount++;

            // If spacebar has been pressed enough times, hide the background
            if (spacebarPressCount >= spacebarPressGoal)
            {
                carvingBackground.SetActive(false); // Hide the background
                restartButton.gameObject.SetActive(true);  // Show the restart button
            }

            // Check if the spacebar is pressed over the crescent moon area
            if (isMouseOverCrescent)
            {
                // Make the restart button visible if the spacebar is pressed on the moon silhouette
                restartButton.gameObject.SetActive(true);
            }
        }
    }

    // Helper method to check if the mouse is over a specific collider
    private bool IsMouseOverCollider(Collider2D collider)
    {
        // Get the mouse position in world coordinates
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Check if the mouse position is inside the collider bounds
        return collider.OverlapPoint(mousePos);
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
