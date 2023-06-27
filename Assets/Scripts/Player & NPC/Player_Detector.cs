using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� ���� �ֺ� NPC �����ϰ� ų ����Ʈ ��ư ��/Ȱ��ȭ ó��
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

    // ũ����� ���� �� 0��° ��� ų npc�� ����
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

    // NPC�� ���� �ʾҴٸ� ����Ʈ�� �߰�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>() && !other.GetComponent<NPCMove>().isDead)
        {
            _NpcEntered = true;
            npcsTransforms.Add(other.transform);
        }
    }

    //NPC�� �����ٸ� ����Ʈ���� ����, ���� �׾��ٸ� ����Ʈ ��ư ��Ȱ��ȭ
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

    // �������� ���� ������ ���� ���� NPC ���� �ϱ�

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
