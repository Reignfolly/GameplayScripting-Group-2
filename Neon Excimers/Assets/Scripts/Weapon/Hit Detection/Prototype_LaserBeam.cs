using UnityEngine;
using System.Collections;

public class Prototype_LaserBeam : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserPrefab;     // Prefab with LineRenderer
    public float laserRange = 30f;     // Maximum laser distance
    public float laserLifetime = 0.08f; // How long the beam exists
    public float laserWidth = 0.12f;   // Starting beam thickness

    [Header("Debug")]
    public bool showDebugRay = false;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
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
        Vector3 startPoint = transform.position;

        // Create ray from camera to mouse
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit mouseHit;
        Vector3 targetPoint;

        // Find where the mouse is pointing in world space
        if (Physics.Raycast(ray, out mouseHit))
        {
            targetPoint = mouseHit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(laserRange);
        }

        // Direction from player toward mouse
        Vector3 direction = (targetPoint - startPoint).normalized;

        RaycastHit hit;

        Vector3 endPoint;

        // Laser with limited range
        if (Physics.Raycast(startPoint, direction, out hit, laserRange))
        {
            endPoint = hit.point;

            if (hit.collider.CompareTag("Enemy"))
            {
                Disintegrate(hit.collider.gameObject);
            }
        }
        else
        {
            endPoint = startPoint + direction * laserRange;
        }

        if (showDebugRay)
            Debug.DrawLine(startPoint, endPoint, Color.red, 1f);

        CreateLaserVisual(startPoint, endPoint);
    }

    void CreateLaserVisual(Vector3 start, Vector3 end)
    {
        GameObject laser = Instantiate(laserPrefab);

        LineRenderer lr = laser.GetComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;

        StartCoroutine(FadeLaser(lr, laser));
    }

    IEnumerator FadeLaser(LineRenderer lr, GameObject laser)
    {
        float timer = 0f;

        while (timer < laserLifetime)
        {
            timer += Time.deltaTime;

            float t = timer / laserLifetime;

            float width = Mathf.Lerp(laserWidth, 0f, t);

            lr.startWidth = width;
            lr.endWidth = width;

            yield return null;
        }

        Destroy(laser);
    }

    void Disintegrate(GameObject target)
    {
        var EnemyHealthModule = target.GetComponent<Health_Module>();
        EnemyHealthModule.TakeDamage(50);
        //Destroy(target);
    }
}
