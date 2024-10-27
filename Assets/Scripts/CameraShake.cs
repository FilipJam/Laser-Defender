using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float _shakeDuration = 1f;
    [SerializeField] float _shakeMagnitude = 0.5f;

    [SerializeField] float _shakeInterval = 0.1f;

    Vector3 _initialPosition;


    void Start()
    {
        _initialPosition = transform.position;
    }

    public void Play() {
        StartCoroutine(Shake());
    }

    IEnumerator Shake() {
        float elapsedTime = 0f;

        while(elapsedTime < _shakeDuration) {
            transform.position = _initialPosition + (Vector3)Random.insideUnitCircle * _shakeMagnitude;
            elapsedTime += _shakeInterval;
            yield return new WaitForSeconds(_shakeInterval);
        }

        transform.position = _initialPosition;
    }

    
}
