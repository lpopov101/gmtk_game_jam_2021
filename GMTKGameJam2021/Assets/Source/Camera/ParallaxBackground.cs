using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private Transform _backgroundTransform;
    [SerializeField]
    private float _parallaxFactor;

    private Vector2 _backgroundPosOriginal;
    private Vector2 _camPosOriginal;

    private void Start()
    {
        _backgroundPosOriginal = _backgroundTransform.position;
        _camPosOriginal = transform.position;
    }
    
    private void Update()
    {
        _backgroundTransform.position =
            new Vector3(_backgroundPosOriginal.x - ((transform.position.x - _camPosOriginal.x) * _parallaxFactor),
                        _backgroundPosOriginal.y - ((transform.position.y - _camPosOriginal.y) * _parallaxFactor),
                        _backgroundTransform.position.z);
    }
}
