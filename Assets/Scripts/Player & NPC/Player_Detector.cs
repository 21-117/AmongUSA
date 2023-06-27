using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 기준 주변 NPC 감지하고 킬 리포트 버튼 비/활성화 처리
public class Player_Detector : MonoBehaviour
{
    public static Player_Detector instance;

    public bool _NpcEntered = false, _reportValue = false;
    public Transform npcs;
    public List<Transform> npcsTransforms = new List<Transform>();
    public Image report_Image;
    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    // 크루원이 있을 시 0번째 대상 킬 npc로 지정
    private void FixedUpdate()
    {
        if (npcsTransforms.Count == 0)
        {
            npcs = PlayerMove.instance.transform;
        }
        else if (npcsTransforms.Count != 0)
        {
            npcs = npcsTransforms[0].transform;
        }
    }

    // NPC가 죽지 않았다면 리스트에 추가
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>() && !other.GetComponent<NPCMove>().isDead)
        {
            _NpcEntered = true;
            npcsTransforms.Add(other.transform);
        }
    }

    //NPC가 나갔다면 리스트에서 삭제, 만약 죽었다면 리포트 버튼 비활성화
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>())
        {
            _NpcEntered = false;
            if (npcsTransforms.Count != 0)
            {
                npcsTransforms.Remove(other.transform);
            }
        }

        if (other.GetComponent<NPCMove>() && other.GetComponent<NPCMove>().isDead)
        {
            report_Image.color = new Color(1f, 1f, 1f, 0.2f);
            _reportValue = false;
        }

    }

    // 임포스터 자작 리폿을 위해 죽은 NPC 감지 하기

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>() && other.GetComponent<NPCMove>().isDead)
        {
            npcsTransforms.Remove(other.transform);
            report_Image.color = new Color(1f, 1f, 1f, 1f);
            _reportValue = true;
        }

    }

}
