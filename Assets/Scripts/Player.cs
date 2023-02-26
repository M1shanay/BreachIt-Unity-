using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        //Debug.Log(_health);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}