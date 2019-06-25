using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    private GameObject manager;
    public GameObject Pos1;
    public GameObject Pos2;
    public Player player;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == Pos2.transform.position)
        {
           Debug.Log("FinishedBuilding");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            
           // player.desiredPos = Pos1.transform.position;

            if (player._isTurn)
            {
              //  player.desiredPos = Pos2.transform.position;
            }
        }
    }
}
