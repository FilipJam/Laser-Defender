using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 10f;
    [SerializeField] float _lifespan = 2f;

    [SerializeField] float _turnRate = 5f;

    bool _isTracking = false;

    float _rotationSpeed = 0;

    float _timeCounter = 0f;

    Rigidbody2D _body;


    void Awake() {
        _body = GetComponent<Rigidbody2D>();
    }

    void Start() {
        if(_isTracking){
            CalculateRotationSpeed();
        }
    }

    
    void Update()
    {
        Turn();
        Move();
        CountTime();
        Die();
    }

    public void SetTracking(bool state) {
        _isTracking = state;
    }

    void CalculateRotationSpeed() {
        GameObject player = GameObject.Find("Player");
        if(player == null) {
            _rotationSpeed = 0;
        } else {
            float xDiff = player.transform.position.x - transform.position.x;
            float yDiff = transform.position.y - player.transform.position.y;
            _rotationSpeed = xDiff / yDiff * _turnRate;
        }
    }
    

    void Turn() {
        if(_rotationSpeed != 0) {
            transform.Rotate(0,0, _rotationSpeed * Time.deltaTime);
            float angle_z = Mathf.Clamp(transform.rotation.eulerAngles.z, 90, 270);
            transform.rotation = Quaternion.Euler(0,0, angle_z);
        }
        
    }

    void Move() {
        _body.velocity =  _bulletSpeed * transform.up;
    }

    void CountTime() {
        _timeCounter += Time.deltaTime;
    }

    void Die() {
        if(_timeCounter >= _lifespan) {
            Destroy(gameObject);
        }
    }
}
