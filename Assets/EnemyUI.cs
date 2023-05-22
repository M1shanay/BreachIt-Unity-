using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyUI : MonoBehaviour
{
    public static UnityEvent OnTakeDamage;
    public static void SendDamage()
    {
        OnTakeDamage.Invoke();
    }

}
