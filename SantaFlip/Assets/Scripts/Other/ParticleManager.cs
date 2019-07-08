using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    #region Singleton

    public static ParticleManager Instance;

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
    private GameObject JumpingEffect;
    private GameObject LandingEffect;
    private GameObject DeathEffect;
    private GameObject GameEndEffect;
    private GameObject GiftEffect;
    private GameObject TrailEffect;
    private GameObject Gift;
    private Vector3 giftDifference = new Vector3(0f, 10f, 0f);
    private Vector3 dif = new Vector3(1.5f, 0f, 2f);
    private Vector3 trail = new Vector3(5f, 0, 0f);
    private void Start()
    {
       JumpingEffect = ObjectPooler.SharedInstance.GetPooledObject(0);
       LandingEffect = ObjectPooler.SharedInstance.GetPooledObject(1);
       DeathEffect = ObjectPooler.SharedInstance.GetPooledObject(2);
       GameEndEffect = ObjectPooler.SharedInstance.GetPooledObject(3);
       GiftEffect = ObjectPooler.SharedInstance.GetPooledObject(4);
       TrailEffect = ObjectPooler.SharedInstance.GetPooledObject(5);
       Gift = ObjectPooler.SharedInstance.GetPooledObject(6);
    }

    private void Update()
    {
        if (Player.Instance._isGrounded)
        {
            TrailEffect.SetActive(true);
            TrailEffect.transform.position = Player.Instance.transform.position - trail ;
        }
        else
        {
            TrailEffect.SetActive(false);
        }
    }

    public IEnumerator GiftEffects()
    {
        if (GameManager.Instance.combo != 0)
        {
            GiftEffect.transform.position = GameManager.Instance.chimneyTargetMan.position + giftDifference;
            GiftEffect.SetActive(true);
            Gift.transform.position = Player.Instance.transform.position + dif;
            Gift.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        Gift.SetActive(false);
        yield return new WaitForSeconds(2f);
        GiftEffect.SetActive(false);
    }
    public IEnumerator JumpingEffects()
    {
        JumpingEffect.transform.position = Player.Instance.transform.position;
        JumpingEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        JumpingEffect.SetActive(false);
    }
    public IEnumerator GameEndEffects()
    {
        GameEndEffect.transform.position = Player.Instance.transform.position;
        GameEndEffect.SetActive(true);
        yield return new WaitForSeconds(5f);
        GameEndEffect.SetActive(false);
    }
    
    public IEnumerator LandingEffects()
    {
        LandingEffect.transform.position = Player.Instance.transform.position;
        LandingEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        LandingEffect.SetActive(false);
    }
    
    public IEnumerator DeathEffects()
    {
        DeathEffect.transform.position = Player.Instance.transform.position;
        DeathEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeathEffect.SetActive(false);
    }
    
}
