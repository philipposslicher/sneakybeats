using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverIncrease : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float hoverIncrease;
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        rt.localScale *= hoverIncrease;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        rt.localScale /= hoverIncrease;
    }
}
