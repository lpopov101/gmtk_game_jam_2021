using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowNPlayers : MonoBehaviour
{
    [SerializeField]
    private Transform[] _playerTransforms;
    [SerializeField]
    private float _orthoSizeCoefficient;
    [SerializeField]
    private float _minOrthoSize;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        var averagePlayerX = 0.0F;
        var averagePlayerY = 0.0F;
        var minPlayerX = float.MaxValue;
        var minPlayerY = float.MaxValue;
        var maxPlayerX = float.MinValue;
        var maxPlayerY = float.MinValue;
        var countF = (float)_playerTransforms.Length;
        foreach(var playerTransform in _playerTransforms)
        {
            averagePlayerX += playerTransform.position.x / countF;
            averagePlayerY += playerTransform.position.y / countF;
            minPlayerX = Mathf.Min(minPlayerX, playerTransform.position.x);
            maxPlayerX = Mathf.Max(maxPlayerX, playerTransform.position.x);
            minPlayerY = Mathf.Min(minPlayerY, playerTransform.position.y);
            maxPlayerY = Mathf.Max(maxPlayerY, playerTransform.position.y);
        }
        transform.position = new Vector3(averagePlayerX, averagePlayerY, transform.position.z);
        var xDist = maxPlayerX - minPlayerX;
        var yDist = maxPlayerY - minPlayerY;
        var orthoSize = _orthoSizeCoefficient * Mathf.Sqrt(Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(yDist, 2)));
        _camera.orthographicSize = Mathf.Max(orthoSize, _minOrthoSize);
    }
}
