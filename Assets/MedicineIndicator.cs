using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MedicineIndicator : MonoBehaviour
{
    public GameObject Indicator;
    public TMP_Text timer;
    private Color32 _cont = new Color32(255, 255, 255, 0);



    private byte a = 0;
    public void TimerNotify()
    {
        timer.enabled = true;
        timer.text = "You are full";
    }
    public void TimerStart(float time)
    {
        timer.enabled = true;
        timer.text = time + "";
    }
    public void TimerFinish()
    {
        StartFlash();
        timer.text = "Healed";
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
    public void StartFlash()
    {
        Indicator.GetComponent<Animator>().enabled = true;
        Indicator.GetComponent<Animator>().SetBool("Saving", true);
    }
    public IEnumerator DisableSaved()
    {
        yield return new WaitForSeconds(1.2f);
        timer.GetComponent<Animator>().SetBool("Saved", true);
        Indicator.GetComponent<Animator>().SetBool("Saved", true);
    }
}
