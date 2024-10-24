using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveConfig", fileName = "New WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> _enemyPrefabs;
    [SerializeField] Transform _pathPrefab;
    [SerializeField] float _moveSpeed = 5f;

    [SerializeField] float _spawnTime = 1f;
    [SerializeField] float _spawnVariance = 0.3f;
    [SerializeField] float _minimumSpawnTime = 0.2f;



    public float MoveSpeed => _moveSpeed;

    public int EnemyCount => _enemyPrefabs.Count;

    public Transform StartingPoint => _pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform point in _pathPrefab) {
            waypoints.Add(point);
        }

        return waypoints;
    }

    public GameObject GetEnemyAt(int index) => _enemyPrefabs[index];

    public float GetRandomSpawnTime() {
        float min = _spawnTime - _spawnVariance;
        float max = _spawnTime + _spawnVariance;

        float randomSpawnTime = Random.Range(min, max);

        return Mathf.Clamp(randomSpawnTime, _minimumSpawnTime, max);
    }

}
