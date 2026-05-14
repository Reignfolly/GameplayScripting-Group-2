using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LaserShooter : MonoBehaviour
{

    [Header("References")]

    public WeaponStats WeaponStats; // Reference to weapon stats ScriptableObject
    public Transform firePoint;
    public Camera cam;
    public GameObject laserPrefab;

    public GameObject FarLaserPrefab;

    [Header("Laser Stats")]
    public float range = 25f;
    public float damage = 50f;
    public float duration = 0.1f;
    public float width = 0.8f;
    public float fireRate = 0.08f;

    public int PenetrationFactor = 3;


    [Header("Debug")]
    public bool showDebug = false;

    public GameObject HitSoundPrefab;
    public GameObject LaserFireSoundPrefab;

    private float fireDelay = 0f;

    public GameObject hitEffect;

    public LayerMask IgnoreThisLayer;
    void Awake()
    {
        UpdateGunStats();
    }
    void Update()
    {
        fireDelay += Time.deltaTime; //fixed fire rate by making it always count
        if (Input.GetMouseButton(0) && fireDelay >= fireRate)
        {
            // Fires every fireRate
            fireDelay = 0;
            FireLaser();
            
        }

    }
    public void UpdateGunStats()
    {
        if (WeaponStats == null) return;
        //called whenever an upgrade button is pressed
        // The gun now uses its own local copy of the float.
        range = WeaponStats.range;
        damage = WeaponStats.damage;
        width = WeaponStats.area;
        fireRate = WeaponStats.attackSpeed;

    }

    void FireLaser()
    {

        float Maximum_Full_Laser_Range = (range * 2) * 1.25f;
        Vector3 start = firePoint.position;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Vector3 target;

        if (Physics.Raycast(ray, out RaycastHit mouseHit, 9999, ~IgnoreThisLayer))
        {
            target = mouseHit.point;
        }
        else
        {
            target = ray.GetPoint(range);
        }

        // 🔥 KEEP LASER FLAT (TOP-DOWN FIX)
        target.y = start.y;

        Vector3 direction = (target - start).normalized;

        RaycastHit hit;
        Vector3 end;

        // Gets all the objects that can be reasonably hit by our laser
        RaycastHit[] All_Objects_Hit;
        All_Objects_Hit = Physics.RaycastAll(start, direction, Maximum_Full_Laser_Range, ~IgnoreThisLayer);

        // This list takes all the objects hit and-
        // filters for only enemies
        List<RaycastHit_Comparable> All_Enemies_Hit = new List<RaycastHit_Comparable>();
        for (int i = 0; i < All_Objects_Hit.Length; i++)
        {
            RaycastHit newObjHit = All_Objects_Hit[i];
            
            if (newObjHit.collider.transform.root.CompareTag("Enemy"))
            {
                RaycastHit_Comparable NewRaycastHit_ToCompare = new RaycastHit_Comparable()
                {
                    TheRaycastHitItself = newObjHit,
                    Distance_To_Laser_Origin = newObjHit.distance
                };
                All_Enemies_Hit.Add(NewRaycastHit_ToCompare);
            }
        }
        

        if (All_Enemies_Hit.Count > 0)
        {
            // orders list from closest to furthest
            GameObject HitSound = Instantiate(HitSoundPrefab);
            HitSound.transform.position = All_Enemies_Hit[0].TheRaycastHitItself.transform.position;
            All_Enemies_Hit.Sort();
            
            if (All_Enemies_Hit.Count > 2)
            {
                for (int i = 0; i < PenetrationFactor; i++)
                {
                    RaycastHit Enemy_Hit = All_Enemies_Hit[i].TheRaycastHitItself;
                    var hp = Enemy_Hit.collider.transform.root.GetComponent<Health_Module>();
                    if (Enemy_Hit.collider.transform.root.CompareTag("Enemy"))
                    {
                        if (hp != null)
                            hp.TakeDamage((int)damage);
                            spawnWeaponEffects(Enemy_Hit.point);
                    }
                }
                end = All_Enemies_Hit[(PenetrationFactor - 1)].TheRaycastHitItself.point;
            } else {
                RaycastHit Enemy_Hit = All_Enemies_Hit[0].TheRaycastHitItself;
                var hp = Enemy_Hit.collider.transform.root.GetComponent<Health_Module>();
                if (Enemy_Hit.collider.transform.root.CompareTag("Enemy"))
                {
                    if (hp != null)
                        hp.TakeDamage((int)damage);
                        spawnWeaponEffects(Enemy_Hit.point);
                }
                end = All_Enemies_Hit[0].TheRaycastHitItself.point;
            }
        }
        else
        {
            end = start + direction * Maximum_Full_Laser_Range;
        };
        
        
        SpawnLaser(start, end);





        // Normal Laser // NONE OF THIS HAS BEEN TOUCHED, IT'S JUST COMMENTED.
        /*if (Physics.Raycast(start, direction, out hit, range, ~IgnoreThisLayer))
        {
            end = hit.point;

            spawnWeaponEffects(hit.point);

            if (hit.collider.transform.root.CompareTag("Enemy"))
            {
                // Handles the creation of temporary sound object holders
                GameObject HitSound = Instantiate(HitSoundPrefab);
                HitSound.transform.position = hit.transform.position;
                var hp = hit.collider.transform.root.GetComponent<Health_Module>();


                if (hp != null)
                    hp.TakeDamage((int)damage);

            }
        }
        else
        {
            end = start + direction * range;

            // This is for the Far Laser
            var FarStart = end;
            var FarEnd = end;
            if (Physics.Raycast(FarStart, direction, out hit, (range * 1.25f), ~IgnoreThisLayer))
            {
                FarEnd = hit.point;
                spawnWeaponEffects(hit.point);

                if (hit.collider.transform.root.CompareTag("Enemy"))
                {
                    // Handles the creation of temporary sound object holders
                    GameObject HitSound = Instantiate(HitSoundPrefab);
                    HitSound.transform.position = hit.transform.position;
                    var hp = hit.collider.transform.root.GetComponent<Health_Module>();


                    if (hp != null)
                    {
                        hp.TakeDamage((int)damage / 2); // Far laser does half damage
                    }
                }
            }
            else
            {
                FarEnd = FarStart + direction * (range * 1);
            }

            SpawnFarLaser(FarStart, FarEnd);

            // End Whitish Blue Far Laser Stuffs
        }*/



    }

    void SpawnLaser(Vector3 start, Vector3 end)
    {
        GameObject laser = Instantiate(laserPrefab);

        LaserBeam beam = laser.GetComponent<LaserBeam>();
        beam.Initialize(start, end, width, duration);

        GameObject FireSound = Instantiate(LaserFireSoundPrefab);
        FireSound.transform.position = this.gameObject.transform.position;
    }

    void SpawnFarLaser(Vector3 start, Vector3 end)
    {
        GameObject laser = Instantiate(FarLaserPrefab);

        LaserBeam beam = laser.GetComponent<LaserBeam>();
        beam.Initialize(start, end, width, duration);
    }

    void spawnWeaponEffects(Vector3 HitPoint)
    {
        GameObject NewWeaponEffect = Instantiate(hitEffect, HitPoint, Quaternion.identity);
    }
}