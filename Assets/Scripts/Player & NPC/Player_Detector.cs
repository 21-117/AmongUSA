using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Detector : MonoBehaviour
{
    public static Player_Detector instance;

    public bool _NpcEntered = false, _reportValue = false;
    public Transform npcs;
    public List<Transform> npcsTransforms = new List<Transform>();
    private List<float> distances = new List<float>();
    private float distance;
    public Image report_Image;
    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    private void Start()
    {

    }
    private void FixedUpdate()
    {


        if(npcsTransforms.Count == 0)
        {
            npcs = PlayerMove.instance.transform;
        }
        else if(npcsTransforms.Count != 0)
        {
            npcs = npcsTransforms[0].transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>() && !other.GetComponent<NPCMove>().isDead)
        {
            _NpcEntered = true;
            //npcs = other.transform;
            // 리스트에 npc 트랜스폼 추가
            npcsTransforms.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>())
        {
            _NpcEntered = false;
            //npcs = PlayerMove.instance.transform;
            //들어온 만큼 카운트가 늘었을 것이므로 들어온 수만큼 차감
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
