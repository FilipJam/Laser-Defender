using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float _gameOverLoadDelay = 1f;

    SaveManager _saveManager;

    void Awake() {
        _saveManager = FindObjectOfType<SaveManager>();
    }

    public void LoadGame() {
        if(_saveManager.CurrentFile == null) { return; }
        DestroyGameSession();
        DestroyIfExists(FindObjectOfType<MenuAudioPlayer>().gameObject);
        SceneManager.LoadScene("Gameplay");
    }
    public void LoadMainMenu() {
        DestroyGameSession();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver() => Invoke(nameof(DelayLoadGameOver), _gameOverLoadDelay);

    public void QuitGame() => Application.Quit();

    void DelayLoadGameOver() => SceneManager.LoadScene("GameOver");

    void DestroyGameSession() {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if(gameSession != null) {
            Destroy(gameSession.gameObject);
        }
    }

    void DestroyIfExists(GameObject obj) {
        if(obj != null) {
            Destroy(obj);
        }
    }

}
