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
        // scrolls over texture in a loop
        Scroll();
    }

    void Scroll() {
        Vector2 offset = _scrollSpeed * Time.deltaTime;
        _material.mainTextureOffset += offset;
    }
}
