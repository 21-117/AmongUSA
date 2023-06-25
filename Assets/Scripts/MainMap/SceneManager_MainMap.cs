using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SceneManager_MainMap : MonoBehaviour
{
    public GameObject introUI, imposterEnding;
    private Animator introAmimator;
    private float introAnimationTime = 7.5f;
    private int npcCount, npcDead = 0;
    public Slider taskSlider;
    public TextMeshProUGUI killCountText;
    private bool _ImposterEnding;
    public bool _CrewEnding;
    void Start()
    {
        introAmimator = introUI.transform.GetComponent<Animator>();
        StartCoroutine(IntroPlayOnAwake());
    }

    private void Update()
    {
        if (!_ImposterEnding)
        {
            if (NPC_COUNT == NPC_DEADCOUNT)
            {
                StartCoroutine(ImposterEndingPlay());
                _ImposterEnding = true;
            }
        }

        if (!_CrewEnding)
        {
            //����
            StartCoroutine(CrewEndingPlay());
            _CrewEnding = true;
        }

        
    }

    public int NPC_DEADCOUNT
    {
        get { return npcDead; }
        set 
        {
            npcDead = value;
            killCountText.text = $"��� ũ����� �����϶� ({NPC_DEADCOUNT}/10) ";
            taskSlider.value = npcDead;
        }
    }

    public int NPC_COUNT
    {
        get { return npcCount; }
        set 
        {
            npcCount = value;
            taskSlider.maxValue = npcCount;
           
        }
    }

    IEnumerator IntroPlayOnAwake()
    {
        introUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(introAnimationTime);
        introAmimator.enabled = false;
        introUI.gameObject.SetActive(false);
    }
    IEnumerator ImposterEndingPlay()
    {
        imposterEnding.gameObject.SetActive(true);
        yield return null;
    }
    IEnumerator CrewEndingPlay()
    {
        imposterEnding.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        
    }


}
