using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 10f;
    [SerializeField] float _lifespan = 2f;

    float _timeCounter = 0f;
    Rigidbody2D _body;

    void Awake() {
        _body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
        CountTime();
        Die();
    }

    void Move() {
        // move in direction the bullet is facing
        _body.velocity =  _bulletSpeed * transform.up;
    }
    void CountTime() {
        _timeCounter += Time.deltaTime;
    }
    void Die() {
        // destroys 
        if(_timeCounter >= _lifespan) {
            Destroy(gameObject);
        }
    }
}
