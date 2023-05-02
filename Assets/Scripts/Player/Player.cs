using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private int _health;
    [SerializeField] private Animator animator;

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
        animator.Play("HitReaction1");
        InGameUI.SendDamage();
        if (_health <= 0)
        {
            animator.Play("Death1");
            //_loseCanvas.SetActive(true);
        }
    }
}