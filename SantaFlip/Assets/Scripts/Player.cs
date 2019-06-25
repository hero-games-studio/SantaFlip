using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private float rotationPowerTime;
    public float jumpSpeed;
    public bool _isTurn;
    private bool isStop = true;
    public bool _isGrounded;
    private Rigidbody _rb;
    private  Vector3 _EulerAngleVelocity;
    public Vector3 desiredPos;
    private float frictionEffect;
    
    private void Start()
    {
        _EulerAngleVelocity = new Vector3(0, 0, 460);
        _rb = GetComponent<Rigidbody>();
        desiredPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (_isTurn || !_isTurn)
        {
            Quaternion deltaRotation = Quaternion.Euler(-_EulerAngleVelocity * rotationPowerTime  * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }

        if (isStop)
        {
            Quaternion friction = Quaternion.Euler(0,0,150 * rotationPowerTime* frictionEffect * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * friction );
        }
       
        //PlayerMovement();
    }

    void Update()
    {
        Debug.Log(frictionEffect);
      //  Debug.Log(rotationPowerTime);
        FlipControl();

        if (_isTurn)
        {
            rotationPowerTime += Time.deltaTime;
            if (rotationPowerTime >= 1)
            {
                rotationPowerTime = 1;
            }
        }
        else 
        {
            rotationPowerTime -= Time.deltaTime;
            if (rotationPowerTime <= 0) 
            {
                rotationPowerTime = 0;
            }
        }

        if (isStop)
        {
            if (rotationPowerTime > 0.9f)
            {
                frictionEffect = 1.22f;
            }
            else if (rotationPowerTime > 0.7f)
            {
                frictionEffect = 1.3f;
            }
            else if (rotationPowerTime > 0.4f)
            {
                frictionEffect = 1.6f;
            }
            else if (rotationPowerTime > 0.2f)
            {
                frictionEffect = 2f;
            }
        }
        else
        {
            frictionEffect = 1f;
        }
       
    }

  
    private void FlipControl()
    {
        if (Input.GetMouseButtonDown(0))
         {
              _isTurn = true;
              isStop = false;
           //   ResultingValuefrominput += Input.GetAxis("MouseX") * RotationSpeed * RotationFriction;
            
              Jump();
         }
        if (Input.GetMouseButtonUp(0))
         {
              _isTurn = false;
              isStop = true;
              //  transform.rotation = Quaternion.Lerp(quaternionrotatefrom, quaternionrotateto,Time.deltaTime * RotationSmoothness);
         }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rb.velocity = Vector3.up * jumpSpeed;
        }
    }

  /*  private IEnumerator JumpSpeed()
    {
        jumpSpeed = 5f;
        yield return new WaitForSeconds(0.5f);
      
    }*/
   
    private void PlayerMovement()
    {
        if (transform.position != desiredPos)
        {
           // transform.position = Vector3.MoveTowards(transform.position, desiredPos, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            
            if (transform.rotation.z > 0.3 || transform.rotation.z < - 0.3)
            {
               // Debug.Log("Death");
               // Debug.Log("DeathParticleEffect");
                this.gameObject.SetActive(false);
               // Instantiate(part, transform.position, Quaternion.identity); //Object Poolingle yaz
            } 
            else if (transform.rotation.z > 0.13 || transform.rotation.z < - 0.13)
            {
               // Debug.Log("Nearmiss");
                transform.rotation = new Quaternion(0,0,0,0);
            }
            else if (transform.rotation.z < 0.13 || transform.rotation.z > - 0.13)
            {
                //Debug.Log("Perfect");
                transform.rotation = new Quaternion(0,0,0,0);
            }
            else if(transform.rotation.z < 0.3 || transform.rotation.z > - 0.3)
            {
                transform.rotation = new Quaternion(0,0,0,0);
              //  Debug.Log("Normal");
            }
           
            
            _isGrounded = true;

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
