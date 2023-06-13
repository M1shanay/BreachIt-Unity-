using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private PlayerAudio _audio;
    [SerializeField] private Animator _animator;
    [SerializeField] private Movement _playerMovement;

    private LineRenderer line;
    public Transform laserposition;

    private ImpactManager impactSpawner;

    public static Action ImpactDeal;
    public static bool CanFire = true;
    public static bool _isReloading = false;

    private float fireRate = 10f;
    private float nextTimeToFire = 0f;
    private int _bullets = 30;

    private float penetrationDistans = 20f;

    void Start()
    {
        _isReloading = false;
        CanFire = true;
        _bullets = 30;
        line = GetComponent<LineRenderer>();
        impactSpawner = GetComponent<ImpactManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LaserProjection();

        if (!Player._dead && !InGameUI._won && !_playerMovement.GetIsMobile)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && CanFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(transform.position, transform.forward);
                _audio.PlayShoot();
                _bullets--;
                InGameUI.SendBullets(_bullets);
            }
            if (_bullets <= 0)
            {
                CanFire = false;
            }
            StartReloading();
        }
        //SecondHit();
    }

    public IEnumerator MobileShooting()
    {
        while (true)
        {
            if (!Player._dead && !InGameUI._won)
            {
                if (Time.time >= nextTimeToFire && CanFire)
                {
                    _playerMovement.MobileShootAnimation(1);
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot(transform.position, transform.forward);
                    _audio.PlayShoot();
                    _bullets--;
                    InGameUI.SendBullets(_bullets);
                }
                if (_bullets <= 0)
                {
                    _playerMovement.MobileShootAnimation(2);
                    CanFire = false;
                }
                StartReloading();
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void StartReloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {
            CanFire = false;
            _isReloading = true;
            _audio.PlayReload();
            _animator.Play("Reloading");
            StartCoroutine(Reloading());
        }
    }

    public void StartReloadingMobile()
    {
        if (!_isReloading)
        {
            CanFire = false;
            _isReloading = true;
            _audio.PlayReload();
            _animator.Play("Reloading");
            StartCoroutine(Reloading());
        }
    }

    public IEnumerator Reloading()
    {
        yield return new WaitForSeconds(4f);
        _bullets = 30;
        InGameUI.SendBullets(_bullets);
        CanFire = true;
        _isReloading = false;
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

                    if (Physics.Raycast(penetrationRay, out penetrationHit, 3f))
                    {
                        Debug.DrawLine(hit.point, penetrationHit.point, Color.yellow);
                        
                        if (penetrationHit.collider)
                        {
                            impactSpawner.ImpactSpawn(penetrationHit, -1);
                            //DamageManager(hit);

                            Ray wallbang = new Ray(penetrationHit.point + ray.direction, ray.direction);
                            RaycastHit wallbangHit;
                            
                            float wallbangDist = penetrationDistans/Vector3.Distance(transform.position, penetrationHit.point);

                            Debug.DrawRay(penetrationHit.point, ray.direction * wallbangDist, Color.red);
                            if (Physics.Raycast(wallbang, out wallbangHit, wallbangDist))
                            {
                                Debug.DrawLine(penetrationHit.point, wallbangHit.point, Color.red);
                                if (wallbangHit.collider)
                                {
                                    impactSpawner.ImpactSpawn(wallbangHit, 1);
                                    DamageManager(wallbangHit);
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
        if (target.collider.CompareTag("Enemy"))
        {
            Soldier soldier = target.transform.GetComponent<Soldier>();
            soldier.ApllyDamage(damage);
        }
        else if (target.collider.CompareTag("EnemyKnife"))
        {
            SoldierKnife soldierKnife = target.transform.GetComponent<SoldierKnife>();
            soldierKnife.ApllyDamage(damage);
        }
    }

    public int GetBulletCount
    {
        get { return _bullets; }
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