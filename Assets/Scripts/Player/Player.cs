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
    [SerializeField] private HealthBase _healthBase;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Vector2 friction = new Vector2(-.1f, 0);
    [SerializeField] private float speed = 10;
    [SerializeField] private float runningSpeed = 15;
    [SerializeField] private float forceJump = 18;
    [SerializeField] private float minJumpHeight = 5;

    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerDeath = "Death";
    [SerializeField] private string runBool = "Running";
    [SerializeField] private SOAnimationPlayer animationPlayer;
    //[SerializeField] private float jumpScaleX = 0.7f;w
    [SerializeField] private Ease ease = Ease.OutBack;

    private void Awake()
    {
        if (_healthBase != null)
            _healthBase.OnKill += OnPlayerKill;
    }

    private void Update()
    {
        HandleJump();
        HandleMove();
    }

    private void OnPlayerKill()
    {
        _healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(triggerDeath);
    }

    private void HandleMove()
    {
        if (Input.GetKey(moveLeftButton))
        {
            myRigidbody.velocity = new Vector2(Input.GetKey(springButton) ? runningSpeed : speed, myRigidbody.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(moveRightButton))
        {
            myRigidbody.velocity = new Vector2(Input.GetKey(springButton) ? -runningSpeed : -speed, myRigidbody.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Mathf.Abs(myRigidbody.velocity.x) < 0.1f)
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);

        if (myRigidbody.velocity.x > 0)
            myRigidbody.velocity += friction;
        else if (myRigidbody.velocity.x < 0)
            myRigidbody.velocity -= friction;

        AnimationMovement();
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
        myRigidbody.transform.DOScaleY(animationPlayer.JumpScaleY, animationPlayer.animationDuration)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(ease);
        //myRigidbody.transform.DOScaleX(jumpScaleX, animationDuration)
        //    .SetLoops(2, LoopType.Yoyo)
        //    .SetEase(ease);
    }

    private void AnimationMovement()
    {
        if (myRigidbody.velocity.magnitude > 0)
            animator.SetBool(runBool, true);
        else
            animator.SetBool(runBool, false);
    }
}
