using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _startSpeed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.TransformDirection(Vector3.forward * _startSpeed);
    }

    private void Damage()
    {
        _player.TakeDamage(_damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage();
            Destroy(gameObject);
        }
    }
}