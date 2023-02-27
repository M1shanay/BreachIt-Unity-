using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Button : MonoBehaviour, IPointerExitHandler,IPointerEnterHandler
{
    public Color32 Highlighted;
    private Color32 Normal;

    void Start()
    {
        Normal = GetComponentInChildren<TMP_Text>().color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TMP_Text>().color = Highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<TMP_Text>().color = Normal;
    }
}
