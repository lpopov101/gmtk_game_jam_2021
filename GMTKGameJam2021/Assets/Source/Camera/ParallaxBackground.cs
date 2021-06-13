using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private Transform _backgroundTransform;
    [SerializeField]
    private float _parallaxFactor;
    [SerializeField]
    private Vector2 _initPos;
    
    private void Update()
    {
        _backgroundTransform.localPosition = new Vector3(-(transform.position.x - _initPos.x) * _parallaxFactor,
                                                         -(transform.position.y - _initPos.y) * _parallaxFactor,
                                                         _backgroundTransform.localPosition.z);
    }
}
