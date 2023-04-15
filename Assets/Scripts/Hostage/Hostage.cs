using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Hostage : MonoBehaviour
{
    private UiHostageIndicator indicator = new UiHostageIndicator();
    public GameObject Indicator;
    public TMP_Text timer;
    private GameObject Player;
    bool Saved = false;
    float distance;
    float _timeToSave = 10f;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
        Debug.Log("asdqweqwe");
        InGameUI.SendHostageSave();
        indicator.TimerFinish();
        Saved = true;
    }
    private void StartSaveHostage()
    {
        if (SaveDistance() < 7f)
        {
            if (SaveDistance() < 3f && _timeToSave >= 0)
            {
                _timeToSave -= Time.deltaTime;
                indicator.IndicatorChangeToEnd();
                indicator.TimerStart((float)Math.Round(_timeToSave,3));
                Debug.Log(_timeToSave);
            }
            else if (_timeToSave < 0)
            {
                SaveHostage();
            }
            else
            {
                Debug.Log("asd");
                indicator.IndicatorChangeToStart();
                indicator.TimerEnd();
                _timeToSave = 10f;
            }
        }
    }
    private float SaveDistance()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        return distance;
    }
}
