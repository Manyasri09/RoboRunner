using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the scene at index 1 (your main game scene)
    public void PlayGame()
    {
        Debug.Log("<color=green>[MainMenu]</color> Play button clicked — loading Scene 1...");
        SceneManager.LoadScene(1);  // Loads scene with build index 1
    }

    // Optional: quit button (for Windows builds)
    public void QuitGame()
    {
        Debug.Log("<color=red>[MainMenu]</color> Quit button clicked — exiting game...");
        Application.Quit();
    }
}
