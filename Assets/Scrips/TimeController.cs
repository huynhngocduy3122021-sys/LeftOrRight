using UnityEngine;
using System;
using TMPro;

public class TimeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float time = 120f;
    private float currentTime;
    private bool isTimeUp = false;
    public TMP_Text timeText;

    public Action OnTimeUp;
    

    // Update is called once per frame
    void Update()
    {
        if(isTimeUp)
        {
            currentTime -= Time.deltaTime;
            updateTimeUI();

            if(currentTime <= 0)
            {
                isTimeUp = false;
                currentTime = 0;
                updateTimeUI();
                OnTimeUp?.Invoke();
            }
            
            
        }
        
    }
    public void startTimer()
    {
        currentTime = time;
        isTimeUp = true;
    }


    private void updateTimeUI()
    {
        timeText.text = $"{Mathf.CeilToInt(currentTime)}";
    }
}
