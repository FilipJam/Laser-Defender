using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject _powerUp;
    
    [Header("Spawning")]
    [SerializeField] float _spawnRate = 5f;
    [SerializeField] float _spawnVariance = 1f;
    [SerializeField] float _spawnMinimum = 1f;

    SpriteRenderer _spriteRenderer;

    float _spawnRange;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {  
        _spawnRange = _spriteRenderer.bounds.extents.x;
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps() {
        // spawns power ups continuously
        while(true) {
            yield return new WaitForSeconds(GetRandomSpawnTime());
            // spawn at random time in random position
            Instantiate(_powerUp, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    float GetRandomSpawnTime() {
        float min = _spawnRate - _spawnVariance;
        float max = _spawnRate + _spawnVariance;

        float randomSpawnTime = Random.Range(min, max);
        return Mathf.Clamp(randomSpawnTime, _spawnMinimum, max);
    }

    Vector2 GetRandomSpawnPosition() {
        float randomPosX = Random.Range(transform.position.x -_spawnRange, transform.position.x + _spawnRange);
        return new Vector2(randomPosX, transform.position.y);
    }
}
