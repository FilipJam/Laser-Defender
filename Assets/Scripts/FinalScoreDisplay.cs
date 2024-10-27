using UnityEngine;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{

    GameSession _gameSession;

    void Awake() {
        _gameSession = FindObjectOfType<GameSession>();
    }
    void Start()
    {
        string displayText = "You Scored:\n" + (_gameSession?.Score ?? 0) + "\n";
        if(_gameSession != null && _gameSession.Score > _gameSession.PreviousHighscore) {
            displayText += "You beat your Highscore: " + _gameSession.PreviousHighscore;
        }
        GetComponent<TextMeshProUGUI>().text = displayText;
    }
    


}
