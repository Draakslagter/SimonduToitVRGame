using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    [SerializeField] private float gameTime;
    public UnityEvent<float> onTimer;
    public UnityEvent onTimerEnd;
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
    private void FixedUpdate()
    {
        KeepTime();
    }

    private void KeepTime()
    {
        
        if (gameTime <= 0)
        {
            gameTime = 0;
            onTimerEnd.Invoke();
        }
        else
        {
            gameTime -= Time.deltaTime;
        }
        onTimer.Invoke(gameTime);
    }
}
