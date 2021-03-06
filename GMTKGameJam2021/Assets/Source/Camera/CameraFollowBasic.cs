using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBasic : MonoBehaviour
{
    [SerializeField]
    private Transform followTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followTransform.position.x,
                                         followTransform.position.y,
                                         transform.position.z);
    }
}
