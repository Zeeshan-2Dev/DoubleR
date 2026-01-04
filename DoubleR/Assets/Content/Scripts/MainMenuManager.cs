using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void StartGame()
    {
        // Replace "GameScene" with the exact name of your game scene
        SceneManager.LoadScene("City_Map");
    }
}
