using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField] private Movement _player;

    //��������------------------------
    public void OnFireButtonDown()
    {
        _player.MobileShootAnimation(1);
    }

    public void OnFireButtonUp()
    {
        _player.MobileShootAnimation(2);
    }
    //--------------------------------

    //�����������---------------------
    public void OnReloadButtonDown()
    {

    }
    //--------------------------------

    //���� � ����---------------------
    public void OnKickButtonDown()
    {
        _player.Kick();
    }
    //--------------------------------
}