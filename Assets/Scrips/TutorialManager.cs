using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    
    [Header("Kéo tấm nền mờ DarkBackground vào đây")]
    public GameObject darkBackground; 
    public GameObject[] objectsToHide;

    void Start()
    {
        if(PlayerPrefs.GetInt("TutorialShown", 0) == 0)
        {

            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(false); // Ẩn các đối tượng cần ẩn
                }
            }
            darkBackground.SetActive(true); // Bật tấm nền mờ
            howToPlayPanel.SetActive(true); // Bật bảng hướng dẫn
            Time.timeScale = 0f; // Dừng game
        }
    }

    public void CloseTutorial()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(true); // Hiện lại các đối tượng đã ẩn
            }
        }
        darkBackground.SetActive(false); // Tắt tấm nền mờ
        howToPlayPanel.SetActive(false); // Tắt bảng hướng dẫn
        Time.timeScale = 1f; // Tiếp tục game

        PlayerPrefs.SetInt("TutorialShown", 1);
        PlayerPrefs.Save();
    }
}