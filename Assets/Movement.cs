using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private Animator animator;
    public GameObject FirePref;
    private bool flag;
    //public Camera MainCamera;
    public float gravity;
    public float jumpForce;
    public float speed;
    private float _jumpSpeed;
    private float _jumpVertical;
    private float _jumpHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        //Rotation();
        LookOnCursor();
    }
    void Move()
    {
        float horizontal = 0;
        float vertical = 0;
        if (_controller.isGrounded)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpSpeed = jumpForce;
                _jumpHorizontal = horizontal;
                _jumpVertical = vertical;
            }
            
            // Animation
            Vector3 _animationMove = new Vector3(horizontal, 0f, vertical);
            float velZ = Vector3.Dot(_animationMove.normalized, transform.forward);
            float velX = Vector3.Dot(_animationMove.normalized, transform.right);

            animator.SetFloat("VelocityZ", velZ, 0.1f, Time.deltaTime);
            animator.SetFloat("VelocityX", velX, 0.1f, Time.deltaTime);

        }
        else
        {
            Jump();
        }
        _jumpSpeed += gravity * Time.deltaTime;
        Vector3 direction = new Vector3(horizontal * speed * Time.deltaTime, _jumpSpeed * Time.deltaTime, vertical * speed * Time.deltaTime);
        _controller.Move(direction);
    }
    void Jump()
    {
        _jumpSpeed += gravity * Time.deltaTime;
        Vector3 jdirection = new Vector3(_jumpHorizontal * speed * Time.deltaTime, _jumpSpeed * Time.deltaTime, _jumpVertical * speed * Time.deltaTime);
        _controller.Move(jdirection);
    }
    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Ù˚‚Ù‚");
            animator.SetFloat("Shoot", 1f, 0.05f, Time.deltaTime);
            FirePref.SetActive(true);
        }
        else //if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("neÙ˚‚Ù‚");
            FirePref.SetActive(false);
            animator.SetFloat("Shoot", 0, 0.05f, Time.deltaTime);
        }
    }
    void LookOnCursor()
    {   
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0;
        if (playerPlane.Raycast (ray, out hitdist))
        { 			
            Vector3 targetPoint = ray.GetPoint(hitdist);  			
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);  			
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f*Time.deltaTime);
        } 	
    }
    }
