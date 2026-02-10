using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Rigidbody rb;
    public float forwardForce = 500f;
    public float strafeForce = 200f;


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        if (Input.GetKey("d"))
        {
            rb.AddForce(strafeForce * Time.deltaTime, 0, 0,ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-strafeForce * Time.deltaTime, 0, 0,ForceMode.VelocityChange);
        }
    }
}
