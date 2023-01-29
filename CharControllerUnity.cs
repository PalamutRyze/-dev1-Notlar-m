using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2 : MonoBehaviour
{
    
    public float jumpforce=1500.0f;
    public float speed=1.0f;
    private float moveDirection;
    
    private bool moving;
    private bool jump;
    private bool grounded=true;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();//caching animator
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        _rigidbody2D.velocity = new Vector2(speed*moveDirection, _rigidbody2D.velocity.y);
        if (jump == true)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpforce);
            jump = false;
        }

    }

    private void Update()
    {
        if (grounded == true && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection=-1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed",speed);
            }else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = 1.0f;
                _spriteRenderer.flipX = false;
                anim.SetFloat("speed",speed);
            }
        }else if (grounded == true)
        {
            moveDirection = 0.0f;
            anim.SetFloat("speed",0.0f);
        }

        if (grounded == true && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("Jump");
            anim.SetBool("Grounded",false);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Zemin"))
        {
            anim.SetBool("Grounded",true);
            grounded = true;
        }

        
    }
}