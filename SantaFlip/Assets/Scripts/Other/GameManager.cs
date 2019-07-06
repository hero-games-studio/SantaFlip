﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
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

    public List<BuildingScript> BuildingsList = new List<BuildingScript>();
    public List<GiftController> ChimneyList = new List<GiftController>();
    public int combo;
    public int RequiredGiftToEnd;    
    public Transform _nextTarget;
    public Transform chimneyTargetMan;
    public TextMeshPro chimneyTextMan;
    public float gravity;
    public float height;
    public bool gameEnd;
    private int numberOfGiftGiven;
    private bool isChimney;

    
    public enum STATE
    {
        Play,
        Pause,
        Dead
    }
    
    public STATE state = STATE.Pause;
    void FixedUpdate()
    {
        switch (state)
        {
            case STATE.Play:

                break;
            case STATE.Pause:
                
                break;
            case STATE.Dead:
             //   RestartGame();
                break;
        }
        
    }
    private void Start()
    {
        chimneyTextMan = GetComponentInChildren<TextMeshPro>();
    }
    
    private void Update()
    {
      
        BuidingsControl();
        
        ComboControl();
        
        PlayerDead();
    }

    private void BuidingsControl()
    {
        for (int i = 0; i < BuildingsList.Count; i++)
        {
            if (BuildingsList[i].isPlayerOn)
            {
                
                if (!Player.Instance.isDead)
                {
                    _nextTarget = BuildingsList[i+1].target;

                    if (gameEnd)
                    {
                        StartCoroutine(ParticleManager.Instance.GameEndEffects());
                        UIManager.Instance.ShowLevelCompletePanel();
                        
                        if (numberOfGiftGiven > RequiredGiftToEnd)
                        {
                            Debug.Log("GameEnd");
                            RestartGame();
                        }
                        else
                        {
                            Debug.Log("You Cant Win!!");
                            RestartGame();
                        }
                        break;
                    }
                }
            }
        }
    }

    public void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.Instance.HideGameStartPanel();
        }
    }
    private void RestartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("WorkPlace");
        }
    }
    private void PlayerDead()
    {
        if (Player.Instance.isDead)
        {
            StartCoroutine(DeadAnim());
        }
    }
    private void ComboControl()
    {
        for (int i = 0 ; i < ChimneyList.Count; i++)
        {
            if (ChimneyList[i].isGift)
            {
                isChimney = ChimneyList[i].isGift;
                chimneyTargetMan = ChimneyList[i].chimneyTarget;
                chimneyTextMan = ChimneyList[i].chimneytext;
                ChimneyList.Remove(ChimneyList[i]);
                
                if (Player.Instance._isGrounded)
                {
                    if (isChimney)
                    {
                        StartCoroutine(UIManager.Instance.ShowGiftText(chimneyTargetMan));
                        StartCoroutine("GiftGive");
                    } 
                }
            }
        }
    }


    private IEnumerator GiftGive()
    {
        Player.Instance.hasGift = true;
        Player.Instance.speed = 1;
        numberOfGiftGiven += combo;
        combo = 0;
        isChimney = false;
        yield return new WaitForSeconds(1f);
        Player.Instance.hasGift = false;
        Player.Instance.speed = 13;

    }
    public IEnumerator DeadAnim()
    {
        StartCoroutine(ParticleManager.Instance.DeathEffects());
        Player.Instance._rb.constraints = RigidbodyConstraints.None;
        //Player.Instance._rb.AddExplosionForce(10,Vector3.zero,20);
        yield return new WaitForSeconds(1f);
        Player.Instance.gameObject.SetActive(false);
        UIManager.Instance.ShowGameOverPanel();
        RestartGame();
    }
    public int GetFlipComboCount()
    {
        return combo + 1;
    }
    
    public int GetGiftCount()
    {
        return combo;
    }
    
    /*public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        //StartCoroutine(RestartGame());

    }*/
    
    /*
       if (transform.rotation.z > 0.3 || transform.rotation.z < - 0.3)
            {
               isDead = true;
            } 
            else if((transform.rotation.z < 0.3 || transform.rotation.z > - 0.3)
                    && (transform.rotation.z > 0.21 || transform.rotation.z < - 0.21)
                    && GameManager.Instance.combo != 0 )
            {
                GameManager.Instance.combo = 0;
                StartCoroutine(UIManager.Instance.ShowNearMissText());
            } 
            else if ((transform.rotation.z < 0.21 || transform.rotation.z > - 0.21)
                     && (transform.rotation.z > 0.1305 || transform.rotation.z < - 0.1305)
                     && GameManager.Instance.combo != 0)
            {
               StartCoroutine(UIManager.Instance.ShowNormalText());
            }
            else if ((transform.rotation.z < 0.1305 || transform.rotation.z > - 0.1305) 
                     && GameManager.Instance.combo != 0)
            {
                GameManager.Instance.combo = GameManager.Instance.combo + 1;
                StartCoroutine(UIManager.Instance.ShowPerfectText());
            }
     */
}
