using UnityEngine;
using System;
using System.Collections.Generic;
public class AI_SpawnPositionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this.gameObject.GetComponentInChildren<MeshRenderer>();
        var GameManagerHolder = GameObject.Find("GameManager");
        var AI_Manager_Script = GameManagerHolder.gameObject.GetComponent<AI_GameManager>();
        AI_Manager_Script.Add_SpawnPoint_To_List(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
