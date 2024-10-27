using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;

    // margin is an additional constraint to the bounds where the player is allowed to move
    [SerializeField] float _marginLeft = 1f;
    [SerializeField] float _marginRight = 1f;
    [SerializeField] float _marginTop = 1f;
    [SerializeField] float _marginBottom = 1f;

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

    // returns the spaceship sprite out of 2 sprites in the Player GameObject
    SpriteRenderer FindPlayerSprite() {
        List<SpriteRenderer> sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        return sprites.Find(sprite => sprite.CompareTag("Player"));
    }
 
    // assigns the bounds of player sprite
    void InitPadding() {
        SpriteRenderer spriteRenderer = FindPlayerSprite();
        Vector2 bounds = spriteRenderer.sprite.bounds.extents;

        _spriteBounds = bounds;
    }

    // assigns the bounds of camera view
    void InitBounds() {
        _minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        _maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
    }

    // input event which saves the input for reuse
    void OnMove(InputValue value) {
        _moveInput = value.Get<Vector2>();
    }

    // updates player position based on input and constraints
    void Move() {
        // the amount to move for the current frame
        Vector3 delta = _moveSpeed * Time.deltaTime * _moveInput;
        // using that amount, we calculate the new position
        Vector2 newPos = transform.position + delta;
        
        // constrains the new position to not go beyond the bounds the player is allowed to move
        newPos.x = Mathf.Clamp(newPos.x, _minBounds.x + _spriteBounds.x + _marginLeft, _maxBounds.x - _spriteBounds.x - _marginRight);
        newPos.y = Mathf.Clamp(newPos.y, _minBounds.y + _spriteBounds.y + _marginBottom, _maxBounds.y - _spriteBounds.y - _marginTop);

        // update player position to the finalised new position
        transform.position = newPos; 
    }
}
