using UnityEngine;

public class CarvingToolScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Update the carving tool's position to follow the mouse
        transform.position = mousePos;
    }
}
