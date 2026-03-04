using UnityEngine;
using System.Collections;

public class Self_Destuct : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float targetTime = 0.65f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
