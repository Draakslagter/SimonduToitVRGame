using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI cleanText;
    [SerializeField] private TextMeshProUGUI brokenText;
    [SerializeField] private TextMeshProUGUI timerText;
    private void Start()
    {
        ScoreManager.Instance.OnScoreChange.AddListener(UpdateScoreUI);
        Timer.Instance.onTimer.AddListener(UpdateTimerUI);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChange.RemoveListener(UpdateScoreUI);
        Timer.Instance.onTimer.RemoveListener(UpdateTimerUI);
    }

    private void UpdateScoreUI(int score, int cleanAmount, int brokenAmount)
    {
       scoreText.text = $"Score: {score}";
       cleanText.text = $"Items Cleaned: {cleanAmount}";
       brokenText.text = $"Items Broken: {brokenAmount}";
    }

    private void UpdateTimerUI(float time)
    {
        timerText.text = $"Time: {time:00}";
    }
}
