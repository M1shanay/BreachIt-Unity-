using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LevelButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Color32 Highlighted;
    public GameObject Panel;
    public GameObject TextPanel;
    private Color32 Normal;
    private bool Active = true;

    void Start()
    {
        GetComponentInChildren<TMP_Text>().enabled = false;
        TextPanel.SetActive(false);
        Normal = GetComponentInChildren<TMP_Text>().color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Active)
        {
            GetComponentInChildren<TMP_Text>().enabled = true;
            Panel.SetActive(false);
            GetComponentInChildren<TMP_Text>().color = Highlighted;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Active)
        {
            GetComponentInChildren<TMP_Text>().enabled = false;
            Panel.SetActive(true);
            GetComponentInChildren<TMP_Text>().color = Normal;
        }
    }
    public void Deactivate()
    {
        TextPanel.SetActive(true);
        Active = false;
        Panel.SetActive(false);
        GetComponentInChildren<TMP_Text>().enabled = false;
    }
    public void Activate()
    {
        TextPanel.SetActive(false);
        Active = true;
        Panel.SetActive(true);
    }
    public IEnumerator ActiveButton()
    {
        yield return new WaitForSeconds(0.5f);
        Activate();
        Debug.Log("פûגפûג");
    }
}
