using System;
using UnityEngine;

public class GameplayAudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip _shootingClip;
    [SerializeField] [Range(0f, 1f)] float _shootingVolume = 1f;

    [Header("Impact")]
    [SerializeField] AudioClip _impactClip;
    [SerializeField] [Range(0f, 1f)] float _impactVolume = 1f;

    [Header("Explosion")]
    [SerializeField] AudioClip _explosionClip;
    [SerializeField] [Range(0f, 1f)] float _explosionVolume = 1f;

    public void PlayShootingClip() {
        AudioSource.PlayClipAtPoint(_shootingClip, Camera.main.transform.position, _shootingVolume);
    }

    public void PlayImpactClip() {
        AudioSource.PlayClipAtPoint(_impactClip, Camera.main.transform.position, _impactVolume);
    }

    public void PlayExplosionClip() {
        AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, _explosionVolume);
    }
}
