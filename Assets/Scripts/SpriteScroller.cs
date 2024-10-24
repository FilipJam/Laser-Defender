using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 _scrollSpeed;
    Material _material;

    void Awake() {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        Vector2 offset = _scrollSpeed * Time.deltaTime;
        _material.mainTextureOffset += offset;
    }
}
