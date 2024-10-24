using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner _enemySpawner;
    WaveConfigSO _wave;
    
    List<Transform> _waypoints;
    int _waveIndex = 0;

    private void Awake() {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start() {
        _wave = _enemySpawner.CurrentWave;
        _waypoints = _wave.GetWaypoints();
        transform.position = _waypoints[_waveIndex++].position;
    }

    private void Update() {
        if(_waveIndex == _waypoints.Count) {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = _waypoints[_waveIndex].position;
        float deltaStep = _wave.MoveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, deltaStep);
        //Debug.Log("Moving towards");

        if(transform.position == targetPosition) {
            _waveIndex++;
        }
    }
}
