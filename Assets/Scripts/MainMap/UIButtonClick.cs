using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (Player_Detector.instance.npcsTransforms.Count != 0)
        {
            PlayerMove.instance._isKill = true;
            PlayerMove.instance.transform.position = Player_Detector.instance.npcs.position;
            Player_Detector.instance.npcs.GetComponent<NPCMove>().isDead = true;
        }
    }
    public void _EnterMainMap()
    {
        SceneManager.LoadScene(1);
    }
    public void _EnterMainLobby()
    {
        SceneManager.LoadScene(0);
    }

    // 게임 종료
    public void _Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
