using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource _steps;
    [SerializeField] private AudioSource _shoot;
    [SerializeField] private AudioSource _reload;

    private CharacterController _playerVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _playerVelocity = _player.GetComponent<CharacterController>();
        StartCoroutine(PlaySteps());
    }



    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator PlaySteps()
    {
        while (true)
        {
            if (_playerVelocity.velocity != Vector3.zero)
            {
                _steps.pitch = Random.Range(0.9f, 1.1f);
                _steps.Play();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void PlayShoot()
    {
        //_shoot.volume = Random.Range(0.7f, 1f);
        _shoot.PlayOneShot(_shoot.clip);
    }

    public void PlayReload()
    {
        _reload.Play();
    }
}
