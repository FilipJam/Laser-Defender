using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    [SerializeField] float _shakeDuration = 1f; // duration of shaking
    [SerializeField] float _shakeMagnitude = 0.5f; // strength of shaking
    [SerializeField] float _shakeInterval = 0.1f; // frequency of shaking

    Vector3 _initialPosition;

    void Start()
    {
        // center point for camera shake
        _initialPosition = transform.position;
    }

    public void Play() {
        StartCoroutine(Shake());
    }

    IEnumerator Shake() {
        float elapsedTime = 0f;

        // shake camera for _shakeDuration long
        while(elapsedTime < _shakeDuration) {
            // random position within circle of radius=_shakeMagnitude and center=_initial position
            transform.position = _initialPosition + (Vector3)Random.insideUnitCircle * _shakeMagnitude;
            elapsedTime += _shakeInterval;
            yield return new WaitForSeconds(_shakeInterval);
        }

        // reset position
        transform.position = _initialPosition;
    }
}
