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

}
