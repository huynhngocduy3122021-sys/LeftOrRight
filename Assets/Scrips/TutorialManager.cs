using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject howToPlayPanel;

    void Start()
    {
        if(PlayerPrefs.GetInt("TutorialShown", 0) == 0)
        {
            howToPlayPanel.SetActive(true);
            Time.timeScale = 0f; // dừng game
        }
    }

    public void CloseTutorial()
    {
        howToPlayPanel.SetActive(false);
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("TutorialShown", 1);
        PlayerPrefs.Save();
    }
}