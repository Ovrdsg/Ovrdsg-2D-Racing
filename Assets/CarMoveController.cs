using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveController : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _jumpForce = 15.0f;
    private float _horizontalInput;
    private Rigidbody2D _rigidbody;

    private bool _facingRight = true;

    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    private int extraJumps;
    private int extraJumpsValue = 3;
    
    void Start()
    {
        extraJumps = extraJumpsValue;
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
        _horizontalInput = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);

        TurnCar();
    }

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if(_isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
            extraJumps--;
        }

        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && _isGrounded == true)
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
        }

        
    }
    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void TurnCar()
    {
        if (_facingRight == false && _horizontalInput > 0)
        {
            Flip();
        }

        else if (_facingRight == true && _horizontalInput < 0)
        {
            Flip();
        }
    }
}
