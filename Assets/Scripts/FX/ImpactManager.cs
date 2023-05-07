using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactManager : MonoBehaviour
{
    public GameObject Breekhitprefab;
    public GameObject Woodhitprefab;
    public GameObject Metalhitprefab;
    public GameObject Enemyhitprefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ImpactSpawn(RaycastHit hit, int Revers)
    {
        //if (Input.GetMouseButton(0))
        //{
        //    if (timer >= 0)
        //    {
        //        timer -= Time.deltaTime;
        //    }
        //    else
        //    {
        if (hit.collider.gameObject.tag == "BreekWall")
        {
            Instantiate(Breekhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
        }
        else if (hit.collider.gameObject.tag == "WoodWall" || hit.collider.gameObject.tag == "Door")
        {
            Instantiate(Woodhitprefab, hit.point, Quaternion.LookRotation((transform.position - hit.point) * Revers, Vector3.up));
        }
        else if (hit.collider.gameObject.tag == "Metal")
        {
            Instantiate(Metalhitprefab, hit.point, Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal) * Revers/*laserposition.position - hit.point*/, Vector3.up));
        }
        else if (hit.collider.gameObject.tag == "Enemy")
        {
            Instantiate(Enemyhitprefab, hit.point, Quaternion.LookRotation((transform.position - hit.point) * Revers, Vector3.up));
        }
        else if (hit.collider.gameObject.tag == "EnemyKnife")
        {
            Instantiate(Enemyhitprefab, hit.point, Quaternion.LookRotation((transform.position - hit.point) * Revers, Vector3.up));
        }
        //        timer = time;
        //    }
        //}
    }
}
