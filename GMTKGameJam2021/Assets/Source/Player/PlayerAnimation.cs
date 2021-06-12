using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerManager _playerMgr;
    private StateMachine<PlayerManager.MovementState> _stateMachine;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMgr = GetComponent<PlayerManager>();
        _stateMachine = _playerMgr.GetMovementStateMachine();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _animator.SetInteger("MovementState",
            (int)_playerMgr.GetMovementStateMachine().GetCurrentState());
        HorizontalSpriteFlip();
    }

    private void HorizontalSpriteFlip()
    {
        if (_stateMachine.GetCurrentState() == PlayerManager.MovementState.SNAPPING) {
            _spriteRenderer.flipX = _rb.velocity.x <= 0;
        }
        else if(_playerMgr.GetPlayerHorizontalAxis() < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_playerMgr.GetPlayerHorizontalAxis() > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
