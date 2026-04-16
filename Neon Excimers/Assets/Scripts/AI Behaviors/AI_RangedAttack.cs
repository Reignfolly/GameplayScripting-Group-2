using UnityEngine;
using System;
using System.Collections;
public class AI_RangedAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public float lifeTime = 20f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void fire()
    {
        GameObject NewHostileBullet = Instantiate(bulletPrefab);
        Physics.IgnoreCollision(NewHostileBullet.GetComponent<Collider>(),
        bulletSpawn.parent.GetComponent<Collider>());

        NewHostileBullet.transform.position = bulletSpawn.position;
        Vector3 rotation = NewHostileBullet.transform.rotation.eulerAngles;

        NewHostileBullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        NewHostileBullet.gameObject.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(NewHostileBullet, lifeTime));
    }


    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
