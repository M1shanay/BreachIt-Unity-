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

    private float penetrationDistans = 15f;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LaserProjection();
        Penetration(transform.position, transform.forward);
        //SecondHit();
    }
    void LaserProjection()
    {
        line.SetPosition(0, laserposition.position);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,Mathf.Infinity))
        {
            if (hit.collider)
            {
                line.SetPosition(1, hit.point);
                ImpactManager(hit,1);
            }
        }
        else
        {
            line.SetPosition(1, transform.forward * 5000);
        }
    }
    void Penetration(Vector3 position, Vector3 forward)
    {
        Ray ray = new Ray(position, forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Ray penetrationRay = new Ray(hit.point + ray.direction, -ray.direction);
            RaycastHit penetrationHit;
            if (hit.collider)
            {
                ImpactManager(hit, 1);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wallbang"))
                {
                    if (Physics.Raycast(penetrationRay, out penetrationHit, 3f))
                    {
                        Debug.DrawLine(hit.point, penetrationHit.point, Color.yellow);
                        if (penetrationHit.collider)
                        {
                            ImpactManager(penetrationHit, -1);
                            Ray wallbang = new Ray(penetrationHit.point + ray.direction, ray.direction);
                            RaycastHit wallbangHit;
                            float wallbangDist = 2f;//penetrationDistans/Vector3.Distance(transform.position, penetrationHit.point);
                            Debug.Log(wallbangDist);
                            Debug.DrawRay(penetrationHit.point, ray.direction * wallbangDist, Color.green);
                            if (Physics.Raycast(wallbang, out wallbangHit, wallbangDist))
                            {
                                Debug.DrawLine(penetrationHit.point, wallbangHit.point, Color.green);
                                if (wallbangHit.collider)
                                {
                                    ImpactManager(wallbangHit, 1);
                                    Penetration(wallbangHit.point, wallbang.direction);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    void ImpactManager(RaycastHit hit,int Revers)
    {
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
                    Instantiate(Breekhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
                }
                else if (hit.collider.gameObject.tag == "WoodWall"|| hit.collider.gameObject.tag == "Door")
                {
                    Instantiate(Woodhitprefab, hit.point, Quaternion.LookRotation((laserposition.position - hit.point)*Revers, Vector3.up));
                }
                else if (hit.collider.gameObject.tag == "Metal")
                {
                    Instantiate(Metalhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
                }
                else if (hit.collider.gameObject.tag == "Enemy")
                {
                    Instantiate(Enemyhitprefab, hit.point, Quaternion.LookRotation((laserposition.position - hit.point) * Revers, Vector3.up));
                }
                timer = time;
            }
        }
    }
    //void SecondHit()
    //{
    //    RaycastHit WallbangHit;
    //    if (Physics.Raycast(transform.position, transform.forward, out WallbangHit,Mathf.Infinity,~(1 << LayerMask.NameToLayer("Wallbang"))))
    //    {
    //        if (WallbangHit.collider)
    //        {
    //            if (Input.GetMouseButton(0))
    //            {
    //                if (timer >= 0)
    //                {
    //                    timer -= Time.deltaTime;
    //                }
    //                else
    //                {
    //                    if (WallbangHit.collider.gameObject.tag == "BreekWall")
    //                    {
    //                        Instantiate(Breekhitprefab, WallbangHit.point, Quaternion.LookRotation(laserposition.position - WallbangHit.point, Vector3.up));
    //                    }
    //                    else if (WallbangHit.collider.gameObject.tag == "WoodWall")
    //                    {
    //                        Instantiate(Woodhitprefab, WallbangHit.point, Quaternion.LookRotation(laserposition.position - WallbangHit.point, Vector3.up));
    //                    }
    //                    else if (WallbangHit.collider.gameObject.tag == "Metal")
    //                    {
    //                        Instantiate(Metalhitprefab, WallbangHit.point, Quaternion.LookRotation(laserposition.position - WallbangHit.point, Vector3.up));
    //                    }
    //                    else if (WallbangHit.collider.gameObject.tag == "Enemy")
    //                    {
    //                        Instantiate(Enemyhitprefab, WallbangHit.point, Quaternion.LookRotation(laserposition.position - WallbangHit.point, Vector3.up));
    //                    }
    //                    timer = time;
    //                }
    //            }
    //        }
    //    }
    //}
}
