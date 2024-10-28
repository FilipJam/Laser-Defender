using System.Collections;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    [SerializeField] float _shootCooldown = 1f;
    [SerializeField] float _shootVariance = 0.5f;
    [SerializeField] float _minimumShootCooldown = 0.2f;

    Shooter _shooter;

    void Awake() {
        _shooter = GetComponent<Shooter>();
    }

    void Start() {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        // endless shooting
        while(true) {
            _shooter.FireWeapons();
            // fire lasers at different times
            yield return new WaitForSeconds(GetRandomShootCooldown());
        }
    }

    float GetRandomShootCooldown() {
        // calculate minimum and maximum spawn time
        float min = _shootCooldown - _shootVariance;
        float max = _shootCooldown + _shootVariance;

        float randomCooldown = Random.Range(min, max);
        // constrains spawn time
        return Mathf.Clamp(randomCooldown, _minimumShootCooldown, max);
    }
}
