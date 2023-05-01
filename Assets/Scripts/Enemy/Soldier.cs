using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _bulletOffset;
    [SerializeField] private float _targetChasingOffset;
    [SerializeField] private float _spottedDistance;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private float _health;

    private NavMeshAgent _agent;
    private Ray _visionRay;
    private Ray _shootRay;
    private RaycastHit _raycastHit;
    private RaycastHit _shootRayHit;
    private Vector3 _lastPlayerSpottedPosition;
    private bool _isEnemySpotted = false;
    private bool _isShooting = false;
    private float _distanceBetweenPlayer;
    private float _stoppingDistance;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stoppingDistance = _agent.stoppingDistance;
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
            _distanceBetweenPlayer = Vector3.Distance(transform.position, _target.transform.position);

            if (Physics.Raycast(_visionRay, out _raycastHit, _layerMask))
            {
                if ((_raycastHit.transform.tag == "Player") && (_raycastHit.distance <= _spottedDistance))
                {
                    _lastPlayerSpottedPosition = _target.transform.position;
                    _agent.stoppingDistance = _stoppingDistance;
                    _isEnemySpotted = true;
                }
                else if ((_isEnemySpotted) && (_raycastHit.transform.tag != "Player"))
                {
                    _agent.stoppingDistance = 0;
                }

                Debug.DrawRay(_visionRay.origin, _visionRay.direction * 1000f, Color.red);

                if (_isEnemySpotted)
                {
                    _shootRay = new Ray(transform.position + _offset, transform.forward);
                    Debug.DrawRay(_shootRay.origin, _shootRay.direction * 1000f, Color.white);

                    EnemySpotted();

                    if(Physics.Raycast(_shootRay, out _shootRayHit))
                    {
                        if ((_shootRayHit.transform.tag == "Player") && (_distanceBetweenPlayer <= _agent.stoppingDistance && !_isShooting))
                        {
                            StartCoroutine(Shoot());
                        }
                        else if (!_isShooting)
                        {
                            _agent.SetDestination(_lastPlayerSpottedPosition + transform.forward * _targetChasingOffset);

                            if (_raycastHit.transform.tag != "Player")
                            {
                                _isEnemySpotted = false;
                            }
                        }
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;

        while (_distanceBetweenPlayer <= _shootingDistance)
        {
            if (Physics.Raycast(_visionRay, out _raycastHit))
            {
                if (_raycastHit.transform.tag != "Player")
                {
                    break;
                }
            }

            Instantiate(_bullet, transform.position + _bulletOffset, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0.15f, 0f)));

            yield return new WaitForSeconds(_fireRate);
        }

        _isShooting = false;
    }

    public void ApllyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}