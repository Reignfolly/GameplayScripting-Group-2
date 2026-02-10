using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Rigidbody rb;
    public float forwardForce = 5f;
    public float strafeForce = 5f;


    // Update is called once per frame
    void FixedUpdate()
    {
    
        if (Input.GetKey("d"))
        {
            rb.AddForce(strafeForce * Time.deltaTime, 0, 0,ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-strafeForce * Time.deltaTime, 0, 0,ForceMode.VelocityChange);
        }
                if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime,ForceMode.VelocityChange);
        }
                if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime,ForceMode.VelocityChange);
        }
    }
}
