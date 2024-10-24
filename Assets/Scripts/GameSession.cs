using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highScoreText;
    [SerializeField] Color _personalRecordColor;
    [SerializeField] bool _allowSaving;

    Timer _timer;

    SaveManager _saveManager;
    int _score = 0;

    int _highScore = 0;

    int _previousHighscore = 0;

    int[] _longestTime = new int[3];

    bool _playerIsAlive = true;


    public int Score => _score;
    public int HighScore => _highScore;

    public int PreviousHighscore => _previousHighscore;
    public int[] LongestTime => _longestTime;

    public string FormattedLongestTime => string.Format("{2}:{1}.{0}", 
                                                        _longestTime[0], 
                                                        FormatTimeValue(_longestTime[1]), 
                                                        FormatTimeValue(_longestTime[2]));

    public bool PlayerIsAlive {get => _playerIsAlive; set => _playerIsAlive = value; }
    
    void Awake() {
        _timer = FindObjectOfType<Timer>();
        _saveManager = FindObjectOfType<SaveManager>();
        //Debug.Log(_saveManager.CurrentSave);

        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if(numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            LoadState(_saveManager.CurrentSave);
        }
    }

    void Start() {
        SetUp();
        UpdateHighScoreUI();
    }

    public void SetHealthUI(float health, float maxHealth) {
        _healthSlider.value = health / maxHealth * 100;
    }

    public void AddScore(int amount) {
        _score += amount;
        _scoreText.text = _score.ToString();
        UpdateHighScoreUI();
    }

    public void UpdateLongestTime(int[] time) {
        int index = time.Length-1;
        while(time[index] == _longestTime[index]) {
            if(index == 0) return;
            index--;
        }

        if(time[index] > _longestTime[index]) {
            for(int i = 0; i < time.Length; i++){
                _longestTime[i] = time[i];
            }
            _timer.TimerText.color = _personalRecordColor;
        }
    }

    public void SaveGame() {
        if(!_allowSaving) { return; }
        _saveManager.SaveFile(new Save(_highScore, _longestTime));
    }

    public void LoadState(Save save) {
        _highScore = save.Highscore;
        _previousHighscore = _highScore;
        _longestTime = save.LongestRun;
    }

    void SaveLongestTime() {
        string[] timeKeys = {"Deciseconds", "Seconds", "Minutes"};
        for(int i = 0; i < timeKeys.Length; i++) {
            PlayerPrefs.SetInt(timeKeys[i], _longestTime[i]);
        }
    }

    void LoadLongestTime() {
        string[] timeKeys = {"Deciseconds", "Seconds", "Minutes"};
        for (int i = 0; i < timeKeys.Length; i++)
        {
            _longestTime[i] = PlayerPrefs.GetInt(timeKeys[i]);
        }

        // TODO
        // _longestTime = _saveManager.LongestRun;
    }

    void Restart() {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetUp(){
        Health playerHealth = GameObject.Find("Player").GetComponent<Health>();
        _healthSlider.value = _healthSlider.maxValue = playerHealth.HP;
    }

    void UpdateHighScoreUI() {
        if(_score > _highScore) {
            _highScore = _score;
            _scoreText.color = _personalRecordColor;
        }
        
        _highScoreText.text = _highScore.ToString();
    }

    string FormatTimeValue(int num) => (num < 10 ? "0" : "") + num;

}
