using UnityEngine;

public class Regenerator : MonoBehaviour
{
    [SerializeField] float _healthPercent = 1f;
    Health _playerHealth;
    GameSession _gameSession;
    

    void Awake() {
        _playerHealth = GetComponent<Health>();
        _gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        // constant regeneration
        RegenerateHealth();
    }

    void RegenerateHealth() {
        // amount of health equivelant to specified percent out of total health
        float regenerationRate = _healthPercent * _playerHealth.MaxHP;
        // multiply by Time.deltaTime so healing is consistent regardless of FPS
        _playerHealth.Heal(regenerationRate * Time.deltaTime);
        _gameSession.SetHealthUI(_playerHealth.HP, _playerHealth.MaxHP);
    }
}
