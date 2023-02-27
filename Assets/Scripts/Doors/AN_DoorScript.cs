using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_DoorScript : MonoBehaviour
{
    public GameObject Player;
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
    float currentLim;

    void Start()
    {
        Kicked = Player.GetComponent<Movement>().Kicked;
        pAnimation = Player.GetComponent<Animator>();
        rbDoor = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && NearView())
        {
            pAnimation.SetFloat("Kick", 1);
            StartCoroutine(DoorKick());
            Kicked = true;
        }
        else if (Kicked)
        {
            pAnimation.SetFloat("Kick", -1);
            Kicked = false;
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
        if (isOpened)
        {
            currentLim = 120f;
        }
        else
        {
            // currentLim = hinge.angle;
            if (currentLim > 1f)
                currentLim -= .5f * OpenSpeed;
        }

       
        hingeLim.max = currentLim;
        hingeLim.min = -currentLim;
        hinge.limits = hingeLim;
    }
    private IEnumerator DoorKick()
    {
        pAnimation.SetFloat("VelocityZ", 0);
        pAnimation.SetFloat("VelocityX", 0);
        Player.GetComponent<Movement>().Kicked = true;
        yield return new WaitForSeconds(0.65f);
        rbDoor.AddForce(Player.transform.forward * 80000f);
        yield return new WaitForSeconds(0.65f);
        Player.GetComponent<Movement>().Kicked = false;
    }
}
