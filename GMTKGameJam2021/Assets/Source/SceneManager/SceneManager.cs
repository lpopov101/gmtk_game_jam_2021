using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;

    public static SceneManager FindSceneManager()
    {
        return FindObjectOfType<SceneManager>();
    }

    public InputManager GetInputManager()
    {
        return _inputManager;
    }
}
