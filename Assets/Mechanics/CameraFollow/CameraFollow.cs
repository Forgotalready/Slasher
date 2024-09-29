using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private float _smoothing;

    [SerializeField] private Vector3 _cameraOffset;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;



    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _startPosition = transform.position;
        _startPosition += _cameraOffset;
    }

    private void FixedUpdate()
    { 
        var targetPosition = _followTarget.transform.position;
        var newPosition = _startPosition + new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * _smoothing);
    }
}
