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
        PlayClipFromCamera(_shootingClip, _shootingVolume);
    }

    public void PlayImpactClip() {
        PlayClipFromCamera(_impactClip, _impactVolume);
    }

    public void PlayExplosionClip() {
        PlayClipFromCamera(_explosionClip, _explosionVolume);
    }

    // plays clip at main camera's position
    void PlayClipFromCamera(AudioClip clip, float volume) {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
