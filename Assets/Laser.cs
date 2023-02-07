using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer line;
    public Transform laserposition;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, laserposition.position);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit))
        {
            if (hit.collider)
            {
                line.SetPosition(1, hit.point);
            }
        }
        else
        {
            line.SetPosition(1, transform.forward * 5000);
        }
    }
}
