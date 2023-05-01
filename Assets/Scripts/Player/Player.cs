using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private int _health;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(25);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        InGameUI.SendDamage();
        if (_health <= 0)
        {
            //_loseCanvas.SetActive(true);
        }
    }
}