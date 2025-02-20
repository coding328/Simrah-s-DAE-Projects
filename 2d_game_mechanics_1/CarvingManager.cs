using UnityEngine;

public class CarvingManager : MonoBehaviour
{ using UnityEngine;
using UnityEngine.UI;

public class CarvingManager : MonoBehaviour
{
    public GameObject carvingTool;  // The carving tool (chisel)
    public GameObject woodBlock;  // The wood block where we carve
    public GameObject crescentMoonOutline;  // The crescent moon shape
    public Text feedbackText;  // The UI text to display feedback (Well Done! or Try Again)

    private bool isCarvingComplete = false;
    private bool isCarvedOutside = false;

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
        // Check if the carving tool is within the moon outline
        if (IsCarvingInsideMoonOutline())
        {
            // Simulate carving by changing the wood color to gray (you can add better effects here)
            woodBlock.GetComponent<SpriteRenderer>().color = Color.gray;

            // Check if the player carved outside the outline
            if (!IsCarvingInsideMoonOutline())
            {
                isCarvedOutside = true;
            }

            // If carving is complete, show the "Well Done!" message
            if (IsCarvingComplete())
            {
                CompleteCarving();
            }
        }
    }

    bool IsCarvingInsideMoonOutline()
    {
        // For simplicity, we'll just check if the carving tool is inside the moon outline
        // You can improve this by adding a 2D Collider to the outline and checking if the tool is within it
        return true;  // For now, just return true (you can add better collision later)
    }

    bool IsCarvingComplete()
    {
        // You can add more logic here to check if the moon is fully carved out.
        // For now, we'll say it's complete once the player interacts with the outline.
        return true;
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
    }
}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
