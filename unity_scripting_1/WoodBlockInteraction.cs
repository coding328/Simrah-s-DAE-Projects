using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WoodBlockInteraction : MonoBehaviour
{
    public GameObject carvingBackground;
    public GameObject crescentMoon;
    public Button restartButton;

    private Collider2D woodCollider;
    private Collider2D crescentCollider;

    private int spacebarPressCount = 0;
    private const int spacebarPressGoal = 10;

    private Rigidbody2D rb;  // For physics example
    private Vector2 testDirection = Vector2.up;  // Vector variable usage example

    // MonoBehaviour - Awake(): Perform setup before the game starts
    void Awake()
    {
        Debug.Log("Awake: Preparing components and initial setup...");

        // Demonstrate use of GetComponent<T>()
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Debug.Log("Awake: Rigidbody2D found and assigned.");
        }
    }

    // MonoBehaviour - Start(): Initialize game state or references
    void Start()
    {
        Debug.Log("Start: Initializing game objects...");

        // Demonstrate use of GetComponent<T>()
        woodCollider = GetComponent<Collider2D>();
        crescentCollider = crescentMoon.GetComponent<Collider2D>();

        // Hide restart button initially
        restartButton.gameObject.SetActive(false);
    }

    // MonoBehaviour - Update(): Handle per-frame game logic
    void Update()
    {
        // Use of Vector2 static properties for directions
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mouseWorldPos - (Vector2)transform.position).normalized;

        if (directionToMouse == Vector2.right) Debug.Log("Mouse is to the RIGHT of the object.");
        if (directionToMouse == Vector2.left) Debug.Log("Mouse is to the LEFT of the object.");
        if (directionToMouse == Vector2.up) Debug.Log("Mouse is ABOVE the object.");
        if (directionToMouse == Vector2.down) Debug.Log("Mouse is BELOW the object.");

        bool isMouseOverWoodBlock = IsMouseOverCollider(woodCollider);
        bool isMouseOverCrescent = IsMouseOverCollider(crescentCollider);

        // Handle user input: Spacebar interaction
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacebarPressCount++;

            if (spacebarPressCount >= spacebarPressGoal)
            {
                carvingBackground.SetActive(false);
                restartButton.gameObject.SetActive(true);
            }

            if (isMouseOverCrescent)
            {
                restartButton.gameObject.SetActive(true);
            }
        }
    }

    // MonoBehaviour - FixedUpdate(): Handle physics-related logic
    void FixedUpdate()
    {
        if (rb != null)
        {
            // Apply a small dummy force for demonstration (commented to avoid unintended behavior)
            // rb.AddForce(Vector2.up * 0.1f);

            Debug.Log("FixedUpdate: Physics tick processed.");
        }
    }

    // Helper method to detect if the mouse is over a collider
    private bool IsMouseOverCollider(Collider2D collider)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider.OverlapPoint(mousePos);
    }

    // Restart the game scene
    public void RestartGame()
    {
        Debug.Log("RestartGame: Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
