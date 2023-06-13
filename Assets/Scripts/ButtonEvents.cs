using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField] private Movement _player;
    [SerializeField] private Weapon _weapon;
    private IEnumerator _ienumeratorShoot;
    private IEnumerator _ienumeratorRightSlant;
    private IEnumerator _ienumeratorLeftSlant;
    private bool _isFireButtonDown = false;
    private bool _isLeftCLinButtonDown = false;
    private bool _isRightCLinButtonDown = false;

    private void Start()
    {
        _ienumeratorShoot = _weapon.MobileShooting();
    }

    //Стрельба------------------------
    public void OnFireButtonDown()
    {
        _isFireButtonDown = true;
        StartCoroutine(ShootAnimationMobile());
        StartCoroutine(_ienumeratorShoot);
    }

    public void OnFireButtonUp()
    {
        _isFireButtonDown = false;
        StopCoroutine(_ienumeratorShoot);
    }
    //--------------------------------

    //Перезарядка---------------------
    public void OnReloadButtonDown()
    {
        _weapon.StartReloadingMobile();
    }
    //--------------------------------

    //Наклон влево--------------------
    public void OnClinLeftButtonDown()
    {
        _isLeftCLinButtonDown = true;
        StartCoroutine(LeftClinAnimationMobile());
    }

    public void OnClinLeftButtonUp()
    {
        _isLeftCLinButtonDown = false;
    }
    //--------------------------------

    //Наклон вправо-------------------
    public void OnClinRightButtonDown()
    {
        _isRightCLinButtonDown = true;
        StartCoroutine(RightClinAnimationMobile());
    }

    public void OnClinRightButtonUp()
    {
        _isRightCLinButtonDown = false;
    }
    //--------------------------------

    //Удар с ноги---------------------
    public void OnKickButtonDown()
    {
        _player.Kick();
    }
    //--------------------------------

    private IEnumerator ShootAnimationMobile()
    {
        while (_isFireButtonDown)
        {
            _player.MobileShootAnimation(1);
            yield return new WaitForFixedUpdate();
        }
        _player.MobileShootAnimation(2);
    }

    private IEnumerator LeftClinAnimationMobile()
    {
        while (_isLeftCLinButtonDown)
        {
            _player.LeftClinAnimationMobile();
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator RightClinAnimationMobile()
    {
        while (_isRightCLinButtonDown)
        {
            _player.RightClinAnimationMobile();
            yield return new WaitForFixedUpdate();
        }
    }
}