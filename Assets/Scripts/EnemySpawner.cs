using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> _waves;
    [SerializeField] float _timeBetweenWaves = 1f;

    WaveConfigSO _currentWave;

    bool _isLooping = true;

    public WaveConfigSO CurrentWave => _currentWave;

    void Start() {
        _currentWave = _waves[0];
        StartCoroutine(LoopWaves());
    }

    IEnumerator LoopWaves()
    {
        yield return new WaitForSeconds(1f);
        do yield return SpawnWaves(); 
        while(_isLooping);
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
        Instantiate(enemy, _currentWave.StartingPoint.position, enemy.transform.rotation, transform);
    }
}