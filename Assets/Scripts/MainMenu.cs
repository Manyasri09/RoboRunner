using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        Debug.Log("<color=green>[MainMenu]</color> Play button clicked — loading Scene 1...");
        SceneManager.LoadScene(1); 
    }

    public void QuitGame()
    {
        Debug.Log("<color=red>[MainMenu]</color> Quit button clicked — exiting game...");
        Application.Quit();
    }
}
