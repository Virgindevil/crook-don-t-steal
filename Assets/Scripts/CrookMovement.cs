using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrookMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _targetPoint;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_targetPoint)
        {
            _transform.position = Vector3.MoveTowards
                (_transform.position,
                _targetPoint.position,
                _speed * Time.deltaTime);
        }
    }    
}
