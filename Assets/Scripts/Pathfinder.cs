using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner _enemySpawner;
    WaveConfigSO _wave;
    
    List<Transform> _waypoints;
    int _pathIndex = 0;

    private void Awake() {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start() {
        _wave = _enemySpawner.CurrentWave;
        _waypoints = _wave.GetWaypoints();
        // move enemy to starting position of wave
        transform.position = _waypoints[_pathIndex++].position;
    }

    private void Update() {
        if(_pathIndex == _waypoints.Count) {
            // destroy when reached the end of wave's path
            Destroy(gameObject);
            return;
        }

        // position of current waypoint in wave
        Vector3 targetPosition = _waypoints[_pathIndex].position;
        // amount to move per frame
        float deltaStep = _wave.MoveSpeed * Time.deltaTime;
        // moves towards target position over multiple frames
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, deltaStep);

        // waits until enemy reaches target before going to next waypoint
        if(transform.position == targetPosition) {
            _pathIndex++;
        }
    }
}
