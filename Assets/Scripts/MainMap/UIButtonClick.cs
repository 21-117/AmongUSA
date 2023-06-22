using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerMove.instance._isButtonPressed = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerMove.instance._isButtonPressed = false;
    }

    public void Button_Kill()
    {
        if (Player_Detector.instance._NpcEntered)
        {
            if (!Player_Detector.instance.npcs.GetComponent<NPCMove>().isDead)
            {
                PlayerMove.instance._isKill = true;
                PlayerMove.instance.transform.position = Player_Detector.instance.npcs.position;
                Player_Detector.instance.npcs.GetComponent<NPCMove>().isDead = true;
            }
        }
    }

}
