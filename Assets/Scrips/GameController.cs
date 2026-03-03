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
    

    public bool isProcessing = false;
    public CharacterController playerController;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        currentQuestionIndex = 0;
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
        levelText.text = $"Màng: {currentLevel + 1}";
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
            currentQuestionIndex++;
            
        }
        else
        {
            Debug.Log("Sai rồi! Thử lại.");
            isProcessing = false;
        }

        playerController.ResetPosition();

        Invoke(nameof(LoadLevel), 1.5f);
    }


}
