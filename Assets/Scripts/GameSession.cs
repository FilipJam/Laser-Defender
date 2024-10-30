using UnityEngine;
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
    MyTime _longestTime;
    SaveManager _saveManager;
    int _score = 0;
    int _highScore = 0;
    int _previousHighscore = 0;
    bool _playerIsAlive = true;

    public int Score => _score;
    public int HighScore => _highScore;
    public int PreviousHighscore => _previousHighscore;
    public MyTime LongestTime => _longestTime;
    public bool PlayerIsAlive { get => _playerIsAlive; set => _playerIsAlive = value; }
    
    void Awake() {
        _timer = FindObjectOfType<Timer>();
        _saveManager = FindObjectOfType<SaveManager>();
        _longestTime = new MyTime();

        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        // maintains singleton pattern
        if(numGameSessions > 1) {
            // destroy copy
            Destroy(gameObject);
        } else {
            // carry original instance over to other scenes
            DontDestroyOnLoad(gameObject);
            LoadState(_saveManager.CurrentSave);
        }
    }

    void Start() {
        UpdateHighScoreUI();
    }

    public void SetHealthUI(float health, float maxHealth) {
        // slider represents percentage of total health remaining
        _healthSlider.value = health / maxHealth * 100;
    }

    public void AddScore(int amount) {
        _score += amount;
        // update display with updated score
        _scoreText.text = _score.ToString();
        UpdateHighScoreUI();
    }

    public void UpdateLongestTime(int[] time) {
        // start from largest time unit (minutes)
        for (int i = time.Length - 1; i >= 0; i--)
        {
            // if largest unit is less, no point in checking the rest
            if(time[i] < _longestTime.Values[i]) {
                return;
            }
            // as soon as we find one that is bigger, the time as a whole is bigger
            if(time[i] > _longestTime.Values[i]) {
                // assign larger time
                _longestTime.Values = time;
                _timer.TimerText.color = _personalRecordColor;
                return;
            }
        }
    }

    public void SaveGame() {
        if(!_allowSaving) { return; }
        _saveManager.SaveFile(new Save(_highScore, _longestTime.Values));
    }

    public void LoadState(Save save) {
        // sync game session with values from current save
        _previousHighscore = _highScore = save.Highscore;
        _longestTime.Values = save.LongestRun.Values;
    }

    void UpdateHighScoreUI() {
        if(_score > _highScore) {
            _highScore = _score;
            _scoreText.color = _personalRecordColor;
        }
        
        // update display with updated highscore value
        _highScoreText.text = _highScore.ToString();
    }

}
