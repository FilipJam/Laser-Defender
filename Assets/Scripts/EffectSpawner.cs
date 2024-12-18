using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem _explosionEffect;
    [SerializeField] ParticleSystem _deathEffect;

    public void PlayHitEffect(Vector2 position) => PlayEffect(_explosionEffect, position);
    public void PlayDeathEffect(Vector2 position) => PlayEffect(_deathEffect, position);

    void PlayEffect(ParticleSystem effect, Vector2 position) {
        if(effect != null) {
            // spawn specified particle effect at specified position
            ParticleSystem instance = Instantiate(effect, position, Quaternion.identity);
            // destroy after effect is completed
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
