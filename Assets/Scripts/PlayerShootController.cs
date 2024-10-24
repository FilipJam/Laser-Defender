using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] float _gunCooldown = 1f;

    Coroutine _firingCoroutine;
    Shooter _shooter;

    bool _isShooting = false;

    void Awake() {
        _shooter = GetComponent<Shooter>();
    }

    void OnFire(InputValue button) {
        if(button.isPressed && _isShooting) {
            StopAllCoroutines();
        }

        _isShooting = button.isPressed;
        if(button.isPressed) {
            _firingCoroutine = StartCoroutine(FireWeaponsContinuously());
        } else {
            StopCoroutine(_firingCoroutine);
        }
    }
    IEnumerator FireWeaponsContinuously() {
        while(true) {
            _shooter.FireWeapons();
            yield return new WaitForSeconds(_gunCooldown);
        }
    }
}
