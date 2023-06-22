using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_MainMap : MonoBehaviour
{
    public GameObject introUI;
    private Animator introAmimator;
    private float introAnimationTime = 7.5f;
    void Start()
    {
        introAmimator = introUI.transform.GetComponent<Animator>();
        StartCoroutine(IntroPlayOnAwake());
    }

    IEnumerator IntroPlayOnAwake()
    {
        introUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(introAnimationTime);
        introAmimator.enabled = false;
        introUI.gameObject.SetActive(false);
    }
}
