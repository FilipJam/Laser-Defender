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
        //_currentWave = _waves[0];
        StartCoroutine(RepeatSpawnWaves());
    }

    IEnumerator RepeatSpawnWaves()
    {
        yield return new WaitForSeconds(1f);
        do {
            yield return SpawnWaves();
            _difficultyLevel++;
            Debug.Log("Difficulty: " + _difficultyLevel);
        }
        while(true);
    }

    IEnumerator SpawnWaves() {
        foreach(var wave in _waves) {
            _currentWave = wave;
            yield return SpawnEnemies();
            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }

    IEnumerator SpawnEnemies() {
        for(int i = 0; i < _currentWave.EnemyCount; i++) {
            SpawnEnemy(i);        
            yield return new WaitForSeconds(_currentWave.GetRandomSpawnTime());
        }
    }

    void SpawnEnemy(int index) {
        GameObject enemy = _currentWave.GetEnemyAt(index);

        Health enemyHealth = Instantiate(enemy, _currentWave.StartingPoint.position, enemy.transform.rotation, transform).GetComponent<Health>();
        enemyHealth.UpgradeHealth(enemyHealth.MaxHP * Mathf.Pow(_difficultyScaling, _difficultyLevel));
    }
}