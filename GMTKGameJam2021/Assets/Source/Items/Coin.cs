using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _collected = false;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = SceneManager.FindSceneManager().GetGameManager();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_collected){
            _gameManager.CollectCoin();
            _animator.SetBool("Collected", true);
            _audioSource.Play();
            Destroy(gameObject, 0.4F);
            _collected=true;
        }
    }
}
