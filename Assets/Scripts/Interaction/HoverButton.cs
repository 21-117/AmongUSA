using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 어몽어스 UI 기능 : 마우스 호버 시 사운드 재생 및 색상 변경해주기
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    AudioSource audioSource;
    public AudioClip[] audioClips = new AudioClip[2];
    void Start()
    {
        button = this.transform.GetComponent<Button>();
        audioSource = this.transform.GetComponent<AudioSource>();
        button.onClick.AddListener(_OnClickedButton);
    }

    // 클릭
    public void _OnClickedButton()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    // 호버 시 초록색
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    
    // 호버 아닐 시 흰색
    public void OnPointerExit(PointerEventData eventData)
    {
     
    }
}
