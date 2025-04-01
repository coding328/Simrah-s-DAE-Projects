using UnityEngine;
using UnityEngine.UI;  // To interact with UI components like Slider and Text

public class ProgressBarScript : MonoBehaviour
{
    public Slider progressBar;  // Reference to the Slider (Progress Bar)
    public Text progressText;   // Reference to the Text UI element to display progress
    private int spacebarPressCount = 0;  // Counter for spacebar presses
    private const int maxPresses = 10;   // Max presses for the bar to be full
    private float incrementValue = 1f;   // Increment value per spacebar press (larger steps)

    void Start()
    {
        // Ensure the progress bar starts at 0 and the text displays the starting value
        progressBar.value = 0;
        UpdateProgressText();
    }

    void Update()
    {
        // Detect when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Increment the spacebar press count
            spacebarPressCount++;

            // Ensure the spacebar press count doesn't exceed the maximum
            if (spacebarPressCount > maxPresses)
            {
                spacebarPressCount = maxPresses;
            }

            // Increment the slider by the defined increment value
            progressBar.value += incrementValue;

            // Make sure the slider doesn't exceed its maximum value
            if (progressBar.value > progressBar.maxValue)
            {
                progressBar.value = progressBar.maxValue;
            }

            // Update the progress text (e.g., "Progress: 3/10")
            UpdateProgressText();
        }
    }

    // Update the progress text UI element
    private void UpdateProgressText()
    {
        progressText.text = "Progress: " + spacebarPressCount + "/" + maxPresses;
    }
}
