using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private AudioSource _steps;
    [SerializeField] private float _stepsRation;
    [SerializeField] private AudioSource _shoot;
    [SerializeField] private AudioSource _hit;
    [SerializeField] private AudioSource _dead;
    [SerializeField] private float _minStepPitch, _maxStepPitch;
    Vector3 current, velocity;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySteps());
    }



    // Update is called once per frame
    void Update()
    {
    }

    Vector3 GetVelocity()
    {
        velocity = (transform.position - current) / Time.deltaTime;
        current = transform.position;
        return velocity;
    }
    IEnumerator PlaySteps()
    {
        while (true)
        {
            if (GetVelocity() != Vector3.zero)
            {
                _steps.pitch = Random.Range(_minStepPitch, _maxStepPitch);
                _steps.Play();
            }
            yield return new WaitForSeconds(_stepsRation);
        }
    }
    public void PlayShoot()
    {
        //_shoot.volume = Random.Range(0.7f, 1f);
        _shoot.PlayOneShot(_shoot.clip);
    }
    public void PlayHit()
    {
        _hit.pitch = Random.Range(0.7f, 1f);
        _hit.Play();
    }
    public void PlayDead()
    {
        _dead.pitch = Random.Range(0.7f, 1f);
        _dead.Play();
    }
}
