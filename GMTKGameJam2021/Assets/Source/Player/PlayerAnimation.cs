using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerManager _playerMgr;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMgr = GetComponent<PlayerManager>();
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
        if(_playerMgr.GetPlayerHorizontalAxis() < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_playerMgr.GetPlayerHorizontalAxis() > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
