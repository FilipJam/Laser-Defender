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
        if(_gameSession == null) { return; }

        string displayText = "You Scored:\n" + _gameSession.Score + "\n";
        // add new line of text if score beat previous highscore
        if(_gameSession.Score > _gameSession.PreviousHighscore) {
            displayText += "You beat your Highscore: " + _gameSession.PreviousHighscore;
        }

        // updates display with modified text
        GetComponent<TextMeshProUGUI>().text = displayText;
    }
}
