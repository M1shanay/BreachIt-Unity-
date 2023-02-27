using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    private LineRenderer line;
    public Transform laserposition;

    private ImpactManager impactSpawner;

    public static Action ImpactDeal;

    private float fireRate = 15f;
    private float nextTimeToFire = 0f;

    private float penetrationDistans = 20f;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        impactSpawner = GetComponent<ImpactManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LaserProjection();
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot(transform.position, transform.forward);
        }
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
            }
        }
        else
        {
            line.SetPosition(1, transform.forward * 5000);
        }
    }
    void Shoot(Vector3 position, Vector3 forward)
    {
        Ray ray = new Ray(position, forward);
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Ray penetrationRay = new Ray(hit.point + ray.direction, -ray.direction);
            RaycastHit penetrationHit;
           
            if (hit.collider)
            {
                impactSpawner.ImpactSpawn(hit, 1);
                DamageManager(hit);

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wallbang"))
                {
                    //if(hit.collider.tag == "Door")
                    //{
                    //    hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*2000f);
                    //}

                    if (Physics.Raycast(penetrationRay, out penetrationHit, 3f))
                    {
                        Debug.DrawLine(hit.point, penetrationHit.point, Color.yellow);
                        
                        if (penetrationHit.collider)
                        {
                            impactSpawner.ImpactSpawn(penetrationHit, -1);
                            DamageManager(hit);

                            Ray wallbang = new Ray(penetrationHit.point + ray.direction, ray.direction);
                            RaycastHit wallbangHit;
                            
                           
                            float wallbangDist = penetrationDistans/Vector3.Distance(transform.position, penetrationHit.point);

                            Debug.DrawRay(penetrationHit.point, ray.direction * wallbangDist, Color.green);
                            if (Physics.Raycast(wallbang, out wallbangHit, wallbangDist))
                            {
                                Debug.DrawLine(penetrationHit.point, wallbangHit.point, Color.green);
                                if (wallbangHit.collider)
                                {
                                    impactSpawner.ImpactSpawn(wallbangHit, 1);
                                    DamageManager(hit);
                                    //Shoot(wallbangHit.point, wallbang.direction);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    //void ImpactManager(RaycastHit hit, int Revers)
    //{
    //    //if (Input.GetMouseButton(0))
    //    //{
    //    //    if (timer >= 0)
    //    //    {
    //    //        timer -= Time.deltaTime;
    //    //    }
    //    //    else
    //    //    {
    //            if (hit.collider.gameObject.tag == "BreekWall")
    //            {
    //                Instantiate(Breekhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
    //            }
    //            else if (hit.collider.gameObject.tag == "WoodWall" || hit.collider.gameObject.tag == "Door")
    //            {
    //                Instantiate(Woodhitprefab, hit.point, Quaternion.LookRotation((laserposition.position - hit.point) * Revers, Vector3.up));
    //            }
    //            else if (hit.collider.gameObject.tag == "Metal")
    //            {
    //                Instantiate(Metalhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
    //            }
    //            else if (hit.collider.gameObject.tag == "Enemy")
    //            {
    //                Instantiate(Enemyhitprefab, hit.point, Quaternion.LookRotation((laserposition.position - hit.point) * Revers, Vector3.up));
    //            }
    //    //        timer = time;
    //    //    }
    //    //}
    //}
    void DamageManager(RaycastHit target)
    {
        if (target.collider.tag == "Enemy")
        {
            Enemy enemy = target.transform.GetComponent<Enemy>();
            //enemy.TakeDamage(damage);
        }
    }
    //void ImpactManager(RaycastHit hit,int Revers)
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        if (timer >= 0)
    //        {
    //            timer -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            if (hit.collider.gameObject.tag == "BreekWall")
    //            {
    //                Instantiate(Breekhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
    //            }
    //            else if (hit.collider.gameObject.tag == "WoodWall"|| hit.collider.gameObject.tag == "Door")
    //            {
    //                Instantiate(Woodhitprefab, hit.point, Quaternion.LookRotation((laserposition.position - hit.point)*Revers, Vector3.up));
    //            }
    //            else if (hit.collider.gameObject.tag == "Metal")
    //            {
    //                Instantiate(Metalhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
    //            }
    //            else if (hit.collider.gameObject.tag == "Enemy")
    //            {
    //                Instantiate(Enemyhitprefab, hit.point, Quaternion.LookRotation((laserposition.position - hit.point) * Revers, Vector3.up));
    //            }
    //            timer = time;
    //        }
    //    }
    //}
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
