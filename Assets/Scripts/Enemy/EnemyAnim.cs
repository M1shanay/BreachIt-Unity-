using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    public Animator animator;
    public CharacterController controller;
    public Transform target;
    Vector3 previous, current, velocity;
    float waitTime;

    void Start()
    {
        current = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - current) / Time.deltaTime;
        current = transform.position;
        Vector3 _animationMove = new Vector3(velocity.x * 10f, 0f, velocity.z * 10f);
        float velZ = Vector3.Dot(_animationMove.normalized, transform.forward);
        float velX = Vector3.Dot(_animationMove.normalized, transform.right);
        animator.SetFloat("VelocityZ", velZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velX, 0.1f, Time.deltaTime);
    }
}