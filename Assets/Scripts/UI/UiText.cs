using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private void Start()
    {
    }
    private void Awake()
    {
        InGameUI.OnEnemyKilled.AddListener(EnemyKilled);
        InGameUI.OnHostageSave.AddListener(HostageSave);
        InGameUI.Bullets.AddListener(BulletShot);
        InGameUI.OnTakeDamage.AddListener(GetPain);
    }

    private void EnemyKilled()
    {
        RemainingKills--;
        KillsText.text = RemainingKills.ToString();
    }
    private void HostageSave()
    {
        Debug.Log("UI");
        RemainingHostages--;
        HostagesText.text = RemainingHostages + "";
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
}
