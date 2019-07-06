using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    private readonly  Vector3 _distance = new Vector3(0,1.7f,0);

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.Instance.transform.position - _distance ;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Combo")
        {
             StartCoroutine(UIManager.Instance.ShowComboText());
             GameManager.Instance.combo += 1;
        }
    }
    
    
}
