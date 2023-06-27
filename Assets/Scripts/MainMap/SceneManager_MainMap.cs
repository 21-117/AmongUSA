using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManager_MainMap : MonoBehaviour
{
    public static SceneManager_MainMap instance;

    // ��Ʈ��, 2�� ���� ������ ����
    public GameObject introUI, imposterEnding, crewEnding;
    private Animator introAmimator;
    private float introAnimationTime = 7.5f;
    // ��Ʈ�δ� ���۰� ���ÿ� �����Ƿ� ������ bool ������ �б� ó��
    private bool _ImposterEnding;
    public bool _CrewEnding;


    // NPC �� ī��Ʈ, ���� ���� NPC üũ
    private int npcCount, npcDead = 0;
    
    // �̼� ����� UI, �ؽ�Ʈ ��ȯ �ϱ�
    public Slider taskSlider;
    public TextMeshProUGUI killCountText;
    
    // ų ���� �Ŵ����� ���
    public AudioSource killSound;

    // ũ�� ������ npc, �÷��̾� ����
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

    // �ʱ�ȭ
    void Init()
    {
        introAmimator = introUI.transform.GetComponent<Animator>();
        StartCoroutine(IntroPlayOnAwake());
    }
    
    // �� ���� ���� ó��
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
    
    // ���� �ʵ� �� �����ϴ� NPC ���� ���� ��ȯ �� UI ǥ��
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

    // �ʱ� ���� �� ��ġ �� NPC ���� ī��Ʈ ++ �ޱ�
    public int NPC_COUNT
    {
        get { return npcCount; }
        set 
        {
            npcCount = value;
            taskSlider.maxValue = npcCount;
           
        }
    }

    // ��Ʈ��
    IEnumerator IntroPlayOnAwake()
    {
        introUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(introAnimationTime);
        introAmimator.enabled = false;
        introUI.gameObject.SetActive(false);
    }
    // �������� �¸� ����
    IEnumerator ImposterEndingPlay()
    {
        imposterEnding.gameObject.SetActive(true);
        yield return null;
    }
    // ũ��� �¸� ����
    IEnumerator CrewEndingPlay()
    {
        player.gameObject.SetActive(false);
        npcs.gameObject.SetActive(false);
        crewEnding.gameObject.SetActive(true);
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene(0);
    }


}
