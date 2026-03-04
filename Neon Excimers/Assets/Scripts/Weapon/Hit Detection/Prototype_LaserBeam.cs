using UnityEngine;
using System.Collections.Generic;

public class Prototype_LaserBeam : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool Show_Debug_Laser = false;
    public GameObject LaserBeam_Prefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast goes from the player object to where the player clicked
            // We need to get the player's position, and then get where the player clicked.
            var cam = Camera.main;
            GameObject Player = GameObject.Find("Player");
            RaycastHit hit;
            Vector3 RaycastStartPoint = Player.transform.position;
            //Vector3 RaycastEndPoint = Input.mousePosition;
            Vector3 RaycastEndPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, (Input.mousePosition.y - cam.pixelHeight), cam.nearClipPlane));

            // Force the Raycast Y-coordinate to be the exact same
            // This 100% Needs to be changed if our levels are going to have varying heights.



            //Debug.Log(RaycastStartPoint.x + " | " + RaycastStartPoint.y + " | " + RaycastStartPoint.z);
            // Debug.Log(RaycastEndPoint);

            // Line Cast lets us cast between two points rather than cast in a direction
            // Since we want our Beam to go between where we clicked and our player, this should-
            //-work fine for now.
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.gameObject.name);
                RaycastEndPoint = hit.point;
                RaycastEndPoint.y = RaycastStartPoint.y;



                Vector3 RayCastDirection = RaycastEndPoint - RaycastStartPoint;
                if (Show_Debug_Laser == true)
                {
                    CreateLaserBeam(RaycastStartPoint, RaycastEndPoint);
                }

                if (Physics.Raycast(RaycastStartPoint, RayCastDirection, out hit, 800))
                {
                    // Pew Pew
                    //Debug.Log("Pew Pew!");
                    //Debug.Log(hit.collider.gameObject.name);

                    // Instantly obliterate the target
                    Disentegrate(hit.collider.gameObject);

                    //Debug.Log(RaycastStartPoint);
                    //Debug.Log(RaycastEndPoint);


                }
                else
                {
                    // Debug.Log("Your laser is broken :(");
                }
            }


        }
    }

    void CreateLaserBeam(Vector3 Start, Vector3 End)
    {
        int width = 2;
        var offset = End - Start;
        var scale = new Vector3(width, offset.magnitude / 2f, width);
        var position2 = Start;
        var position = End;
        Vector3 scaleChange = new Vector3(0f, 10f, 0f);

        var Endcylinder = Instantiate(LaserBeam_Prefab, position, Quaternion.identity);

        var Startcylinder = Instantiate(LaserBeam_Prefab, position2, Quaternion.identity);
        Startcylinder.transform.localScale += scaleChange;

        Startcylinder.transform.LookAt(End, Vector3.back);
        //cylinder.transform.up = offset;
        //cylinder.transform.localScale = scale;
        Debug.DrawLine(Start, End, Color.red, 8, false);
    }

    // Instantly vaporizes target. No refunds.
    void Disentegrate(GameObject target)
    {
        Destroy(target);
    }
}

