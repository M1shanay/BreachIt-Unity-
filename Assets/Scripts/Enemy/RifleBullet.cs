using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _startSpeed;

    private Rigidbody _rigidbody;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.TransformDirection(Vector3.forward * _startSpeed);
    }

    private void Damage()
    {
        _player.GetComponent<Player>().TakeDamage(_damage);
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