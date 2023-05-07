using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierKnife : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _spottedDistance;
    [SerializeField] private float _health;
    [SerializeField] private float _fireRate;

    [SerializeField] private EnemyAnimation _animation;

    private bool _dead = false;
    private BoxCollider _colider;

    private NavMeshAgent _agent;
    private Ray _visionRay;
    private RaycastHit _raycastHit;
    private bool _isEnemySpotted = false;

    private void Start()
    {
        _colider = GetComponent<BoxCollider>();
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(EnemyVision());
    }

    private void EnemySpotted()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _target.transform.position - transform.position, Time.deltaTime * 2f, 00f));
    }

    private IEnumerator EnemyVision()
    {
        while (!_dead)
        {
            _visionRay = new Ray(transform.position + _offset, _target.transform.position - transform.position);

            if (Physics.Raycast(_visionRay, out _raycastHit))
            {
                if (_raycastHit.distance <= _spottedDistance)
                {
                    _isEnemySpotted = true;
                    EnemySpotted();
                }
            }

            Debug.DrawRay(_visionRay.origin, _visionRay.direction * 1000f, Color.red);
            if (_isEnemySpotted)
            {
                _agent.SetDestination(_target.transform.position);
                if(Vector3.Distance(transform.position, _target.transform.position) <= 2.2f)
                {
                    StartCoroutine(Shoot());
                }
            }

            yield return null;
        }
    }
    private IEnumerator Shoot()
    {
        _animation.ShootingAnimation();
        while (Vector3.Distance(transform.position, _target.transform.position) <= 2.2f && !_dead)
        {
            if (Physics.Raycast(_visionRay, out _raycastHit))
            {
                if (_raycastHit.transform.tag != "Player")
                {
                    break;
                }
            }
            yield return new WaitForSeconds(_fireRate);
        }

        _animation.StopShootingAnimation();
    }

    public void ApllyDamage(float damage)
    {
        _health -= damage;
        _animation.HitReaction();
        if (_health <= 0)
        {
            _dead = true;
            _animation.AnimateDeath();
            _colider.enabled = false;
            //Destroy(gameObject);
        }
    }
}
