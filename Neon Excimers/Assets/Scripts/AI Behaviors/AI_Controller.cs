using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
// This script is the brain of AI characters. Primarily responsible for getting the AI character-
// -to the player character
public class AI_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // AI_Target_To_Search_For decides what an AI character should search for. 
    // Right now, the AI should only look for Goalposts (static, pre-defined transform.positions)-
    // -or they should look for the Player
    [SerializeField] private string AI_Target_To_Search_For = "Player";

    // The Goalpost code doesn't function correctly.
    // Each AI_Controller creates its own goalposts List that isn't shared globally and is local to each one
    // The intended behavior is for there to be one, global goalposts List that all the AI_Controllers loop through
    // Would recommend not changing the AI_Target_To_Search_For to goalposts until we can fix it - Aiden
    public List<UnityEngine.GameObject> goalposts = new List<UnityEngine.GameObject>();
    private int current_Goalpost_Index = 0;
    // Don't touch ^

    // This is the object the AI wants to go towards (Not its transform / position)
    [SerializeField] private GameObject MyTarget;
    void Start()
    {
        // Get Navmesh agent (component that allows AI to move to a destination)

        // AI_Navigation_agent.destination = Navigation_Goal.transform.position;
        NavMeshAgent AI_Navigation_agent = GetComponent<NavMeshAgent>();
        switch (AI_Target_To_Search_For)
        {
            case "Player":
                // AI Character will move to player
                // .Find is very expensive. Should only be done on startup.
                MyTarget = GameObject.Find("Player");
                doPlayer_Chase(AI_Navigation_agent, MyTarget);
                break;
            case "Goalposts":
                move_Between_Goalposts(AI_Navigation_agent, current_Goalpost_Index, MyTarget);
                break;


        }
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent AI_Navigation_agent = GetComponent<NavMeshAgent>();
        switch (AI_Target_To_Search_For)
        {
            case "Player":
                doPlayer_Chase(AI_Navigation_agent, MyTarget);
                break;
            case "Goalposts":
                move_Between_Goalposts(AI_Navigation_agent, current_Goalpost_Index, MyTarget);
                break;


        }
    }


    private void doPlayer_Chase(NavMeshAgent AI_Navigation_agent, GameObject MyTarget)
    {
        AI_Navigation_agent.destination = MyTarget.transform.position;
    }

    private void move_Between_Goalposts(NavMeshAgent AI_Navigation_agent, int current_Goalpost_Index, GameObject MyTarget)
    {
        if (AI_Navigation_agent.remainingDistance <= 5 && MyTarget != null)
        {
            Debug.Log("I made it to a goalpost!");
            current_Goalpost_Index += 1;
            AI_Navigation_agent.destination = goalposts[current_Goalpost_Index].transform.position;
        }
        else
        {
            AI_Navigation_agent.destination = goalposts[current_Goalpost_Index].transform.position;
        }
    }
}
