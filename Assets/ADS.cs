using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    void Start()
    {
        YandexSDK.YaSDK.instance.ShowInterstitial();
    }
}