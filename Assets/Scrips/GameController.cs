using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameCOntroller : MonoBehaviour
{
    public static GameCOntroller Instance;
    public TMP_Text questionText;
    public TMP_Text levelText;
    public List<GameLevel> gameLevelList;

    private int currentLevel = 0;// quản lý level của màng chơi
    private int currentQuestionIndex = 0;   // quản lí câu hỏi cảu màng chơi
    private string correctAnswer;
    public GameObject tutorialPanel;
    

    public bool isProcessing = false;
    public CharacterController playerController;
    public TimeController timeController;

    public AnimationBall animationBall;

    private void Awake()
    {
        Instance = this;

        if(timeController != null)
        {
            timeController.startTimer();
        }
        
    }

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        currentQuestionIndex = 0;

        if(timeController != null)
        {
            timeController.OnTimeUp = TimeOut;
        }
        CheckTutorial();
        LoadLevel();
    }

    void LoadLevel()
    {
        if (currentLevel >= gameLevelList.Count)
        {
            Debug.Log("You win!");

             PlayerPrefs.DeleteKey("CurrentLevel"); // xoá cả màng chơi để chơi lại từ đầu
             SceneManager.LoadScene("StartGame");
            return;
        }

        GameLevel levelData = gameLevelList[currentLevel];

        if(currentQuestionIndex >= levelData.listLevel.Count)
        {
            Debug.Log("Hoàn thành level " + currentLevel);
            currentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            SceneManager.LoadScene("Win"); // tải lại scene để load level mới
            return;
        }

        GameData questionData = levelData.listLevel[currentQuestionIndex];

        questionText.text =  $"{questionData.numberA} ? {questionData.numberB}";
        levelText.text = $"Màn: {currentLevel + 1}";
        correctAnswer = questionData.numberA < questionData.numberB ? "<": ">";
        isProcessing = false;

       
  
        
    }

    public void checkAnswer(string playerAnswer)
    {
        if (isProcessing)
            return;

        isProcessing = true;

        

        if (playerAnswer == correctAnswer)
        {
            Debug.Log("Đúng rồi!");
            if(animationBall != null)
            {
                animationBall.PlayCorrectAnimation();
            }
            currentQuestionIndex++;
            
        }
        else
        {
            Debug.Log("Sai rồi! Thử lại.");
           if(animationBall != null)
            {
                animationBall.PlayIncorrectAnimation();
            }
            isProcessing = false;
        }

        // playerController.ResetPosition();

        Invoke(nameof(LoadLevel), 1.5f);
    }
    
    private void TimeOut()
    {
       

        Debug.Log("Hết giờ! Thử lại.");
        isProcessing = true;
        playerController.ResetPosition();
        SceneManager.LoadScene("Lose");
    }
    void CheckTutorial()
    {
        if(PlayerPrefs.GetInt("TutorialShown", 0) == 0)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0f; // dừng game
        }
    }
   

    

}
