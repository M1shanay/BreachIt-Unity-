using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer line;
    public Transform laserposition;
    public GameObject Breekhitprefab;
    public GameObject Woodhitprefab;
    public GameObject Metalhitprefab;
    public GameObject Enemyhitprefab;
    public float time = 0.05f;
    private float timer;
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
                if (Input.GetMouseButton(0))
                {
                    if (timer >= 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "BreekWall")
                        {
                            Instantiate(Breekhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal)/*laserposition.position - hit.point*/, Vector3.up));
                        }
                        else if (hit.collider.gameObject.tag == "WoodWall")
                        {
                            Instantiate(Woodhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal)/*laserposition.position - hit.point*/, Vector3.up));
                        }
                        else if (hit.collider.gameObject.tag == "Metal")
                        {
                            Instantiate(Metalhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal)/*laserposition.position - hit.point*/, Vector3.up));
                        }
                        else if (hit.collider.gameObject.tag == "Enemy")
                        {
                            Instantiate(Enemyhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal)/*laserposition.position - hit.point*/, Vector3.up));
                        }
                        timer = time;
                    }
                }
            }
        }
        else
        {
            line.SetPosition(1, transform.forward * 5000);
        }
    }
}
