using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Transform _healthBar;
    [SerializeField] float _maxHealth = 10;
    [SerializeField] bool _isPlayer;


    CameraShake _cameraShake;
    EffectSpawner _effectSpawner;
    GameSession _gameSession;
    GameplayAudioPlayer _audioPlayer;
    LevelManager _levelManager;
    PowerManager _powerManager;

    float _health;
    public float HP => _health;
    public float MaxHP => _maxHealth;


    void Awake() {
        _effectSpawner = FindObjectOfType<EffectSpawner>();
        _gameSession = FindObjectOfType<GameSession>();
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<GameplayAudioPlayer>();
        _levelManager = FindObjectOfType<LevelManager>();
        _powerManager = FindObjectOfType<PowerManager>();
    }

    void Start() {
        _health = _maxHealth;
    }

    public void UpgradeHealth(float value) {
        _maxHealth = value;
        _health = value;
    }

    public void Heal(float amount) {
        _health += amount;
        if(_health > _maxHealth) {
            _health = _maxHealth;
        }
        UpdateHealthBar();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(_health > 0 && other.TryGetComponent<DamageDealer>(out var damageDealer)) {
            if(_gameSession.PlayerIsAlive) {
                TakeDamage(damageDealer.Damage);
                HandlePlayerHit();
                HandleEnemyDeath();
            }
            _effectSpawner.PlayHitEffect(transform.position);
            _audioPlayer.PlayImpactClip();
            damageDealer.Hit();

            
        }
    }

    void UpdateHealthBar() {
        if(_healthBar == null) { return; }

        _healthBar.localScale = new Vector2(_health / _maxHealth, _healthBar.localScale.y);
        float posX = (1 - _healthBar.localScale.x) / 2;
        _healthBar.localPosition = new Vector2(posX, _healthBar.localPosition.y);
    }

    

    void TakeDamage(int amount) {
        _health -= amount;
        if(_health <= 0) {
            Destroy(gameObject);
            _effectSpawner.PlayDeathEffect(transform.position);
            _audioPlayer.PlayExplosionClip();
        }
        UpdateHealthBar();
    }

    void HandlePlayerHit() {
        if(!_isPlayer) { return; }

        _gameSession.SetHealthUI(_health, _maxHealth);
        _cameraShake.Play();
        _powerManager.AddPower(-1);

        if(_health <= 0) {
            _gameSession.PlayerIsAlive = false;
            _gameSession.SaveGame();
            _levelManager.LoadGameOver();
        }
    }

    void HandleEnemyDeath() {
        if(!_isPlayer && _health <= 0) {
            _gameSession.AddScore(50);
        }
    }
}
