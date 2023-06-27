using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// UI ��ư Ŭ�� �� �Լ� ȣ��
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

    // �÷��̾ NPC ų
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

    // �÷��̾ NPC ����
    public void Button_Report()
    {
        if (Player_Detector.instance._reportValue)
        {
            SceneManager_MainMap.instance._CrewEnding = true;
        }
    }

    // ų ��Ÿ��
    public IEnumerator CoolTime()
    {
        this.transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        this.transform.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(2f);

        this.transform.GetComponent<Button>().interactable = true;
        this.transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    // �� ��ȯ
    public void _EnterMainMap()
    {
        SceneManager.LoadScene(1);
    }
    public void _EnterMainLobby()
    {
        SceneManager.LoadScene(0);
    }

    // ���� ����
    public void _Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
