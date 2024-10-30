using UnityEngine;

public class MenuAudioPlayer : MonoBehaviour
{
    void Awake() {
        int numMenuPlayers = FindObjectsByType<MenuAudioPlayer>(FindObjectsSortMode.None).Length;

        if(numMenuPlayers == 1) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
