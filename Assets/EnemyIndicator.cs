using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyIndicator : MonoBehaviour
{
    [SerializeField] GameObject _healthBar;
    [SerializeField] GameObject _Player;
    Animator _animator;
    float _maxHP;
    float _currentHP;
    bool _inRange = true;
    bool _isActive = true;
    private void Start()
    {
        _animator = _healthBar.GetComponentInChildren<Animator>();
        _maxHP = transform.GetComponent<Soldier>().CurrentHP;
        _healthBar.GetComponent<Slider>().maxValue = _maxHP;
        _healthBar.GetComponent<Slider>().value = _maxHP;
        _currentHP = _maxHP;
    }
    private void Update()
    {
        if (_isActive)
        {
            DistanceOfView();
        }
    }
    public void TakeDamage()
    {
        _currentHP = transform.GetComponent<Soldier>().CurrentHP;
        _healthBar.GetComponent<Slider>().value = _currentHP;
    }
    public void IndicatorDisable()
    {
        _isActive = false;
        _animator.Play("AlphaOff");
    }
    void DistanceOfView()
    {
        if (Vector3.Distance(transform.position, _Player.transform.position) < 10f && (transform.GetComponent<Soldier>()._isEnemySpotted || transform.GetComponent<Soldier>()._isChasing))
        {
            _inRange = true;
            _animator.Play("AlphaOn");

        }
        else if (_inRange == true)
        {
            _animator.Play("AlphaOff");
            _inRange = false;
        }
    }
}
