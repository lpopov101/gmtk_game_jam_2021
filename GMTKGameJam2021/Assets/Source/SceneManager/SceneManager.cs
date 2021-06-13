using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = new GameManager(GameObject.FindGameObjectsWithTag("Coin").Length);
    }

    public static SceneManager FindSceneManager()
    {
        return FindObjectOfType<SceneManager>();
    }

    public InputManager GetInputManager()
    {
        return _inputManager;
    }

    public GameManager GetGameManager()
    {
        return _gameManager;
    }
}
