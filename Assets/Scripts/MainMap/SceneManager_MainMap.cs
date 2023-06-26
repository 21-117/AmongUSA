using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SceneManager_MainMap : MonoBehaviour
{
    public static SceneManager_MainMap instance;

    public GameObject introUI, imposterEnding, crewEnding;
    private Animator introAmimator;
    private float introAnimationTime = 7.5f;
    private int npcCount, npcDead = 0;
    public Slider taskSlider;
    public TextMeshProUGUI killCountText;
    private bool _ImposterEnding;
    public bool _CrewEnding;

    public AudioSource killSound;

    public GameObject npcs, player;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

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

        if (_CrewEnding)
        {
            StartCoroutine(CrewEndingPlay());
            _CrewEnding = false;
        }

    }


    public int NPC_DEADCOUNT
    {
        get { return npcDead; }
        set 
        {
            npcDead = value;
            killCountText.text = $"葛电 农风盔阑 级戈窍扼 ({NPC_DEADCOUNT}/10) ";
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
        player.gameObject.SetActive(false);
        npcs.gameObject.SetActive(false);
        crewEnding.gameObject.SetActive(true);
        yield return new WaitForSeconds(12f);

        SceneManager.LoadScene(0);
        
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;
//#else
//        Application.Quit();
//#endif
        
    }


}
