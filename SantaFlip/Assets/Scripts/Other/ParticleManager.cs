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
    public GameObject JumpingEffect;
    public GameObject LandingEffect;
    private void Start()
    {
       JumpingEffect = ObjectPooler.SharedInstance.GetPooledObject(0);
       LandingEffect = ObjectPooler.SharedInstance.GetPooledObject(1);
    }

    public IEnumerator JumpingEffects()
    {
        JumpingEffect.transform.position = Player.Instance.transform.position;
        JumpingEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        JumpingEffect.SetActive(false);
    }
    
    public IEnumerator LandingEffects()
    {
        LandingEffect.transform.position = Player.Instance.transform.position;
        LandingEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        LandingEffect.SetActive(false);
    }
    
}
