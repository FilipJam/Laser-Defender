using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] BulletMovement _bullet;

    public void Shoot() {
        // spawn bullet at gun's position
        Instantiate(_bullet, transform.position, _bullet.transform.rotation);
    }
}
