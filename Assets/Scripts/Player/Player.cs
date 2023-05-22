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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //_loseCanvas.SetActive(true);
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