using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;  // To use generic lists

public class CarvingManager : MonoBehaviour
{
    // Public variables that can be set in the Unity Inspector
    public GameObject[] tools;  // Array of tools (Chisel, Hammer, Sander)
    public GameObject currentTool;
    public GameObject woodPiece;  // The piece of wood to carve
    public GameObject carvingOutline;  // The outline of the object to carve

    // Private variables for internal management
    private GameObject currentObjectToCarve;  // The object currently being carved
    private List<GameObject> carvedObjects = new List<GameObject>();  // List to store carved objects

    // UI Buttons for switching tools
    public Button[] toolButtons;

    // Objects that can be carved (e.g., bear, square, etc.)
    public GameObject[] objectsToCarve;  // List of objects to carve, like bear, square, etc.

    // Public list of active carving shapes
    public List<GameObject> activeShapes = new List<GameObject>();  // List of active carving shapes

    // Function to initialize the game (Start function)
    private void Start()
    {
        // Deactivate all tools initially
        foreach (var tool in tools)
        {
            tool.SetActive(false);
        }

        // Initialize the first object to carve (e.g., bear)
        if (objectsToCarve.Length > 0)
        {
            SetCarvingObject(0);  // Set default carving object (bear)
        }

        // Initialize button listeners for switching tools
        foreach (Button button in toolButtons)
        {
            button.onClick.AddListener(() => OnToolButtonClicked(button));
        }

        // Start by showing the first carving shape in the list
        if (activeShapes.Count > 0)
        {
            DisplayCarvingShapes();
        }
    }

    // Function to switch tools when button is clicked
    public void SwitchTool(int toolIndex)
    {
        if (currentTool != null)
        {
            currentTool.SetActive(false);  // Deactivate the current tool
        }

        if (toolIndex < tools.Length)
        {
            currentTool = tools[toolIndex];
            currentTool.SetActive(true);  // Activate the selected tool
        }
        else
        {
            Debug.LogWarning("Tool index out of range!");
        }
    }

    // Function to handle UI button clicks for tool selection
    private void OnToolButtonClicked(Button clickedButton)
    {
        for (int i = 0; i < toolButtons.Length; i++)
        {
            if (clickedButton == toolButtons[i])
            {
                SwitchTool(i);  // Switch tool based on button click
                break;  // Exit the loop once the correct button is found
            }
        }
    }

    // Function to set the object to carve (e.g., bear or other shapes)
    public void SetCarvingObject(int objectIndex)
    {
        if (objectIndex < objectsToCarve.Length)
        {
            if (currentObjectToCarve != null)
                currentObjectToCarve.SetActive(false);  // Deactivate current object

            currentObjectToCarve = objectsToCarve[objectIndex];
            currentObjectToCarve.SetActive(true);  // Activate new object to carve
            carvedObjects.Add(currentObjectToCarve);  // Add object to list of carved objects
        }
        else
        {
            Debug.LogWarning("Object index out of range!");
        }
    }

    // Function to display carving shapes (looping through the active shapes list)
    private void DisplayCarvingShapes()
    {
        // Loop through the activeShapes list and display each carving shape
        foreach (var shape in activeShapes)
        {
            if (shape != null)
            {
                shape.SetActive(true);  // Activate each shape in the active list
            }
        }
    }

    // Function to carve the wood based on user interaction (placeholder for carving logic)
    public void CarveObject(Vector3 hitPoint)
    {
        // Example: If a tool is active, carve the wood at the hit point (using raycasting or mouse position)
        if (currentTool != null)
        {
            Debug.Log($"Carving with {currentTool.name} at {hitPoint}");
            // Here, you would implement actual carving logic, like raycasting or tool interaction
        }
        else
        {
            Debug.LogWarning("No tool selected.");
        }
    }

    // A public function to reset carving (could be used for restarting the game or resetting a carving)
    public void ResetCarving()
    {
        // Reset all carved objects by deactivating them
        foreach (var obj in carvedObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        carvedObjects.Clear();  // Clear the list of carved objects

        // Reset the wood piece to its original state
        if (woodPiece != null)
        {
            woodPiece.SetActive(true);
        }
    }
}


