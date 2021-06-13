using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    

    [SerializeField]
    private float _horizontalAcceleration = 1.0F;
    [SerializeField]
    private float _horizontalAccelerationMidair = 1.0F;
    [SerializeField]
    private float _horizontalGroundedDecceleration = 1.0F;
    [SerializeField]
    private float _groundClampForce = 1.0F;
    [SerializeField]
    private float _topHorizontalSpeed = 2.0F;
    [SerializeField]
    private float _jumpStrength = 10.0F;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Transform[] _groundProbeTransforms;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerManager _playerMgr;
    private PlayerSound _playerSound;
    private StateMachine<PlayerManager.MovementState> _stateMachine;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMgr = GetComponent<PlayerManager>();
        _playerSound = GetComponent<PlayerSound>();
        _stateMachine = _playerMgr.GetMovementStateMachine();
        InitStateMachine();
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Run();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedRun();
    }

    private void InitStateMachine()
    {
        _stateMachine.SetStateFixedBehaviorCallback( PlayerManager.MovementState.IDLE,
        () =>
        {
            Deccelerate();
            ClampHorizontalVelocity();
            ApplyGroundClampForce();
        });

        _stateMachine.SetStateFixedBehaviorCallback( PlayerManager.MovementState.WALKING,
        () =>
        {
            ApplyGroundedHorizontalForce();
            ClampHorizontalVelocity();
            ApplyGroundClampForce();
            _playerSound.PlayWalkSound();
        });

        _stateMachine.SetStateFixedBehaviorCallback(PlayerManager.MovementState.JUMPING,
        () =>
        {
            ApplyMidairHorizontalForce();
            ClampHorizontalVelocity();
        });

        _stateMachine.SetStateFixedBehaviorCallback(PlayerManager.MovementState.SNAPPING,
        () =>
        {
            ApplyMidairHorizontalForce();
        });

        _stateMachine.SetStateFixedBehaviorCallback(PlayerManager.MovementState.MIDAIR,
        () =>
        {
            ApplyMidairHorizontalForce();
            ClampHorizontalVelocity();
        });

        _stateMachine.SetStateEntryCallback(PlayerManager.MovementState.JUMPING,
        () =>
        {
            Jump();
            _playerSound.PlayJumpSound();
        });

        _stateMachine.SetStateEntryCallback(PlayerManager.MovementState.SNAPPING,
        () =>
        {
            Snap();
            _playerSound.PlaySnapSound();
        });

        _stateMachine.SetStateTransitionCallback(new[] { PlayerManager.MovementState.WALKING,
                                                         PlayerManager.MovementState.IDLE},
                                                 PlayerManager.MovementState.JUMPING,
        () =>
        {
            return _playerMgr.GetPlayerJump();
        });

        _stateMachine.SetStateTransitionCallback(new[] { PlayerManager.MovementState.WALKING,
                                                         PlayerManager.MovementState.IDLE},
                                                 PlayerManager.MovementState.MIDAIR,
        () =>
        {
            return !CheckGrounded();
        });

        _stateMachine.SetStateTransitionCallback(new[] { PlayerManager.MovementState.JUMPING,
                                                         PlayerManager.MovementState.MIDAIR,
                                                         PlayerManager.MovementState.WALKING,
                                                         PlayerManager.MovementState.IDLE},
                                                 PlayerManager.MovementState.SNAPPING,
        () =>
        {
            float magnitude = PlayerPositionUtils.getSnapMagnitude();
            return _playerMgr.GetPlayerFire() && magnitude > 0;
        });

        _stateMachine.SetStateTransitionCallback(PlayerManager.MovementState.JUMPING,
                                                 PlayerManager.MovementState.MIDAIR,
        () =>
        {
            return _rb.velocity.y <= 0 && !_playerMgr.GetPlayerFire() && !_playerMgr.GetPlayerJump();
        });

        _stateMachine.SetStateTransitionCallback(PlayerManager.MovementState.SNAPPING,
                                                 PlayerManager.MovementState.IDLE,
        () =>
        {
            bool slow = Mathf.Abs(_rb.velocity.x) < 2f;
            if(slow && CheckGrounded() && !_playerMgr.GetPlayerFire())
            {
                _playerSound.PlayLandSound();
                return true;
            }
            return false;
        });

        _stateMachine.SetStateTransitionCallback(PlayerManager.MovementState.MIDAIR,
                                                 PlayerManager.MovementState.IDLE,
        () =>
        {
            if(CheckGrounded())
            {
                if(_rb.velocity.y <= 0)
                {
                    _playerSound.PlayLandSound();
                }
                return true;
            }
            return false;
        });

        _stateMachine.SetStateTransitionCallback(PlayerManager.MovementState.WALKING,
                                                 PlayerManager.MovementState.IDLE,
        () =>
        {
            return _playerMgr.GetPlayerHorizontalAxis() == 0;
        });

        _stateMachine.SetStateTransitionCallback(PlayerManager.MovementState.IDLE,
                                                 PlayerManager.MovementState.WALKING,
        () =>
        {
            return _playerMgr.GetPlayerHorizontalAxis() != 0;
        });
    }

    private void ApplyGroundedHorizontalForce()
    {
        var direction = Vector2.right;
        var hit = Physics2D.Raycast((Vector2)transform.position + (0.5F * _rb.velocity.normalized), -Vector2.up, 100, _groundLayer);
        if(hit.collider != null)
        {
            direction = -Vector2.Perpendicular(hit.normal);
        }
        _rb.AddForce(direction * _horizontalAcceleration * _playerMgr.GetPlayerHorizontalAxis());
    }

    private void ApplyMidairHorizontalForce()
    {
        _rb.AddForce(Vector2.right * _horizontalAccelerationMidair * _playerMgr.GetPlayerHorizontalAxis());
    }

    private void Deccelerate()
    {
        var oppositeVelocityDirection = -_rb.velocity.normalized;
        _rb.AddForce(oppositeVelocityDirection * _horizontalGroundedDecceleration);
    }

    private void ApplyGroundClampForce()
    {
        _rb.AddForce(Vector2.down * _groundClampForce);
    }

    private void ClampHorizontalVelocity()
    {
        if (_playerMgr.GetPlayerFire())
        {
            return;
        }

        var clampedXVelocity = Mathf.Clamp(_rb.velocity.x, -_topHorizontalSpeed,
                                            _topHorizontalSpeed);
        _rb.velocity = new Vector2(clampedXVelocity, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
    }

    private void Snap()
    {
        Vector2 snapVector = PlayerPositionUtils.getSnapVectorForPlayer(_rb);
        float magnitude = PlayerPositionUtils.getSnapMagnitude();
        _rb.AddForce(snapVector * magnitude, ForceMode2D.Impulse);

        if (CheckGrounded() && magnitude > 0) {
            _rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }

    private bool CheckGrounded()
    {
        foreach(var groundProbeTransform in _groundProbeTransforms)
        {
            if(Physics2D.OverlapCircle(groundProbeTransform.position, 0.2F, _groundLayer))
            {
                return true;
            }
        }
        return false;
    }
}
