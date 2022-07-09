using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eModeMove
{
    horizontal,
    vertical,
    anywhere
}

public class PlayerInteract : MonoBehaviour
{
    public float speed = 5f;

    public eModeMove EModeMove;

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
        MoveWithCurrentMode();
    }

    private void MoveWithCurrentMode()
    {
        _targetVector = Vector3.zero;
        switch (EModeMove)
        {
            case eModeMove.horizontal:
                HorizontalMove();
                break;
            
            case eModeMove.vertical:
                VerticalMove();
                break;
            
            case eModeMove.anywhere:
                HorizontalMove();
                VerticalMove();
                break;
        }
        
        _rigidbody2D.velocity = _targetVector * speed;
    }

    private void HorizontalMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _targetVector = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _targetVector = Vector3.left;
        }
    }

    private void VerticalMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _targetVector = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _targetVector = Vector3.down;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (EModeMove == eModeMove.vertical && col.gameObject.CompareTag("Horizontal") || 
            EModeMove == eModeMove.horizontal && col.gameObject.CompareTag("Vertical"))
        {
            EModeMove = eModeMove.anywhere;
        }
        else if(col.gameObject.CompareTag("Horizontal"))
        {
            EModeMove = eModeMove.horizontal;
        }
        else if (col.gameObject.CompareTag("Vertical"))
        {
            EModeMove = eModeMove.vertical;
        }

        print("eModeMove = " + EModeMove);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (EModeMove == eModeMove.anywhere && other.gameObject.CompareTag("Horizontal"))
        {
            EModeMove = eModeMove.vertical;
        }
        else if (EModeMove == eModeMove.anywhere && other.gameObject.CompareTag("Vertical"))
        {
            EModeMove = eModeMove.horizontal;
        }
        
    }
}
