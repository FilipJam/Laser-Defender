using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject _healthBar;
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

    // sets new health for enemies
    public void ChangeHealth(float value) {
        _maxHealth = value;
        _health = value;
    }
    public void Heal(float amount) {
        _health += amount;
        // ensures health is not larger than max health when healing
        if(_health > _maxHealth) {
            _health = _maxHealth;
        }
        // updates health bar to match health value
        UpdateHealthBar();
    }

    void OnTriggerEnter2D(Collider2D other) {
        // check if what is colliding can deal damage
        if(other.TryGetComponent<DamageDealer>(out var damageDealer)) {
            TakeDamage(damageDealer.Damage);
            HandlePlayerHit();
            HandlePlayerDeath();
            HandleEnemyDeath();
            _effectSpawner.PlayHitEffect(transform.position);
            _audioPlayer.PlayImpactClip();
            damageDealer.Hit();
        }
    }

    void UpdateHealthBar() {
        // player has health UI, no health bar attached to gameObject
        if(_healthBar == null) { return; }

        // resizes box to match remaining health
        _healthBar.transform.localScale = new Vector2(_health / _maxHealth, _healthBar.transform.localScale.y);
        // calculates new position to be left aligned
        float posX = (1 - _healthBar.transform.localScale.x) / 2;
        // updates position to new calculated position
        _healthBar.transform.localPosition = new Vector2(posX, _healthBar.transform.localPosition.y);
    }
    void TakeDamage(int amount) {
        _health -= amount;
        // destroy gameObject when dead
        if(_health <= 0) {
            Destroy(gameObject);
            _effectSpawner.PlayDeathEffect(transform.position);
            _audioPlayer.PlayExplosionClip();
        }
        // sync visuals with health value
        UpdateHealthBar();
    }
    void HandlePlayerHit() {
        // only applies to player
        if(_isPlayer) {
            _gameSession.SetHealthUI(_health, _maxHealth);
            _cameraShake.Play();
            _powerManager.AddPower(-1);
        }
    }
    void HandlePlayerDeath() {
        // Game Over
        if(_isPlayer && _health <= 0) {
            // shares knowledge that player is dead
            _gameSession.PlayerIsAlive = false;
            // saves score and time now that the game is over
            _gameSession.SaveGame();
            // proceeds to the game over screen
            _levelManager.LoadDelayedGameOver();
        }
    }
    void HandleEnemyDeath() {
        /*
            score points
            if gameObject is enemy,
            if enemy's health is depleted,
            while player is still alive (avoids scoring points when player is dead)
        */
        if(!_isPlayer && _health <= 0 && _gameSession.PlayerIsAlive) {
            _gameSession.AddScore(50);
        }
    }
}
