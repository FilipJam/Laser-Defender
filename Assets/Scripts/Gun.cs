using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] BulletMovement _bullet;
    [SerializeField] bool _isTracking = false;

    public void Shoot() {
        Instantiate(_bullet, transform.position, _bullet.transform.rotation).SetTracking(_isTracking);
    }
    // Quaternion CalculateBulletRotation() => _isTracking ? FacingPlayerRotation() : _bullet.transform.rotation;
    // Quaternion FacingPlayerRotation() {
    //     GameObject player = GameObject.Find("Player");

    //     if(player == null) {
    //         Debug.Log("Player NOT found!");
    //         return _bullet.transform.rotation;
    //     }

    //     Vector3 direction = (transform.position - player.transform.position).normalized;
    //     Quaternion targetRotation = Quaternion.LookRotation(direction);

        
    //     return targetRotation;
    //     //return new Quaternion(0,0, targetRotation.z, targetRotation.w);
    // }
}
