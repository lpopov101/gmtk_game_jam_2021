using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum MovementState
    {
        NONE = 0,
        IDLE = 1,
        WALKING = 2,
        JUMPING = 3,
        MIDAIR = 4,
        SNAPPING = 5
    }

    [SerializeField]
    private int _playerIndex = 0;
    
    private InputManager _inputMgr;
    private StateMachine<MovementState> _movementStateMachine;

    private void Start()
    {
        _inputMgr = SceneManager.FindSceneManager().GetInputManager();
        _movementStateMachine = new StateMachine<PlayerManager.MovementState>(
            PlayerManager.MovementState.MIDAIR);
    }
    
    public int GetPlayerIndex()
    {
        return _playerIndex;
    }

    public float GetPlayerHorizontalAxis()
    {
        return _inputMgr.GetPlayerHorizontalAxis(_playerIndex);
    }

    public float GetPlayerVerticalAxis()
    {
        return _inputMgr.GetPlayerVerticalAxis(_playerIndex);
    }

    public bool GetPlayerJump()
    {
        return _inputMgr.GetPlayerJump(_playerIndex);
    }

    public bool GetPlayerFire()
    {
        return _inputMgr.GetPlayerFire(_playerIndex);
    }

    public StateMachine<MovementState> GetMovementStateMachine()
    {
        return _movementStateMachine;
    }
}
