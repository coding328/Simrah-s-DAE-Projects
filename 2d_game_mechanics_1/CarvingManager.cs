using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CarvingManager : MonoBehaviour
{
    public GameObject carvingTool;  // The carving tool (chisel)
    public GameObject woodBlock;  // The wood block where we carve
    public GameObject crescentMoonOutline;  // The crescent moon shape
    public TextMeshProUGUI feedbackText;  // The TextMeshProUGUI component for feedback
    public float carvingRadius = 0.5f;  // The radius around the crescent moon to check for carving completion
    private Material woodMaterial;  // Material of the wood block

    private HashSet<Vector2> carvedPositions = new HashSet<Vector2>();  // Tracks carved positions
    private HashSet<Vector2> savedCarvedPositions = new HashSet<Vector2>();  // To save progress
    private int clickCount = 0;  // To track how many valid places the player has clicked
    private bool isCarvingToolTouchingMoon = false;  // To track if carving tool is touching moon outline
    private bool isWellDone = false;  // Flag to track if the player has carved all around the moon

    void Start()
    {
        feedbackText.text = "";  // Start with no feedback
        LoadProgress();  // Load any previously saved progress

        // Initialize wood material from wood block's sprite renderer
        woodMaterial = woodBlock.GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        // Move the carving tool to the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        carvingTool.transform.position = mousePosition;

        // Carve when the mouse is pressed
        if (Input.GetMouseButtonDown(0))  // Left click to carve
        {
            Carve(mousePosition);
        }

        // Check if the carving tool touches the crescent moon outline
        if (IsTouchingMoonOutline())
        {
            if (!isCarvingToolTouchingMoon) // Only trigger once
            {
                isCarvingToolTouchingMoon = true;
                HandleMoonTouch();  // Handle crescent moon touch logic
            }
        }
        else
        {
            isCarvingToolTouchingMoon = false;
        }
    }

    void Carve(Vector2 mousePosition)
    {
        // Check if the carving tool is inside the wood block (but outside the crescent moon)
        if (IsCarvingInsideWood())
        {
            if (!carvedPositions.Contains(mousePosition))
            {
                carvedPositions.Add(mousePosition);
                ChangeWoodColorAtPosition(mousePosition);

                clickCount++;

                // Save progress every 5 clicks
                if (clickCount == 5)
                {
                    SaveProgress();
                    feedbackText.text = "Progress Saved!";
                }
                // Save progress again after 10 clicks
                else if (clickCount == 10)
                {
                    SaveProgress();
                    feedbackText.text = "Progress Saved Again!";
                }

                // Check if the player has completed carving around the crescent moon
                CheckCarvingCompletion();
            }
        }
    }

    bool IsCarvingInsideWood()
    {
        // Check if the carving tool is inside the wood block collider
        Collider2D woodCollider = woodBlock.GetComponent<Collider2D>();
        return woodCollider != null && woodCollider.OverlapPoint(carvingTool.transform.position);
    }

    bool IsTouchingMoonOutline()
    {
        // Check if the carving tool is touching the crescent moon outline
        Collider2D carvingToolCollider = carvingTool.GetComponent<Collider2D>();
        Collider2D moonCollider = crescentMoonOutline.GetComponent<Collider2D>();

        return carvingToolCollider != null && moonCollider != null && carvingToolCollider.IsTouching(moonCollider);
    }

    void HandleMoonTouch()
    {
        // Turn the crescent moon outline grey to indicate touch
        SpriteRenderer moonRenderer = crescentMoonOutline.GetComponent<SpriteRenderer>();
        Color originalColor = moonRenderer.color;
        moonRenderer.color = Color.grey;

        // Display "Try Again" message
        feedbackText.text = "Try Again!";

        // Reset the game after a brief delay
        Invoke("ResetGameAfterMoonTouch", 2f);  // 2 seconds delay for feedback and reset
    }

    void ResetGameAfterMoonTouch()
    {
        // Restore the original crescent moon outline color
        SpriteRenderer moonRenderer = crescentMoonOutline.GetComponent<SpriteRenderer>();
        moonRenderer.color = Color.black;

        // Restore the carved positions to the last saved state
        RestoreProgress();

        // Reset the wood block to its original state
        woodBlock.GetComponent<SpriteRenderer>().color = Color.white;  // Reset to white
        feedbackText.text = "";  // Clear the "Try Again" message
    }

    void ChangeWoodColorAtPosition(Vector2 position)
    {
        // Convert world position to normalized UV position for the shader
        Vector2 woodBlockSize = woodBlock.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 localPos = position - (Vector2)woodBlock.transform.position;
        Vector2 uvPosition = new Vector2(localPos.x / woodBlockSize.x, localPos.y / woodBlockSize.y);

        // Update the shader with the new carving position
        woodMaterial.SetVector("_CarvePos", new Vector4(uvPosition.x, uvPosition.y, 0, 0));
        woodMaterial.SetFloat("_CarveRadius", carvingRadius);
    }

    void SaveProgress()
    {
        // Save the carved positions to PlayerPrefs
        List<Vector2> savedList = new List<Vector2>(carvedPositions);
        string savedData = "";

        foreach (var pos in savedList)
        {
            savedData += $"{pos.x},{pos.y}|";
        }

        PlayerPrefs.SetString("CarvedPositions", savedData);  // Save positions as a string
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        // Load the saved carved positions from PlayerPrefs
        if (PlayerPrefs.HasKey("CarvedPositions"))
        {
            string savedData = PlayerPrefs.GetString("CarvedPositions");
            string[] positions = savedData.Split('|');

            foreach (var position in positions)
            {
                if (!string.IsNullOrEmpty(position))
                {
                    string[] coords = position.Split(',');
                    Vector2 loadedPos = new Vector2(float.Parse(coords[0]), float.Parse(coords[1]));
                    carvedPositions.Add(loadedPos);
                    ChangeWoodColorAtPosition(loadedPos);
                }
            }
        }
    }

    void RestoreProgress()
    {
        // Restore the last saved carved positions
        carvedPositions = new HashSet<Vector2>(savedCarvedPositions);
        feedbackText.text = "Game Restarted!";
        // Reset wood color to the last saved color or default
        woodBlock.GetComponent<SpriteRenderer>().color = Color.white;  // Reset to white
    }

    void CheckCarvingCompletion()
    {
        // Check if all points around the crescent moon have been carved
        if (isWellDone) return;  // If already marked as well done, no need to check again

        // Get the position of the crescent moon outline
        Vector2 moonCenter = crescentMoonOutline.transform.position;

        // Check if all carved points are within the specified radius around the crescent moon
        bool isComplete = true;
        foreach (var carvedPos in carvedPositions)
        {
            if (Vector2.Distance(moonCenter, carvedPos) > carvingRadius)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            isWellDone = true;
            feedbackText.text = "Well Done!";  // Display "Well Done!" when the carving is completed around the moon
        }
    }
}
