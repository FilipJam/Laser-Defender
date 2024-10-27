using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _longestTimeText;
    TextMeshProUGUI _timerText;

    //DebugSession _debugSession;
    GameSession _gameSession;
    int[] _totalTime = new int[3];

    readonly int[] _unitLimits = {10, 60};

    public TextMeshProUGUI TimerText => _timerText;

    void Awake() {
        _timerText = GetComponent<TextMeshProUGUI>();
        //_debugSession = FindObjectOfType<DebugSession>();
        _gameSession = FindObjectOfType<GameSession>();
    }    

    void Start()
    {
        //_longestTimeText.text = GetUpdatedText(_debugSession.LongestTime);
        _longestTimeText.text = GetUpdatedText(_gameSession.LongestTime);
        StartCoroutine(Tick());
    }

    IEnumerator Tick() {
        while(true) {
            yield return new WaitForSeconds(0.1f);
            if(!_gameSession.PlayerIsAlive) { break; }

            AddDeciseconds(1);
            ShiftTimeValues();
            //_debugSession.UpdateLongestTime(_totalTime);
            _gameSession.UpdateLongestTime(_totalTime);
        
            _timerText.text = GetUpdatedText(_totalTime);
            _longestTimeText.text = GetUpdatedText(_gameSession.LongestTime);
        }
    }

    void AddDeciseconds(int sec) {
        _totalTime[0] += sec;
    }

    void ShiftTimeValues() {
        for (int i = 0; i < _totalTime.Length; i++)
        {
            if(i == _totalTime.Length-1) { return; }
            while(_totalTime[i] >= _unitLimits[i]) {
                _totalTime[i] -= _unitLimits[i];
                _totalTime[i+1]++;
            }
        }
    }



    string GetUpdatedText(int[] time) {
        (string ds, string sec, string min) = GetFormattedTime(time);
        return string.Format("{0}:{1}.{2}", min, sec, ds);
    }

    (string, string, string) GetFormattedTime(int[] time) {
        string[] result = new string[time.Length];
        for(int i = 0; i < time.Length; i++) {
            result[i] = i > 0 ? FormatTimeValue(time[i]) : time[i].ToString();
        }
        return (result[0], result[1], result[2]);
    }

    string FormatTimeValue(float timeValue) {
        return (timeValue < 10 ? "0" : "") + timeValue;
    }


}
