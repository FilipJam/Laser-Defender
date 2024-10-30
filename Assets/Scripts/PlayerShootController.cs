using System.Collections;
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
        // if OnRelease is missed (e.g. changed focus or cursor away from game screen)
        // and OnPress is triggered twice in a row
        if(button.isPressed && _isShooting) {
            // stops coroutines to prevent multiple coroutines running
            StopAllCoroutines();
        }

        // monitors when shooting is active to prevent duplicate shooting
        _isShooting = button.isPressed;
        if(button.isPressed) {
            // start shooting on press and hold
            _firingCoroutine = StartCoroutine(FireWeaponsContinuously());
        } else {
            // stop shooting OnRelease
            StopCoroutine(_firingCoroutine);
        }
    }
    IEnumerator FireWeaponsContinuously() {
        while(true) {
            // fires all weapons at once
            _shooter.FireWeapons();
            yield return new WaitForSeconds(_gunCooldown);
        }
    }
}
