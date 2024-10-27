using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _longestTimeText;
    
    TextMeshProUGUI _timerText;
    GameSession _gameSession;
    MyTime _currentTime;

    public TextMeshProUGUI TimerText => _timerText;

    void Awake() {
        _timerText = GetComponent<TextMeshProUGUI>();
        _gameSession = FindObjectOfType<GameSession>();
    }    

    void Start()
    {
        _currentTime = new MyTime();
        _longestTimeText.text = _gameSession.LongestTime.ToString();
        StartCoroutine(Tick());
    }

    IEnumerator Tick() {
        while(true) {
            yield return new WaitForSeconds(0.1f);
            if(!_gameSession.PlayerIsAlive) { break; }

            _currentTime.AddDeciseconds(1);
            _gameSession.UpdateLongestTime(_currentTime.Values);
        
            _timerText.text = _currentTime.ToString();
            _longestTimeText.text = _gameSession.LongestTime.ToString();
        }
    }
}
