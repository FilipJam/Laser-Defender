using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveConfig", fileName = "New WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    // enemies for current wave
    [SerializeField] List<GameObject> _enemyPrefabs;

    // path the enemies follow
    [SerializeField] Transform _pathPrefab;
    [SerializeField] float _moveSpeed = 5f;

    // provides varying spawn time for enemies
    [SerializeField] float _spawnTime = 1f;
    [SerializeField] float _spawnVariance = 0.3f;
    [SerializeField] float _minimumSpawnTime = 0.2f;


    public float MoveSpeed => _moveSpeed;
    public int EnemyCount => _enemyPrefabs.Count;
    public Transform StartingPoint => _pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();
        
        // gets Transform components from children
        foreach(Transform point in _pathPrefab) {
            waypoints.Add(point);
        }
        return waypoints;
    }

    public GameObject GetEnemyAt(int index) => _enemyPrefabs[index];

    public float GetRandomSpawnTime() {
        // calculate minimum and maximum spawn time
        float min = _spawnTime - _spawnVariance;
        float max = _spawnTime + _spawnVariance;

        float randomSpawnTime = Random.Range(min, max);

        // constrains spawn time
        return Mathf.Clamp(randomSpawnTime, _minimumSpawnTime, max);
    }

}
