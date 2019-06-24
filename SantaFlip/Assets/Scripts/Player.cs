using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _speed;
    private bool _isTurn;
    public bool _isGrounded;
    private Rigidbody _rb;
    private  Vector3 _EulerAngleVelocity;
    private Vector3 _desiredPos;
    public GameObject Pos1;
    public GameObject Pos2;
    private void Start()
    {
        _EulerAngleVelocity = new Vector3(0, 0, 460);
        _rb = GetComponent<Rigidbody>();
        _desiredPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (_isTurn)
        {
            Quaternion deltaRotation = Quaternion.Euler(_EulerAngleVelocity * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
        
        PlayerMovement();
    }

    void Update()
    {
        FlipControl();
        
    }

    private void PlayerMovement()
    {
        if (transform.position != _desiredPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _desiredPos, _speed * Time.deltaTime);
        }
    }

    private void FlipControl()
    {
        if (Input.GetMouseButton(0))
         {
              _isTurn = true;
              Jump();
         }

        if (Input.GetMouseButtonUp(0))
         {
              _isTurn = false;
         }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rb.velocity = Vector3.up * 10f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            
            if (transform.rotation.z > 0.3 || transform.rotation.z < - 0.3)
            {
                Debug.Log("Death");
            }
            else
            {
                transform.rotation = new Quaternion(0,0,0,0);
            }
            
            _isGrounded = true;

            _desiredPos = Pos2.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        } 
    }

}
