using UnityEngine;

public class DebugSession : MonoBehaviour
{
    float _highScore;

    int[] _longestTime = new int[3];

    public float HighScore => _highScore;

    public int[] LongestTime => _longestTime;

    void Awake() {
        int numDebugSessions = FindObjectsByType<DebugSession>(FindObjectsSortMode.None).Length;
        if(numDebugSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateHighScore(int score) {
        _highScore = Mathf.Max(score, _highScore);
    }

    public void UpdateLongestTime(int[] time) {
        int index = time.Length-1;
        while(time[index] == _longestTime[index]) {
            if(index == 0) return;
            index--;
        }

        if(time[index] > _longestTime[index]) {
            _longestTime = time;
        }
    }


}
