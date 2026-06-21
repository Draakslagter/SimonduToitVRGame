using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{ 
    public static ScoreManager Instance;
    private int _totalScore;
    private int _cleanAmount;
    private int _brokenAmount;
    public UnityEvent<int, int, int> OnScoreChange;
    private bool _gameOver;

   private void Awake()
   {
       if (Instance != null && Instance != this)
       {
           Destroy(this);
       }
       else
       {
           Instance = this;
       }
   }

   private void Start()
   {
       BreakableObject.OnScorePoints += IncreaseScore;
       Timer.Instance.onTimerEnd.AddListener(GameOver);
   }
   
   private void OnDisable()
   {
       BreakableObject.OnScorePoints -= IncreaseScore;
       Timer.Instance.onTimerEnd.RemoveListener(GameOver);
   }
   private void IncreaseScore(int amount, bool clean, bool broken)
    {
        if (_gameOver) return;
        _totalScore += amount;
        if (clean)
        {
            _cleanAmount++;
        }

        if (broken)
        {
            _brokenAmount++;
        }
        OnScoreChange?.Invoke(_totalScore, _cleanAmount, _brokenAmount);
        if (_brokenAmount > 0)
        {
            AudioBehaviour_BGMusic.Instance.ChangeMusic(_totalScore);
        }
        
    }

    private void GameOver()
    {
        _gameOver = true;
    }
}
