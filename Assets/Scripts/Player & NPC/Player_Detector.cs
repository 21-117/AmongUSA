using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detector : MonoBehaviour
{
    public static Player_Detector instance;

    public bool _NpcEntered = false;
    public Transform npcs;
    private List<Transform> npcsDir = new List<Transform>();
    private float distance;
    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    private void FixedUpdate()
    {
        if (npcsDir.Count > 0)
        {
            for (int i = 0; i < npcsDir.Count -1; i++)
            {
                distance = Vector3.Distance(this.transform.position, npcsDir[i].transform.position);
            }
        }

        Debug.Log(distance);
        //foreach (Transform t in npcsDir)
        //{
        //    distance = Vector3.Distance(this.transform.position, t.transform.position);
        //    Debug.Log(distance);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>() && !other.GetComponent<NPCMove>().isDead)
        {
            _NpcEntered = true;
            //npcs = other.transform;
            // 리스트에 npc 트랜스폼 추가
            npcsDir.Add(npcs);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>())
        {
            _NpcEntered = false;
            //npcs = PlayerMove.instance.transform;
            // 들어온 만큼 카운트가 늘었을 것이므로 들어온 수만큼 차감
            npcsDir.RemoveAt(npcsDir.Count-1);
        }
    }

}
