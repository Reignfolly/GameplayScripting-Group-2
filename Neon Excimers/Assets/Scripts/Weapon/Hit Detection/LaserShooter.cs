using UnityEngine;

public class LaserShooter : MonoBehaviour
{   
    
    [Header("References")]

    public WeaponStats weaponStats; // Reference to weapon stats ScriptableObject
    public Transform firePoint;
    public Camera cam;
    public GameObject laserPrefab;

    [Header("Laser Stats")]
    public float range = 25f;
    public float damage = 50f;
    public float duration = 0.1f;
    public float width = 0.2f;

    [Header("Debug")]
    public bool showDebug = false;

    void PhysicsUpdate()
    {   
        if (weaponStats != null) {//update weapon stats from weapon stats scriptable object
            range = weaponStats.range;      
            damage = weaponStats.damage; 
            width = weaponStats.area;       
        }
    }
    void Update()
    {   

        if (Input.GetMouseButtonDown(0))
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        Vector3 start = firePoint.position;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Vector3 target;

        if (Physics.Raycast(ray, out RaycastHit mouseHit))
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

        if (Physics.Raycast(start, direction, out hit, range))
        {
            end = hit.point;

            if (hit.collider.CompareTag("Enemy"))
            {
                var hp = hit.collider.GetComponent<Health_Module>();
                if (hp != null)
                    hp.TakeDamage((int)damage);
            }
        }
        else
        {
            end = start + direction * range;
        }

        if (showDebug)
            Debug.DrawLine(start, end, Color.red, 1f);

        SpawnLaser(start, end);
    }

    void SpawnLaser(Vector3 start, Vector3 end)
    {
        GameObject laser = Instantiate(laserPrefab);

        LaserBeam beam = laser.GetComponent<LaserBeam>();
        beam.Initialize(start, end, width, duration);
    }
}