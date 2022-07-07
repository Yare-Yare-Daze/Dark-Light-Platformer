using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _targetVector;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _targetVector = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _targetVector = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _targetVector = Vector3.left;
        }
        
        _rigidbody2D.velocity = _targetVector * speed;
        
    }
}
