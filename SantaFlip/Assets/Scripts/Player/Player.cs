using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    #region Singleton

    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    private Rigidbody _rb;
    public float speed;
    private float rotationPowerTime;
    public bool _isTurn;
    private bool isStop = true;
    public bool _isGrounded;
    private readonly  Vector3 _eulerAngleVelocity = new Vector3(0,0,460);
    private float frictionEffect;
    private bool startBuilding;
    
    public float gravity;
    public float height;

    Vector3 CalculateLauncVelocity()
    {
        
        float displacementY = GameManager.Instance._nextTarget.position.y - _rb.position.y;
        Vector3 displacementXz = new Vector3(GameManager.Instance._nextTarget.position.x - _rb.position.x,0,GameManager.Instance._nextTarget.position.z-_rb.position.z);
        
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXz = displacementXz / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

        return velocityXz + velocityY;
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void FixedUpdate()
    {
       DoFlip();
    }

    void Update()
    {
       /* if (transform.position.y < -1)
        {
            Debug.Log("Havada");
        }*/
        
        FlipControl();
        
        SmoothFlip();
    }

    private void DoFlip()
    {
        if (!_isGrounded)
        {
            if (_isTurn || !_isTurn)
            {
                Quaternion deltaRotation = Quaternion.Euler(-_eulerAngleVelocity * rotationPowerTime  * Time.deltaTime);
                _rb.MoveRotation(_rb.rotation * deltaRotation);
            }

            if (isStop)
            {
                Quaternion friction = Quaternion.Euler(0,0,150 * rotationPowerTime* frictionEffect * Time.deltaTime);
                _rb.MoveRotation(_rb.rotation * friction );
            } 
        }
        else if (_isGrounded && !GameManager.Instance.gameEnd)
        {
            PlayerMovement();
        }
     
    }
    private void FlipControl()
    {
        if (Input.GetMouseButtonDown(0))
         {
              _isTurn = true;
              isStop = false;
              startBuilding = true;

              Lauch();
         }
        if (Input.GetMouseButtonUp(0))
         {
              _isTurn = false;
              isStop = true;
         }
    }

    private void SmoothFlip()
    {
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

    void Lauch()
    {
        if (_isGrounded)
        {
            Physics.gravity = Vector3.up * gravity;
            _rb.useGravity = true;
            //Yakınlık Uzaklıga göre Heighti ve Gravitiyi ayarlayabilirsin.
            _rb.velocity = CalculateLauncVelocity();
        }
    }
   
    private void PlayerMovement()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" && startBuilding)
        {
            
            if (transform.rotation.z > 0.3 || transform.rotation.z < - 0.3)
            {
                //Debug.Log("Death");
                gameObject.SetActive(false);
            } 
            else if((transform.rotation.z < 0.3 || transform.rotation.z > - 0.3) && (transform.rotation.z > 0.21 || transform.rotation.z < - 0.21) && GameManager.Instance.combo != 0 )
            {
                //Debug.Log("NearMiss");
            } 
            else if ((transform.rotation.z < 0.21 || transform.rotation.z > - 0.21) && (transform.rotation.z > 0.1305 || transform.rotation.z < - 0.1305) && GameManager.Instance.combo != 0)
            {
               // Debug.Log("Normal");
            }
            else if ((transform.rotation.z < 0.1305 || transform.rotation.z > - 0.1305) && GameManager.Instance.combo != 0)
            {
               // Debug.Log("Perfect");
            }

        }
        
        if (other.gameObject.tag == "Death")
        {
            gameObject.SetActive(false);
        }
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            transform.rotation = new Quaternion(0,0,0,0);
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
