using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float _lifespan = 1f;
    [SerializeField] int _fadeOutFrames = 10;

    SpriteRenderer _spriteRenderer;

    Color _color;
    float _alpha;

    float _fadeOutRate;

    float _fadeOutAmount;



    void Awake() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start() {
        _alpha = _spriteRenderer.color.a;
        _color = _spriteRenderer.color;
        CalculateFadeOut();
        //Invoke(nameof(InitiateDeathSequence), 1f);
        InitiateDeathSequence();
    }

    void CalculateFadeOut() {
        _fadeOutAmount = _spriteRenderer.color.a / _fadeOutFrames;
        _fadeOutRate = _lifespan / _fadeOutFrames;
    }

    IEnumerator FadeOut() {
        while(_spriteRenderer.color.a > 0) {
            _alpha -= _fadeOutAmount;
            _color.a = _alpha;
            _spriteRenderer.color = _color;
            yield return new WaitForSeconds(_fadeOutRate);
        }
    }

    void InitiateDeathSequence() {
        StartCoroutine(FadeOut());
        Destroy(gameObject, _lifespan);
    }
}
