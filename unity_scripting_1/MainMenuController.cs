using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class MainMenuController : MonoBehaviour
{
    // This function will be called when the Start button is clicked
    public void StartGame()
    {
        // Load the GameScene
        SceneManager.LoadScene("GameScene");
    }

    // Start is called once before the first frame update
    void Start()
    {
        // Initialization logic (if needed)
    }

    // Update is called once per frame
    void Update()
    {
        // Logic to run every frame (if needed)
    }
}
