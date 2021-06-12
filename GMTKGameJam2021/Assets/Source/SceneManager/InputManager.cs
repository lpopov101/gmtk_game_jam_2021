using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    [System.Serializable]
    public struct PlayerMovementAxes
    {
        public string verticalAxis;
        public string horizontalAxis;
        public string jumpAxis;
    }

    [SerializeField]
    private PlayerMovementAxes[] _playerMovementAxes;

    public float GetPlayerVerticalAxis(int playerIdx)
    {
        return Input.GetAxisRaw(_playerMovementAxes[playerIdx].verticalAxis);
    }

    public float GetPlayerHorizontalAxis(int playerIdx)
    {
        return Input.GetAxisRaw(_playerMovementAxes[playerIdx].horizontalAxis);
    }

    public bool GetPlayerJump(int playerIdx)
    {
        return Input.GetButtonDown(_playerMovementAxes[playerIdx].jumpAxis);
    }

}
