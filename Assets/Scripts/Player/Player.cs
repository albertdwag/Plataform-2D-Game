using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Key Binding")]
    [SerializeField] private KeyCode moveLeftButton = KeyCode.D;
    [SerializeField] private KeyCode moveRightButton = KeyCode.A;
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode springButton = KeyCode.LeftControl;

    [Header("Movement Settings")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Vector2 friction = new Vector2(-.1f, 0);
    [SerializeField] private float speed = 10;
    [SerializeField] private float runningSpeed = 15;
    [SerializeField] private float forceJump = 18;
    [SerializeField] private float minJumpHeight = 5;

    [Header("Animation Settings")]
    [SerializeField] private float jumpScaleY = 1.5f;
    [SerializeField] private float jumpScaleX = 0.7f;
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private Ease ease = Ease.OutBack;


    private void Update()
    {
        HandleJump();
        HandleMove();
    }

    private void HandleMove()
    {
        if (Input.GetKey(moveLeftButton))
            myRigidbody.velocity = new Vector2(Input.GetKey(springButton) ? runningSpeed : speed, myRigidbody.velocity.y);
        else if (Input.GetKey(moveRightButton))
            myRigidbody.velocity = new Vector2(Input.GetKey(springButton) ? -runningSpeed : -speed, myRigidbody.velocity.y);

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
        {
            AnimationJump();
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, forceJump);
        }

        else if (Input.GetKeyUp(jumpButton) && myRigidbody.velocity.y > minJumpHeight)
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, minJumpHeight);
    }

    private void AnimationJump()
    {
        myRigidbody.transform.DOScaleY(jumpScaleY, animationDuration)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(ease);
        myRigidbody.transform.DOScaleX(jumpScaleX, animationDuration)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(ease);
    }
}
