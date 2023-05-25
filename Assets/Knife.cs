using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private int _damage;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.Find("Player");
    }

    private void Damage()
    {
        _player.GetComponent<Player>().TakeDamage(_damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Damage();
        }
    }
}
