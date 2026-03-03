using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
        loadingController.LoadScene("GamePlay");
    }
    
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        
        loadingController.LoadScene("StartGame");
    }
   
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GamePlay");
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("CurrentLevel", 0); // reset level về 0 để chơi lại từ đầu
        loadingController.LoadScene("GamePlay");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Clicked");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
