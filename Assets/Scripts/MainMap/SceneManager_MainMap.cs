using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManager_MainMap : MonoBehaviour
{
    public static SceneManager_MainMap instance;

    // 인트로, 2개 엔딩 시퀀스 설정
    public GameObject introUI, imposterEnding, crewEnding;
    private Animator introAmimator;
    private float introAnimationTime = 7.5f;
    // 인트로는 시작과 동시에 나오므로 엔딩만 bool 값으로 분기 처리
    private bool _ImposterEnding;
    public bool _CrewEnding;


    // NPC 총 카운트, 현재 죽은 NPC 체크
    private int npcCount, npcDead = 0;
    
    // 미션 진행률 UI, 텍스트 변환 하기
    public Slider taskSlider;
    public TextMeshProUGUI killCountText;
    
    // 킬 사운드 매니저에 담기
    public AudioSource killSound;

    // 크루 엔딩시 npc, 플레이어 끄기
    public GameObject npcs, player;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    void Start()
    {
        Init();
    }

    // 초기화
    void Init()
    {
        introAmimator = introUI.transform.GetComponent<Animator>();
        StartCoroutine(IntroPlayOnAwake());
    }
    
    // 두 개의 엔딩 처리
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
    
    // 현재 필드 상 존재하는 NPC 죽은 갯수 반환 및 UI 표시
    public int NPC_DEADCOUNT
    {
        get { return npcDead; }
        set 
        {
            npcDead = value;
            killCountText.text = $"모든 크루원을 섬멸하라 ({NPC_DEADCOUNT}/10) ";
            taskSlider.value = npcDead;
        }
    }

    // 초기 시작 시 배치 된 NPC 마다 카운트 ++ 받기
    public int NPC_COUNT
    {
        get { return npcCount; }
        set 
        {
            npcCount = value;
            taskSlider.maxValue = npcCount;
           
        }
    }

    // 인트로
    IEnumerator IntroPlayOnAwake()
    {
        introUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(introAnimationTime);
        introAmimator.enabled = false;
        introUI.gameObject.SetActive(false);
    }
    // 임포스터 승리 엔딩
    IEnumerator ImposterEndingPlay()
    {
        imposterEnding.gameObject.SetActive(true);
        yield return null;
    }
    // 크루원 승리 엔딩
    IEnumerator CrewEndingPlay()
    {
        player.gameObject.SetActive(false);
        npcs.gameObject.SetActive(false);
        crewEnding.gameObject.SetActive(true);
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene(0);
    }


}
