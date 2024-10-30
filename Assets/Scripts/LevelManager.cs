using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float _gameOverLoadDelay = 1f;

    SaveManager _saveManager;

    void Awake() {
        _saveManager = FindObjectOfType<SaveManager>();
    }

    // loading from main menu or gameover menu
    public void LoadGame() {
        if(_saveManager.CurrentFile == null) { return; }
        // these don't destroy on load, so we destroy manually
        // destroy game session when starting new game session
        DestroyGameSession();
        DestroyMenuAudioPlayer();
        SceneManager.LoadScene("Gameplay");
    }

    // loading from gameover menu
    public void LoadMainMenu() {
        // no need for game session when going to main menu
        DestroyGameSession();
        SceneManager.LoadScene("MainMenu");
    }
    // delay loading game over scene
    public void LoadDelayedGameOver() => Invoke(nameof(LoadGameOver), _gameOverLoadDelay);
    public void QuitGame() => Application.Quit();

    void LoadGameOver() => SceneManager.LoadScene("GameOver");
    
    // finds and destroys game session if exists
    void DestroyGameSession() {
        DestroyIfExists(FindObjectOfType<GameSession>());
    }

    void DestroyMenuAudioPlayer() {
        DestroyIfExists(FindObjectOfType<MenuAudioPlayer>());
    }

    void DestroyIfExists(Component component) {
        if(component != null) {
            Destroy(component.gameObject);
        }
    }
}
