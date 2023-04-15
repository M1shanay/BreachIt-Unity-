using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UiHostageIndicator : MonoBehaviour
{
    public GameObject Indicator;
    public TMP_Text timer;
    private Color32 _cont = new Color32(255,255,255,0);


    private byte a = 0;
    private void Awake()
    {
        //UiHostage.OnSaveHostageStart.AddListener(IndicatorChangeToEnd);
        //UiHostage.OnSaveHostageEnd.AddListener(IndicatorChangeToStart);
        //UiHostage.OnSaveHostageStartTimer.AddListener(TimerStart);
        //UiHostage.OnSaveHostageEndTimer.AddListener(TimerEnd);
        //UiHostage.OnSaveHostageTimerFinish.AddListener(TimerFinish);
    }
    public void TimerStart(float time)
    {
        timer.enabled = true;
        timer.text = time + "";
    }
    public void TimerFinish()
    {
        timer.text = "Saved";
    }
    public void TimerEnd()
    {
        timer.enabled = false;
    }
    public void IndicatorChangeToEnd()
    {
        if (a < 250)
        {
            a += 2;
        }
        _cont.a = a;
        Indicator.GetComponent<Image>().color = _cont;
    }
    public void IndicatorChangeToStart()
    {
        if (a > 4)
        {
            a -= 2;
        }
        _cont.a = a;
        Indicator.GetComponent<Image>().color = _cont;
    }
}
