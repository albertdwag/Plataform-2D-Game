using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Key Binding")]
    [SerializeField] private KeyCode moveLeftButton = KeyCode.D;
    [SerializeField] private KeyCode moveRightButton = KeyCode.A;
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;

    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Vector2 friction = new Vector2(-.1f, 0);
    [SerializeField] private float speed;
    [SerializeField] private float forceJump;
    [SerializeField] private float maxJumpVelocity;


    private void Update()
    {
        HandleJump();
        HandleMove();
    }

    private void HandleMove()
    {
        if (Input.GetKey(moveLeftButton))
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
        else if (Input.GetKey(moveRightButton))
            myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);

        if (Mathf.Abs(myRigidbody.velocity.x) < 0.1f)
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);

        if (myRigidbody.velocity.x > 0)
            myRigidbody.velocity += friction;
        else if (myRigidbody.velocity.x < 0)
            myRigidbody.velocity -= friction;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpButton) && Mathf.Abs(myRigidbody.velocity.y) < 0.001f)
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, forceJump);

        else if (Input.GetKeyUp(jumpButton) && myRigidbody.velocity.y > maxJumpVelocity)
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, maxJumpVelocity);
    }
}
