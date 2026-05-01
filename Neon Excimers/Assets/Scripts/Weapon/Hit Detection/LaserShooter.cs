using UnityEngine;

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


    [Header("Debug")]
    public bool showDebug = false;

    public GameObject HitSoundPrefab;
    public GameObject LaserFireSoundPrefab;

    private float fireDelay = 0f;

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
            FireLaser();
            fireDelay = 0;
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

        // Normal Laser
        if (Physics.Raycast(start, direction, out hit, range))
        {
            end = hit.point;

            if (hit.collider.CompareTag("Enemy"))
            {
                // Handles the creation of temporary sound object holders
                GameObject HitSound = Instantiate(HitSoundPrefab);
                HitSound.transform.position = hit.transform.position;
                var hp = hit.collider.GetComponent<Health_Module>();


                if (hp != null)
                    hp.TakeDamage((int)damage);
            }
        }
        else
        {
            end = start + direction * range;

            // This is for the Whitish Blue laser
            var FarStart = end;
            var FarEnd = end;
            if (Physics.Raycast(FarStart, direction, out hit, (range * 2)))
            {
                FarEnd = hit.point;

                if (hit.collider.CompareTag("Enemy"))
                {
                    // Handles the creation of temporary sound object holders
                    GameObject HitSound = Instantiate(HitSoundPrefab);
                    HitSound.transform.position = hit.transform.position;
                    var hp = hit.collider.GetComponent<Health_Module>();


                    if (hp != null)
                        hp.TakeDamage((int)damage / 6);
                }
            }
            else
            {
                FarEnd = FarStart + direction * (range * 2);
            }

            SpawnFarLaser(FarStart, FarEnd);

            // End Whitish Blue Far Laser Stuffs
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

        GameObject FireSound = Instantiate(LaserFireSoundPrefab);
        FireSound.transform.position = this.gameObject.transform.position;
    }

    void SpawnFarLaser(Vector3 start, Vector3 end)
    {
        GameObject laser = Instantiate(FarLaserPrefab);

        LaserBeam beam = laser.GetComponent<LaserBeam>();
        beam.Initialize(start, end, width, duration);
    }
}