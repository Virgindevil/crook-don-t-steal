using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrookMovement : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        _transform.position += new Vector3(Time.fixedDeltaTime, 0,0);
    }

    
}
