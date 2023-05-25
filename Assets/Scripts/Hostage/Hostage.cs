using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Hostage : MonoBehaviour
{
    [SerializeField] private UiHostageIndicator indicator;
    public GameObject Indicator;
    public TMP_Text timer;
    [SerializeField] private GameObject Player;
    bool Saved = false;
    float distance;
    float _timeToSave = 5f;
    void Start()
    {
        indicator.timer = timer;
        indicator.Indicator = Indicator;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Saved)
        StartSaveHostage();
    }

    private void SaveHostage()
    {
        Saved = true;
        indicator.TimerFinish();
        InGameUI.SendHostageSave();
        StartCoroutine(indicator.DisableSaved());
    }
    private void StartSaveHostage()
    {
        if (SaveDistance() < 7f && !Saved)
        {
            if (SaveDistance() < 3f && _timeToSave >= 0)
            {
                _timeToSave -= Time.deltaTime;
                indicator.IndicatorChangeToEnd();
                indicator.TimerStart((float)Math.Round(_timeToSave,1));
            }
            else if (_timeToSave < 0)
            {
                SaveHostage();
            }
            else
            {
                indicator.IndicatorChangeToStart();
                indicator.TimerEnd();
                _timeToSave = 5f;
            }
        }
    }
    private float SaveDistance()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        return distance;
    }
}
