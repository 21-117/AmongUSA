using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_MainMap : MonoBehaviour
{
    public GameObject introUI;
    private Animator introAmimator;
    void Start()
    {
        introAmimator = introUI.transform.GetComponent<Animator>();
        StartCoroutine(IntroPlayOnAwake());
    }

    IEnumerator IntroPlayOnAwake()
    {
        introUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(7.5f);
        introAmimator.enabled = false;
        introUI.gameObject.SetActive(false);
    }
}
