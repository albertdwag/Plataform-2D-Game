using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private HealthBase _healthBase;
    [SerializeField] private SOPlayerSetup _playerSetup;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private ParticleSystem _jumpVFX;
    [SerializeField] private AudioSource _jumpSound;

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
        _animator.SetTrigger(_playerSetup.triggerDeath);
    }

    private void HandleMove()
    {
        if (Input.GetKey(_playerSetup.moveLeftButton))
        {
            myRigidbody.velocity = new Vector2(Input.GetKey(_playerSetup.springButton) ? _playerSetup.runningSpeed : _playerSetup.speed, myRigidbody.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(_playerSetup.moveRightButton))
        {
            myRigidbody.velocity = new Vector2(Input.GetKey(_playerSetup.springButton) ? -_playerSetup.runningSpeed : -_playerSetup.speed, myRigidbody.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Mathf.Abs(myRigidbody.velocity.x) < 0.1f)
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);

        if (myRigidbody.velocity.x > 0)
            myRigidbody.velocity += _playerSetup.friction;
        else if (myRigidbody.velocity.x < 0)
            myRigidbody.velocity -= _playerSetup.friction;

        AnimationMovement();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_playerSetup.jumpButton) && Mathf.Abs(myRigidbody.velocity.y) < 0.001f)
        {
            _jumpSound.Play();
            AnimationJump();
            PlayJumpVFX();  
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, _playerSetup.forceJump);
        }

        else if (Input.GetKeyUp(_playerSetup.jumpButton) && myRigidbody.velocity.y > _playerSetup.minJumpHeight)
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, _playerSetup.minJumpHeight);
    }

    private void AnimationJump()
    {
        myRigidbody.transform.DOScaleY(_playerSetup.JumpScaleY, _playerSetup.animationDuration)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(_playerSetup.ease);
        //myRigidbody.transform.DOScaleX(jumpScaleX, animationDuration)
        //    .SetLoops(2, LoopType.Yoyo)
        //    .SetEase(ease);
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
        //if (jumpVFX != null) jumpVFX.Play();
    }

    private void AnimationMovement()
    {
        if (myRigidbody.velocity.magnitude > 0)
            _animator.SetBool(_playerSetup.runBool, true);
        else
            _animator.SetBool(_playerSetup.runBool, false);
    }
} 
