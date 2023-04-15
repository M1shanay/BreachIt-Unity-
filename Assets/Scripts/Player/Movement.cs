using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private FixedJoystick _movementJoystick;
    [SerializeField] private FixedJoystick _rotationJoystick;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _controller;
    public GameObject FirePref;
    public Transform ShootPoint;
    private int Clin = 0;
    //public Camera MainCamera;
    public float gravity;
    public float jumpForce;
    public float speed;
    private float _jumpSpeed;
    private float _jumpVertical;
    private float _jumpHorizontal;
    public bool Kicked;
    public Texture2D cursorTexture;

    private void Awake()
    {
        FirePref.SetActive(false);
        animator.SetFloat("Shoot", 0, 0.05f, Time.deltaTime);
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        if (!Kicked)
        {
            Move();
        }
        CloseToWallAnimation();
        //ShootAnimation();
        Rotation();
        //LookOnCursor();
    }

    void Move()
    {
        float horizontal = 0;
        float vertical = 0;
        if (_controller.isGrounded)
        {
            horizontal = _movementJoystick.Horizontal;
            vertical = _movementJoystick.Vertical;

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

            if (Input.GetKey(KeyCode.Q))
            {
                animator.SetFloat("Clin", -1f, 0.2f, Time.deltaTime);
                Clin = 1;
            }
            else if (Clin==1)
            {
                //Debug.Log("��� ����");
                animator.SetFloat("Clin", 0, 0.2f, Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                Clin = -1;
                animator.SetFloat("Clin", 1f, 0.2f, Time.deltaTime);
            }
            else if (Clin==-1)
            {
                //Debug.Log("��� ����");
                animator.SetFloat("Clin", 0, 0.2f, Time.deltaTime);
            }
            if (animator.GetFloat("Clin") <= 0.001f && animator.GetFloat("Clin") >= -0.001f && Clin!=0)
            {
                //Debug.Log("���");
                Clin = 0;
            }

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

    public void Kick()
    {
        animator.SetFloat("Kick", 0, 0.2f, Time.deltaTime);
    }

    public void ShootAnimation(int setting)
    {
        if (setting == 1)
        {
            animator.SetFloat("Shoot", 1f, 0.05f, Time.deltaTime);
            FirePref.SetActive(true);
        }
        else if (setting == 2)
        {
            FirePref.SetActive(false);
            animator.SetFloat("Shoot", 0, 0.05f, Time.deltaTime);
        }
    }

    void CloseToWallAnimation()
    {
        Ray ray = new Ray(ShootPoint.position, ShootPoint.forward);
        RaycastHit hit;
        Debug.DrawRay(ShootPoint.position, ShootPoint.forward * 100f,Color.green);
        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, 2f, ~(1<<6)))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Map")) 
            {
                //Debug.Log("�����");
                float distance = Vector3.Distance(hit.point, ShootPoint.position);
                float value = 0.65f/distance;
                if(value > 0.95)
                {
                    value = 1;
                }
                Debug.Log(distance);
                animator.SetFloat("Dist", value, 0.05f, Time.deltaTime);
            }
        }
        else if (animator.GetFloat("Dist") >= 0.01f)
        {
            animator.SetFloat("Dist", 0, 0.05f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Dist", 0);
        }
    }

    /*void LookOnCursor()
    {   
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0;
        if (playerPlane.Raycast (ray, out hitdist))
        { 			
            Vector3 targetPoint = ray.GetPoint(hitdist);  			
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);  			
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f*Time.deltaTime);
        } 	
    }*/

    private void Rotation()
    {
        Vector3 rotation = new Vector3(_rotationJoystick.Horizontal, 0, _rotationJoystick.Vertical);

        if(rotation != null)
        {
            Quaternion rotationQuaternion = Quaternion.LookRotation(rotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationQuaternion, _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}