using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_DoorScript : MonoBehaviour
{
    [SerializeField] private AudioSource _doorhit;
    private GameObject Player;
    private Animator pAnimation;
    public Transform KickPoint;
    [Space]
    public bool isOpened = false;
    [Range(0f, 4f)]
    [Tooltip("Speed for door opening, degrees per sec")]
    public float OpenSpeed = 3f;

    private bool Kicked;


    float distance;
    float angleView;
    Vector3 direction;

 
    [HideInInspector]
    public Rigidbody rbDoor;
    HingeJoint hinge;
    JointLimits hingeLim;
    float MaxLim=120f;
    float MinLim=-120f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Kicked = Player.GetComponent<Movement>().Kicked;
        pAnimation = Player.GetComponent<Animator>();
        rbDoor = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        //MaxLim = hinge.limits.max;
        //MinLim = hinge.limits.min;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && NearView() && !Kicked)
        {
            pAnimation.SetFloat("Kick", 1);
            StartCoroutine(DoorKick());
            //Kicked = true;
        }
        else if (Kicked)
        {
            pAnimation.SetFloat("Kick", -1);
            //Kicked = false;
        }
    }

    bool NearView()
    {
        distance = Vector3.Distance(KickPoint.position, Player.transform.position);
        //direction = KickPoint.position - Player.transform.position;
        //angleView = Vector3.Angle(Player.transform.forward, direction);
        direction = KickPoint.position - Player.transform.position;
        angleView = Vector3.Angle(direction, Player.transform.forward);
        Debug.Log(angleView);
        if (distance < 2.5f && angleView < 30f) return true;
        else return false;
    }

    private void FixedUpdate()
    {
        //if (isOpened)
        //{
        //    currentLim = 120f;
        //}
        //else
        //{
        //    // currentLim = hinge.angle;
        //    if (currentLim > 1f)
        //        currentLim -= .5f * OpenSpeed;
        //}
        //
        //
        hingeLim.max = MaxLim;
        hingeLim.min = MinLim;
        hinge.limits = hingeLim;
    }
    private IEnumerator DoorKick()
    {
        Kicked = true;
        pAnimation.SetFloat("VelocityZ", 0);
        pAnimation.SetFloat("VelocityX", 0);
        Player.GetComponent<Movement>().Kicked = true;
        yield return new WaitForSeconds(0.65f);
        _doorhit.pitch = Random.Range(0.8f, 1.1f);
        _doorhit.Play();
        rbDoor.AddForce(Player.transform.forward * 80000f);
        yield return new WaitForSeconds(0.65f);
        Player.GetComponent<Movement>().Kicked = false;
        Kicked = false;
    }
}
