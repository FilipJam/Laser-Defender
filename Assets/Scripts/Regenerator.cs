using UnityEngine;

public class Regenerator : MonoBehaviour
{
    [SerializeField] float _regenerationRate = 1f;
    Health _playerHealth;
    GameSession _gameSession;
    

    void Awake() {
        _playerHealth = GetComponent<Health>();
        _gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateHealth();
    }

    void RegenerateHealth() {
        if(_playerHealth.HP < _playerHealth.MaxHP) {
            _playerHealth.Heal(_regenerationRate * Time.deltaTime);
            _gameSession.SetHealthUI(_playerHealth.HP, _playerHealth.MaxHP);
        }
    }
}
