using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUI : MonoBehaviour
{
    public static UnityEvent OnEnemyKilled = new UnityEvent();
    public static UnityEvent OnHostageSave = new UnityEvent();
    public static UnityEvent<int> Bullets = new UnityEvent<int>();
    public static UnityEvent OnTakeDamage = new UnityEvent();
    public static void SendHostageSave()
    {
        OnHostageSave.Invoke();
    }
    public static void SendEnemyKilled()
    {
        OnEnemyKilled.Invoke();
    }
    public static void SendBullets(int bullets)
    {
        Bullets.Invoke(bullets);
    }
    public static void SendDamage()
    {
        OnTakeDamage.Invoke();
    }

}
