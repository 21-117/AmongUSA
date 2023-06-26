using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float coolTime;
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
            SceneManager_MainMap.instance.killSound.PlayOneShot(SceneManager_MainMap.instance.killSound.clip);
            StartCoroutine(CoolTime());
        }
    }

    public void Button_Report()
    {
        if (Player_Detector.instance._reportValue)
        {
            SceneManager_MainMap.instance._CrewEnding = true;
        }
    }

    public IEnumerator CoolTime()
    {
        this.transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        this.transform.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(2f);

        this.transform.GetComponent<Button>().interactable = true;
        this.transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
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
