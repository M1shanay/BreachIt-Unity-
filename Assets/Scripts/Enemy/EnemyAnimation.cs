using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _flickerFX;
    Vector3 current, velocity;

    void Start()
    {
        current = transform.position;
    }

    void Update()
    {
        MovementAnimation();
    }

    private void MovementAnimation()
    {
        velocity = (transform.position - current) / Time.deltaTime;
        current = transform.position;
        Vector3 _animationMove = new Vector3(velocity.x * 10f, 0f, velocity.z * 10f);
        float velZ = Vector3.Dot(_animationMove.normalized, transform.forward);
        float velX = Vector3.Dot(_animationMove.normalized, transform.right);
        _animator.SetFloat("VelocityZ", velZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velX, 0.1f, Time.deltaTime);
    }

    public void HitReaction()
    {
        if(velocity == Vector3.zero)
        {
            _animator.Play("HitReaction2");
        }
        else
        {
            _animator.Play("HitReaction1");
        }
    }
    public void AnimateDeath()
    {
        _animator.SetInteger("Death", Random.Range(1, 3));
    }
    public void ShootingAnimation()
    {
        _flickerFX.SetActive(true);
        _animator.SetFloat("Shoot", 1);
    }
    public void StopShootingAnimation()
    {
        _flickerFX.SetActive(false);
        _animator.SetFloat("Shoot", -1);
    }
}
