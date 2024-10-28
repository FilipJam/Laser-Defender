using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int _damage = 1;
    public int Damage => _damage;

    // lasers and enemies are destroyed on impact
    public void Hit() => Destroy(gameObject);
}
