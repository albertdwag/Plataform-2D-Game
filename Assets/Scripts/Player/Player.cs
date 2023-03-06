using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _myRigidbody;
    [SerializeField] private KeyCode _moveLeft = KeyCode.D;
    [SerializeField] private KeyCode _moveRight = KeyCode.A;
    [SerializeField] private float _speed;

    private void Update()
    {
        HandleJump();
        HandleMove();
    }

    private void HandleMove()
    {
        if (Input.GetKey(_moveLeft))
            _myRigidbody.velocity = new Vector2(_speed, _myRigidbody.velocity.y);

        if (Input.GetKey(_moveRight))
            _myRigidbody.velocity = new Vector2(-_speed, _myRigidbody.velocity.y);
    }

    private void HandleJump()
    {

    }
}
