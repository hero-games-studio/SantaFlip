using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    private Rigidbody _rb;
    private readonly  Vector3 _distance = new Vector3(0,1.3f,0);
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.Instance.transform.position - _distance ;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Combo")
        {
            GameManager.Instance.combo += 1;
            GameManager.Instance.gift += 1;
        }
    }
}
