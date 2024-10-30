using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _fallSpeed = 10f;

    Rigidbody2D _body;
    PowerManager _powerManager;

    void Awake() {
        _powerManager = FindObjectOfType<PowerManager>();
        _body = GetComponent<Rigidbody2D>();
    }

    void Start() {
        _body.velocity = Vector2.down * _fallSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        // check if colliding with player
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player") {
            // increases the amount of guns attached to player
            _powerManager.AddPower(1);
            // destroy power up once collected
            Destroy(gameObject);
        }
    }
}
