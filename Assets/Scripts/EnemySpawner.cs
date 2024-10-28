using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> _waves;
    [SerializeField] float _timeBetweenWaves = 1f;
    [SerializeField] float _difficultyScaling = 2f;

    WaveConfigSO _currentWave;
    int _difficultyLevel = 0;

    public WaveConfigSO CurrentWave => _currentWave;

    void Start() {
        StartCoroutine(RepeatSpawnWaves());
    }

    IEnumerator RepeatSpawnWaves()
    {
        // 1 second delay when the game starts
        yield return new WaitForSeconds(1f);

        // spawn endless rounds of waves
        do {
            yield return SpawnWaves();
            // after each round of waves, increase difficulty
            _difficultyLevel++;
        }
        while(true);
    }

    IEnumerator SpawnWaves() {
        foreach(var wave in _waves) {
            // assign to _currentWave for enemy Pathfinder to read
            _currentWave = wave;
            // spawn enemies for current wave
            yield return SpawnEnemies();
            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }

    IEnumerator SpawnEnemies() {
        for(int i = 0; i < _currentWave.EnemyCount; i++) {
            SpawnEnemy(i);
            // spawn each enemy at different times
            yield return new WaitForSeconds(_currentWave.GetRandomSpawnTime());
        }
    }

    void SpawnEnemy(int index) {
        GameObject enemy = _currentWave.GetEnemyAt(index);

        // instantiate enemy at starting position of current wave
        Health enemyHealth = Instantiate(enemy, _currentWave.StartingPoint.position, enemy.transform.rotation, transform).GetComponent<Health>();
        // increase health depenging on difficulty level
        enemyHealth.UpgradeHealth(enemyHealth.MaxHP * Mathf.Pow(_difficultyScaling, _difficultyLevel));
    }
}