using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = SceneManager.FindSceneManager().GetGameManager();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _gameManager.CollectCoin();
        _animator.SetBool("Collected", true);
        Destroy(gameObject, 0.4F);
    }
}
