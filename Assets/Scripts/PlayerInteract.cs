using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eModeMove
{
    horizontal,
    vertical,
    anywhere,
    jumping
}

public class PlayerInteract : MonoBehaviour
{
    public float speed = 5f;

    public eModeMove EModeMove;

    private Dictionary<string, Collider2D> _playerSides;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        MoveWithCurrentMode(_direction);
    }

    private void MoveWithCurrentMode(Vector2 direction)
    {
        switch (EModeMove)
        {
            case eModeMove.horizontal:
                HorizontalMove(direction);
                break;
            
            case eModeMove.vertical:
                VerticalMove(direction);
                break;
            
            case eModeMove.anywhere:
                AnySideMove(direction);
                break;
        }
    }

    private void HorizontalMove(Vector2 dir)
    {
        _rigidbody2D.MovePosition(new Vector2(
            transform.position.x + dir.x * speed * Time.deltaTime, 
            transform.position.y));
        
    }

    private void VerticalMove(Vector2 dir)
    {
        _rigidbody2D.MovePosition(new Vector2(
            transform.position.x, 
            transform.position.y + dir.y * speed * Time.deltaTime));
    }

    private void AnySideMove(Vector2 dir)
    {
        _rigidbody2D.MovePosition(new Vector2(
                transform.position.x + dir.x * speed * Time.deltaTime, 
                transform.position.y + dir.y * speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (EModeMove == eModeMove.vertical && col.gameObject.CompareTag("Horizontal") || 
            EModeMove == eModeMove.horizontal && col.gameObject.CompareTag("Vertical"))
        {
            EModeMove = eModeMove.anywhere;
            _rigidbody2D.gravityScale = 0;
        }
        else if(col.gameObject.CompareTag("Horizontal"))
        {
            EModeMove = eModeMove.horizontal;
            _rigidbody2D.gravityScale = 0;
        }
        else if (col.gameObject.CompareTag("Vertical"))
        {
            EModeMove = eModeMove.vertical;
            _rigidbody2D.gravityScale = 0;
        }

        print("eModeMove in enter col = " + EModeMove);
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
        print("eModeMove in exit col = " + EModeMove);
    }
}
