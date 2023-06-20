using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detector : MonoBehaviour
{
    public static Player_Detector instance;

    public bool _NpcEntered = false;
    public Transform npcs;

    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    private void FixedUpdate()
    {
        this.transform.position = PlayerMove.instance.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<NPCMove>() && !other.GetComponentInParent<NPCMove>().isDead)
        {
            _NpcEntered = true;
            npcs = other.transform;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInParent<NPCMove>() )
        {
            _NpcEntered = false;
            npcs = PlayerMove.instance.transform;
        }
    }

}
