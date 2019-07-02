using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager Instance;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        Instance = this;
    }

    #endregion
    [Header("Revive Panel")]
    public GameObject revivePanel;
    [Header("GameOver Panel")]
    public GameObject gameOverPanel;
    [Header("LevelComplete")]
    public GameObject levelCompletePanel;

    [Header("Perfect Text")]
    public GameObject perfectText;
    
    [Header("Normal Text")]
    public GameObject normalText;
    
    [Header("NearMiss Text ")]
    public GameObject nearMissText;
    
    [Header("NearMiss Text ")]
    public GameObject comboText;
    public TextMeshProUGUI flip;
    

    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
    }

    public void ShowRevivePanel()
    {
        revivePanel.SetActive(true);
        {
            HideRevivePanel();
            ShowGameOverPanel();
        }
    }

    public void HideRevivePanel()
    {
        revivePanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void SkipButton()
    {
        
        HideRevivePanel();
        ShowGameOverPanel();
    }
    
    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReviveButton()
    {
        //TODO:ShowAdd
        HideRevivePanel();
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public IEnumerator ShowComboText()
    {
        comboText.gameObject.SetActive(true);
        flip.text = "Flip " + GameManager.Instance.GetFlipComboCount().ToString();
        iTween.MoveFrom(comboText, iTween.Hash("y", comboText.transform.position.y - 20f,  "time", .5f, "easetype", "easeInOutElastic"));
        yield return new WaitForSeconds(.5f);
        comboText.SetActive(false);
    }
    public IEnumerator ShowGiftText(Transform target)
    {
        GameManager.Instance.chimneyTextMan.gameObject.SetActive(true);
        target.position = GameManager.Instance.chimneyTargetMan.position;
        GameManager.Instance.chimneyTextMan.text = "Gift " + GameManager.Instance.GetGiftCount().ToString();
        iTween.MoveFrom(target.gameObject, iTween.Hash("y", target.gameObject.transform.position.y - 10f,  "time", .5f, "easetype", "easeOutQuart"));
        yield return new WaitForSeconds(1f);
        GameManager.Instance.chimneyTextMan.gameObject.SetActive(false);
    }
    
    public IEnumerator ShowPerfectText()
    {
        perfectText.gameObject.SetActive(true);
        iTween.MoveFrom(perfectText, iTween.Hash("y", perfectText.transform.position.y + 100f,  "time", 1f, "easetype", "easeOutBounce"));
        yield return new WaitForSeconds(1f);
        perfectText.SetActive(false);
    }
    public IEnumerator ShowNormalText()
    {
        normalText.SetActive(true);
        iTween.MoveFrom(normalText, iTween.Hash("y", normalText.transform.position.y + 100f,  "time", 1f, "easetype", "easeOutBounce"));
        yield return new WaitForSeconds(1f);
        normalText.SetActive(false);
    }
    public IEnumerator ShowNearMissText()
    {
        nearMissText.SetActive(true);
        iTween.MoveFrom(nearMissText, iTween.Hash("y", nearMissText.transform.position.y + 100f,  "time", 1f, "easetype", "easeOutBounce"));
        yield return new WaitForSeconds(1f);
        nearMissText.SetActive(false);
    }
    
}