using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Animator animator;

    public static bool _dead = false;
    private void Start()
    {
        _dead = false;
        _health = _maxHealth;
    }
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
        Debug.Log(_health);
        InGameUI.SendDamage();
        if (_health <= 0)
        {
            animator.Play("Death1");
            _dead = true;
            InGameUI.SendPlayerDead();

        }
    }
    public bool IsFullHP()
    {
        if (_health == _maxHealth)
        {
            return true;
        }
        else
            return false;
    }
    public void TakeHeal()
    {
        _health = _maxHealth;
        InGameUI.SendHeal();
    }
}