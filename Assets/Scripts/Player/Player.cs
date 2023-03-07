using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Key Binding")]
    [SerializeField] private KeyCode _moveLeftButton = KeyCode.D;
    [SerializeField] private KeyCode _moveRightButton = KeyCode.A;
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    [SerializeField] private Rigidbody2D _myRigidbody;
    [SerializeField] private Vector2 friction = new Vector2(-.1f, 0);
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private float _maxJumpVelocity;


    private void Update()
    {
        HandleJump();
        HandleMove();
    }

    private void HandleMove()
    {
        if (Input.GetKey(_moveLeftButton))
            _myRigidbody.velocity = new Vector2(_speed, _myRigidbody.velocity.y);

        else if (Input.GetKey(_moveRightButton))
            _myRigidbody.velocity = new Vector2(-_speed, _myRigidbody.velocity.y);

        if (_myRigidbody.velocity.x > 0)
            _myRigidbody.velocity += friction;

        else if (_myRigidbody.velocity.x < 0)
            _myRigidbody.velocity -= friction;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_jumpButton) && Mathf.Abs(_myRigidbody.velocity.y) < 0.001f)
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, _forceJump);

        if (Input.GetKeyUp(_jumpButton) && _myRigidbody.velocity.y > _maxJumpVelocity)
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, _maxJumpVelocity);
    }
}
