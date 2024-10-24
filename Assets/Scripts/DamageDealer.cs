using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int _damage = 1;
    public int Damage => _damage;
    public void Hit() => Destroy(gameObject);
}
