using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    [SerializeField] protected GameObject _target;
    [SerializeField] private GameObject _bullet;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected Vector3 _offset;
    [SerializeField] protected Vector3 _bulletOffset;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] protected float _targetChasingOffset;
    [SerializeField] protected float _spottedDistance;
    [SerializeField] protected float _shootingDistance;
    [SerializeField] protected float _health;
    [SerializeField] protected EnemyAnimation _animation;
    [SerializeField] protected EnemyAudio _audio;

    [SerializeField] protected EnemyIndicator _indicator;
    protected NavMeshAgent _agent;
    protected Ray _visionRay;
    protected Ray _shootRay;
    protected RaycastHit _raycastHit;
    protected RaycastHit _shootRayHit;
    protected Vector3 _lastPlayerSpottedPosition;
    public bool _isEnemySpotted = false;
    public bool _isChasing = false;
    protected bool _isShooting = false;
    protected float _distanceBetweenPlayer;
    protected float _stoppingDistance;
    protected bool _dead = false;
    protected BoxCollider _colider;


    private void Start()
    {
        _colider = GetComponent<BoxCollider>();
        _agent = GetComponent<NavMeshAgent>();
        _stoppingDistance = _agent.stoppingDistance;
        StartCoroutine(EnemyVision());
    }

    public float CurrentHP
    {
        get { return _health; }
    }
    protected virtual void EnemySpotted()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _target.transform.position - transform.position, Time.deltaTime * 4f, 00f));
    }

    protected virtual IEnumerator EnemyVision()
    {
        while (!_dead)
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
                            _isChasing = false;
                            StartCoroutine(Shoot());
                        }
                        else if (!_isShooting)
                        {
                            _agent.SetDestination(_lastPlayerSpottedPosition + transform.forward * _targetChasingOffset);
                            _isChasing = true;
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

    protected virtual IEnumerator Shoot()
    {
        _isShooting = true;
        _animation.ShootingAnimation();

        while (_distanceBetweenPlayer <= _shootingDistance && !_dead)
        {
            if (Physics.Raycast(_visionRay, out _raycastHit))
            {
                if (_raycastHit.transform.tag != "Player")
                {
                    break;
                }
            }
            //Instantiate(_bullet, _shotPoint.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0.15f, 0f)));
            Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
            _audio.PlayShoot();
            StartCoroutine(_animation.Flicker());
            yield return new WaitForSeconds(_fireRate);
        }

        _isShooting = false;
        _animation.StopShootingAnimation();
    }

    public virtual void ApllyDamage(float damage)
    {
        _health -= damage;
        _indicator.TakeDamage();
        _animation.HitReaction();
        if (_health <= 0)
        {
            InGameUI.SendEnemyKilled();
            _indicator.IndicatorDisable();
            _dead = true;
            _animation.AnimateDeath();
            _agent.isStopped = true;
            _colider.enabled = false;
            //Destroy(gameObject);
        }
    }
}