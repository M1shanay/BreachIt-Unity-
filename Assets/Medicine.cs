using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Medicine : MonoBehaviour
{
    [SerializeField] GameObject MedicineBox;
    [SerializeField] MedicineIndicator indicator;
    public GameObject Indicator;
    public TMP_Text timer;
    private GameObject Player;
    bool Healed = false;
    float distance;
    float _timeToHeal = 4f;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        indicator.timer = timer;
        indicator.Indicator = Indicator;
        indicator.IndicatorChangeToStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Healed)
            StartHealing();
    }

    private void Heal()
    {
        Healed = true;
        indicator.TimerFinish();
        InGameUI.SendHeal();
        Player.GetComponent<Player>().TakeHeal();
        StartCoroutine(indicator.DisableSaved());
        MedicineBox.SetActive(false);
    }
    private void StartHealing()
    {
        if (HealDistance() < 4f && !Healed)
        {
            if (HealDistance() < 2f && Player.GetComponent<Player>().IsFullHP())
            {
                indicator.TimerNotify();
                indicator.IndicatorChangeToEnd();
            }
            else
            {
                if (HealDistance() < 2f && _timeToHeal >= 0)
                {
                    _timeToHeal -= Time.deltaTime;
                    indicator.IndicatorChangeToEnd();
                    indicator.TimerStart((float)Math.Round(_timeToHeal, 1));
                }
                else if (_timeToHeal < 0)
                {
                    Heal();
                }
                else
                {
                    indicator.IndicatorChangeToStart();
                    indicator.TimerEnd();
                    _timeToHeal = 4f;
                }
            }
        }
    }
    private float HealDistance()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        return distance;
    }
}
