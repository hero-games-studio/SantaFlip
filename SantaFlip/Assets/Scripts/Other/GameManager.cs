using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

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

    public List<BuildingScript> BuildingsList = new List<BuildingScript>();
    public List<GiftController> ChimneyList = new List<GiftController>();
    public int RequiredGiftToEnd;    
    public Transform _nextTarget;
    public bool gameEnd;
    public int combo;
    public int gift;
    private int numberOfGiftGiven;

    private void Update()
    {
        BuidingsControl();
        ComboControl();
        
    }

    private void BuidingsControl()
    {
        for (int i = 0; i < BuildingsList.Count; i++)
        {
            if (BuildingsList[i].isPlayerOn)
            {
                _nextTarget = BuildingsList[i+1].target;
                
                if (gameEnd)
                {
                    if (numberOfGiftGiven > RequiredGiftToEnd)
                    {
                        Debug.Log("GameEnd");   
                    }
                    else
                    {
                        Debug.Log("You Cant Win!!");
                    }
                }
            }
        }
    }

    private void ComboControl()
    {
        for (int i = 0 ; i < ChimneyList.Count; i++)
        {
            if (ChimneyList[i].isGift)
            {
                //Her chimneyden geçerken kontrol etmeyi sağla.
                gift = combo;
                if (Player.Instance._isGrounded)
                {
                    combo = 0;
                    numberOfGiftGiven += gift;
                }
            }
        }
    }
}
