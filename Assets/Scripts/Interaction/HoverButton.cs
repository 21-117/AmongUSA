using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ���� UI ��� : ���콺 ȣ�� �� ���� �������ֱ�
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    void Start()
    {
        button = this.transform.GetComponent<Button>();
    }

    // ȣ�� �� �ʷϻ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.highlightedColor = new Color(0f, 1f, 0f, 1f);
        button.colors = colorBlock;
    }
    
    // ȣ�� �ƴ� �� ���
    public void OnPointerExit(PointerEventData eventData)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.highlightedColor = new Color(1f, 1f, 1f, 1f);
        button.colors = colorBlock;
    }
}
