using UnityEngine;
using TMPro;

public class GameCOntroller : MonoBehaviour
{
    public static GameCOntroller Instance;
    public TMP_Text questionText;
    public TMP_Text levelText;
    public GameLevel gameList;

    private int currentLevel = 0;
    private string correctAnswer;
    

    public bool isProcessing = false;
    public CharacterController playerController;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadLevel();
    }

    void LoadLevel()
    {
        if (currentLevel >= gameList.listLevel.Count)
        {
            Debug.Log("You win!");
            return;
        }

        var levelData = gameList.listLevel[currentLevel];

        // Cập nhật UI
        levelText.text = $"Màn: {currentLevel + 1}";
        questionText.text = $"{levelData.numberA} ? {levelData.numberB}";

        // Tính đáp án đúng
        correctAnswer = levelData.numberA < levelData.numberB ? "<" : ">";

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
            currentLevel++;
            
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
