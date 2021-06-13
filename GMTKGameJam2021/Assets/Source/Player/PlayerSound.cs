using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip _walkClip;
    [SerializeField]
    private AudioClip _jumpClip;
    [SerializeField]
    private AudioClip _snapClip;
    [SerializeField]
    private AudioClip _landClip;

    [SerializeField]
    [Range(0,1)]
    private float _walkVolume;
    [SerializeField]
    [Range(0,1)]
    private float _jumpVolume;
    [SerializeField]
    [Range(0,1)]
    private float _snapVolume;
    [SerializeField]
    [Range(0,1)]
    private float _landVolume;

    private AudioSource _walkAudioSource;
    private AudioSource _jumpAudioSource;
    private AudioSource _snapAudioSource;
    private AudioSource _landAudioSource;

    private void Start()
    {
        _walkAudioSource = gameObject.AddComponent<AudioSource>();
        _walkAudioSource.clip = _walkClip;
        _walkAudioSource.volume = _walkVolume;
        _jumpAudioSource = gameObject.AddComponent<AudioSource>();
        _jumpAudioSource.clip = _jumpClip;
        _jumpAudioSource.volume = _jumpVolume;
        _snapAudioSource = gameObject.AddComponent<AudioSource>();
        _snapAudioSource.clip = _snapClip;
        _snapAudioSource.volume = _snapVolume;
        _landAudioSource = gameObject.AddComponent<AudioSource>();
        _landAudioSource.clip = _landClip;
        _landAudioSource.volume = _landVolume;
    }

    public void PlayWalkSound()
    {
        if(!_walkAudioSource.isPlaying)
        {     
            _walkAudioSource.pitch = Random.Range(0.75F, 1.25F);
            _walkAudioSource.Play();
        }

    }

    public void PlayJumpSound()
    {
        _jumpAudioSource.pitch = Random.Range(0.9F, 1.1F);
        _jumpAudioSource.Play();
    }

    public void PlayLandSound()
    {
        _landAudioSource.pitch = Random.Range(0.9F, 1.1F);
        _landAudioSource.Play();
    }

    public void PlaySnapSound()
    {
        _snapAudioSource.Play();
    }
}
