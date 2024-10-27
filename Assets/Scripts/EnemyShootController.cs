using System.Collections;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    [SerializeField] float _shootCooldown = 1f;

    [SerializeField] float _shootVariance = 0.5f;
    [SerializeField] float _minimumShootCooldown = 0.2f;

    [SerializeField] bool _canShoot = true;
    Shooter _shooter;

    void Awake() => _shooter = GetComponent<Shooter>();

    void Start() => StartCoroutine(Shoot());


    IEnumerator Shoot() {
        while(_canShoot) {
            _shooter.FireWeapons();
            yield return new WaitForSeconds(GetRandomShootCooldown());
            
        }
    }

    float GetRandomShootCooldown() {
        float min = _shootCooldown - _shootVariance;
        float max = _shootCooldown + _shootVariance;

        float randomCooldown = Random.Range(min, max);
        return Mathf.Clamp(randomCooldown, _minimumShootCooldown, max);
    }
}
