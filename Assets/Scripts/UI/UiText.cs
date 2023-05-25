using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiText : MonoBehaviour
{
    public int RemainingKills;
    public int RemainingHostages;
    public TMP_Text KillsText;
    public TMP_Text HostagesText;
    public TMP_Text Bullets;
    
    public GameObject PainHUD;
    private Color32 _painColor;
    private byte _pain = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    private void Awake()
    {
        InGameUI.OnEnemyKilled.AddListener(EnemyKilled);
        InGameUI.OnHostageSave.AddListener(HostageSave);
        InGameUI.Bullets.AddListener(BulletShot);
        InGameUI.OnTakeDamage.AddListener(GetPain);
        InGameUI.OnTakeMedicine.AddListener(GetHeal);
    }

    private void CheckLevelOver()
    {
        if(RemainingHostages == 0 && RemainingKills == 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
    private void EnemyKilled()
    {
        RemainingKills--;
        KillsText.text = RemainingKills.ToString();
        CheckLevelOver();
    }
    private void HostageSave()
    {
        RemainingHostages--;
        HostagesText.text = RemainingHostages + "";
        CheckLevelOver();
    }
    private void BulletShot(int bullets)
    {
        Bullets.text = bullets + "/30";
    }
    private void GetPain()
    {
        _pain += 63;
        _painColor = new Color32(255,255,255,_pain);
        PainHUD.GetComponent<SpriteRenderer>().color = _painColor;
    }
    private void GetHeal()
    {
        _pain = 0;
        _painColor = new Color32(255, 255, 255, _pain);
        PainHUD.GetComponent<SpriteRenderer>().color = _painColor;
    }
}
