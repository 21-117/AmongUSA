using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 어몽어스 UI 기능 : 마우스 호버 시 색상 변경해주기
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    void Start()
    {
        button = this.transform.GetComponent<Button>();
    }

    // 호버 시 초록색
    public void OnPointerEnter(PointerEventData eventData)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.highlightedColor = new Color(0f, 1f, 0f, 1f);
        button.colors = colorBlock;
    }
    
    // 호버 아닐 시 흰색
    public void OnPointerExit(PointerEventData eventData)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.highlightedColor = new Color(1f, 1f, 1f, 1f);
        button.colors = colorBlock;
    }
}
