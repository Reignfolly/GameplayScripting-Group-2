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
    // This should be the player
    [SerializeField] private GameObject MyTarget;

    // This is the enemy the AI wants to go towards and follow
    [SerializeField] private GameObject FriendlyTarget;


    // This variable determines AI behavior. All AI behavior types are stored in this script.
    [SerializeField] Enemy_Types MyEnemy_AI_Type;
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
        switch (MyEnemy_AI_Type)
        {
            case Enemy_Types.Standard:
                // Standard enemy type
                Begin_Standard_Behavior_Package();
                break;
            case Enemy_Types.Shark:
                // Shark enemy type
                Begin_Shark_Behavior_Package();
                break;
            case Enemy_Types.Bulldozer:
                // Bulldozer enemy type
                Begin_Bulldozer_Behavior_Package();
                break;
            case Enemy_Types.Medic:
                // Medic enemy type
                Begin_Medic_Behavior_Package();
                break;
            case Enemy_Types.Officer:
                // Officer enemy type
                Begin_Officer_Behavior_Package();
                break;

        }
        /*NavMeshAgent AI_Navigation_agent = GetComponent<NavMeshAgent>();
        switch (AI_Target_To_Search_For)
        {
            case "Player":
                doPlayer_Chase(AI_Navigation_agent, MyTarget);
                break;
            case "Goalposts":
                move_Between_Goalposts(AI_Navigation_agent, current_Goalpost_Index, MyTarget);
                break;
        }*/
    }



    // (BEGIN) Search For Target Scripts (BEGIN)




    // [END] Search For Target Scripts [END]




    // (BEGIN) Go To Target Scripts (BEGIN)

    private void doPlayer_Chase(NavMeshAgent AI_Navigation_agent, GameObject MyTarget)
    {
        AI_Navigation_agent.destination = MyTarget.transform.position;
    }

    private void doAlly_Chase(NavMeshAgent AI_Navigation_agent, GameObject FriendlyTarget)
    {
        AI_Navigation_agent.destination = FriendlyTarget.transform.position;
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

    // [END] Go To Target Scripts [END]

    void Begin_Standard_Behavior_Package()
    {
        // I am the Standard Enemy Type
        // I am dumb but I am relentless

        // AI: Find player position. Go to player position.

        NavMeshAgent AI_Navigation_agent = GetComponent<NavMeshAgent>();
        doPlayer_Chase(AI_Navigation_agent, MyTarget);

    }
    void Begin_Shark_Behavior_Package()
    {
        // I am the Shark Enemy type
        // I am a bit smarter but predictable

        // AI: move to the player, then move around them for a bit, then charge at them.
        // Alternatively, move to the player, then immediately charge


    }
    void Begin_Bulldozer_Behavior_Package()
    {
        // I am the Bulldozer Enemy type
        // Something something "I am the %&#$#& wall" something something

        // AI: Find player, Intercept the player

    }
    void Begin_Medic_Behavior_Package()
    {
        // I am the Medic Enemy type
        // I am going to heal my friends

        // AI: find nearest enemy that has less than max health. Go to enemy and heal them up to max.
        // Repeat



    }
    void Begin_Officer_Behavior_Package()
    {
        // I am the Officer Enemy type
        // I am going to buff all my friends

        // AI: Find the highest value teammate, follow them
    }
}
