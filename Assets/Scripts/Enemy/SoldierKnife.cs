using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierKnife : Soldier
{
    [SerializeField] GameObject _weapon;
    private void Start()
    {
        _colider = GetComponent<BoxCollider>();
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(EnemyVision());
    }

    protected override void EnemySpotted()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _target.transform.position - transform.position, Time.deltaTime * 10f, 00f));
    }

    protected override IEnumerator EnemyVision()
    {
        while (!_dead)
        {
            _visionRay = new Ray(transform.position + _offset, _target.transform.position - transform.position);

            if (Physics.Raycast(_visionRay, out _raycastHit))
            {
                if ((_raycastHit.distance <= _spottedDistance) && (_raycastHit.transform.tag == "Player"))
                {
                    _isEnemySpotted = true;
                    EnemySpotted();
                }
            }

            Debug.DrawRay(_visionRay.origin, _visionRay.direction * 1000f, Color.red);
            if (_isEnemySpotted)
            {
                _agent.SetDestination(_target.transform.position);
                if(Vector3.Distance(transform.position, _target.transform.position) <= 2.2f && !_isShooting)
                {
                    StartCoroutine(Shoot());
                }
            }

            yield return null;
        }
    }
    protected override IEnumerator Shoot()
    {
        _isShooting = true;
        _animation.ShootingAnimation();
        while (Vector3.Distance(transform.position, _target.transform.position) <= 2.2f && !_dead )
        {
            if (Physics.Raycast(_visionRay, out _raycastHit))
            {
                if (_raycastHit.transform.tag != "Player")
                {
                    break;
                }
            }
            _audio.PlayShoot();
            yield return new WaitForSeconds(_fireRate);
        }
        _isShooting = false;
        _animation.StopShootingAnimation();
    }

    public override void ApllyDamage(float damage)
    {
        _health -= damage;
        _indicator.TakeDamage();
        _animation.HitReaction();
        _audio.PlayHit();
        if (_health <= 0)
        {
            _agent.isStopped = true;
            InGameUI.SendEnemyKilled();
            _dead = true;
            _indicator.IndicatorDisable();
            _animation.AnimateDeath();
            _audio.PlayDead();
            _colider.enabled = false;
            _weapon.GetComponent<CapsuleCollider>().enabled = false;
            //Destroy(gameObject);
        }
    }
}
