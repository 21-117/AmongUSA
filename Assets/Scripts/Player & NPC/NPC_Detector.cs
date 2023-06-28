using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC가 시체 발견 시 리폿이 가능하도록 파라미터 넘겨주기
public class NPC_Detector : MonoBehaviour
{
    SceneManager_MainMap sceneM;

    private void Start()
    {
        sceneM = FindAnyObjectByType<SceneManager_MainMap>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<NPCMove>() && other.GetComponent<NPCMove>().isDead && this.transform.parent.name != other.name)
        {
            sceneM._CrewEnding = true;
        }

    }
}
