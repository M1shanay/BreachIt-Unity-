using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierKnife : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _spottedDistance;

    private NavMeshAgent _agent;
    private Ray _visionRay;
    private RaycastHit _raycastHit;
    private bool _isEnemySpotted = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(EnemyVision());
    }

    private void EnemySpotted()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _target.transform.position - transform.position, Time.deltaTime * 2f, 00f));
    }

    private IEnumerator EnemyVision()
    {
        while (true)
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
            }

            yield return null;
        }
    }
}
