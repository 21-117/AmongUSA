using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detector : MonoBehaviour
{
    public static Player_Detector instance;

    public bool _NpcEntered = false;
    public Transform npcs;
    public List<Transform> npcsTransforms = new List<Transform>();
    private List<float> distances = new List<float>();
    private float distance;
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
        //if (npcsTransforms.Count > 0)
        //{
        //    for (int i = 0; i < npcsTransforms.Count - 1; i++)
        //    {
        //        distances[i] = Vector3.Distance(this.transform.position, npcsTransforms[i].transform.position);
                
        //    }
        //    for (int j = 0; j < distances.Count; j++)
        //    {

        //    }
        //}

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
            // ����Ʈ�� npc Ʈ������ �߰�
            npcsTransforms.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>())
        {
            _NpcEntered = false;
            //npcs = PlayerMove.instance.transform;
            //���� ��ŭ ī��Ʈ�� �þ��� ���̹Ƿ� ���� ����ŭ ����
            if (npcsTransforms.Count != 0)
            {
                npcsTransforms.RemoveAt(npcsTransforms.Count - 1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>() && other.GetComponent<NPCMove>().isDead)
        {
            for (int i = 0; i < npcsTransforms.Count; i++)
            {
                if (other.transform.name == npcs.name) npcsTransforms.Remove(other.transform);//npcsTransforms.Remove(other.transform);
            }
            
        }

    }

}
