using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;

    [SerializeField] float _marginLeft = 1f;
    [SerializeField] float _marginRight = 1f;
    [SerializeField] float _marginTop = 1f;
    [SerializeField] float _marginBottom = 1f;

    float _halfWidth;
    float _halfHeight;


    Vector2 _moveInput;
    Vector2 _spriteBounds;
    Vector2 _minBounds;
    Vector2 _maxBounds;

    void Start() {
        InitPadding();
        InitBounds();
    }

    void Update() {
        Move();
    }

    SpriteRenderer FindPlayerSprite() {
        List<SpriteRenderer> sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        return sprites.Find(sprite => sprite.tag == "Player");
    }

    void InitPadding() {
        SpriteRenderer spriteRenderer = FindPlayerSprite();
        Vector2 bounds = spriteRenderer.sprite.bounds.extents;

        _spriteBounds = bounds;

        //Debug.Log(bounds);
    }

    void InitBounds() {
        _minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        _maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        //Debug.Log("Minimum: " + _minBounds + "\nMaximum: " + _maxBounds);
    }

    void OnMove(InputValue value) {
        _moveInput = value.Get<Vector2>();
    }

    void Move() {
        Vector3 delta = _moveSpeed * Time.deltaTime * _moveInput;

        Vector2 newPos = transform.position + delta;
        
        newPos.x = Mathf.Clamp(newPos.x, _minBounds.x + _spriteBounds.x + _marginLeft, _maxBounds.x - _spriteBounds.x - _marginRight);
        newPos.y = Mathf.Clamp(newPos.y, _minBounds.y + _spriteBounds.y + _marginBottom, _maxBounds.y - _spriteBounds.y - _marginTop);
        transform.position = newPos; 
    }
}
