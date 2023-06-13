using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void ExitToMenu()
    {
        YandexSDK.YaSDK.instance.ShowInterstitial();
        SceneManager.LoadScene("Menu");
    }
    public void Retry()
    {
        YandexSDK.YaSDK.instance.ShowInterstitial();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
