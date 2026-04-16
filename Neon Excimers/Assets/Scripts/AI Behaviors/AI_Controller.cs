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
    [SerializeField] private GameObject PlayerAsTarget;

    [SerializeField] private NavMeshAgent AI_Navigation_agent;

    // This is the enemy the AI wants to go towards and follow
    [SerializeField] private GameObject FriendlyTarget = null;


    // This variable determines AI behavior. All AI behavior types are stored in this script.
    [SerializeField] Enemy_Types MyEnemy_AI_Type;
    void Start()
    {
        // Get Navmesh agent (component that allows AI to move to a destination)

        // AI_Navigation_agent.destination = Navigation_Goal.transform.position;
        NavMeshAgent AI_Navigation_agent = GetComponent<NavMeshAgent>();
        MonoBehaviour AI_Enemy_Tracker_Module = this.gameObject.GetComponentInChildren<Enemy_Spawn_Tracker>();
        switch (AI_Target_To_Search_For)
        {
            case "Player":
                // AI Character will move to player
                // .Find is very expensive. Should only be done on startup.
                PlayerAsTarget = GameObject.Find("Player");
                doPlayer_Chase(AI_Navigation_agent, PlayerAsTarget);
                break;
            case "Goalposts":
                move_Between_Goalposts(AI_Navigation_agent, current_Goalpost_Index, PlayerAsTarget);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AI_Navigation_agent = GetComponent<NavMeshAgent>();
        switch (MyEnemy_AI_Type)
        {
            case Enemy_Types.Standard:
                // Standard enemy type
                Begin_Standard_Behavior_Package();
                break;
            case Enemy_Types.Ranger:
                // Ranger enemy type
                Begin_Ranger_Behavior_Package();
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
                doPlayer_Chase(AI_Navigation_agent, PlayerAsTarget);
                break;
            case "Goalposts":
                move_Between_Goalposts(AI_Navigation_agent, current_Goalpost_Index, PlayerAsTarget);
                break;
        }*/
    }



    // (BEGIN) Search For Target Scripts (BEGIN)




    // [END] Search For Target Scripts [END]
    private GameObject SearchFor_WoundedAlly()
    {
        // If we already have a friendly target selected, and they need health, heal them first
        if (FriendlyTarget != null && FriendlyTarget != this)
        {
            //Debug.Log("I already have a friendly target!");
            var TargetsHealthModule = FriendlyTarget.gameObject.GetComponent<Health_Module>();
            if (TargetsHealthModule.HasThisCharacterTakenDamage())
            {
                //Debug.Log("This target I have needs healing.");
                return FriendlyTarget;
            }
            else
            {
                //Debug.Log("They're already healed up.");
                FriendlyTarget = null;
                return FriendlyTarget;
            }
        }
        //Debug.Log("I don't have a friendly target! Finding one...");
        // We don't have a friendly target already selected, so go ahead and find another one.
        var AI_Enemy_Tracker_Module = this.gameObject.GetComponentInChildren<Enemy_Spawn_Tracker>();
        var Enemies_List = AI_Enemy_Tracker_Module.RetrieveEnemyList();
        // First, search for any enemies that have less health than their max (they've taken damage)
        foreach (GameObject ThisEnemy in Enemies_List)
        {
            var TargetsHealthModule = ThisEnemy.gameObject.GetComponent<Health_Module>();
            if (TargetsHealthModule.HasThisCharacterTakenDamage() && ThisEnemy.gameObject.name != this.gameObject.name)
            {
                /*Debug.Log("Found an enemy with less HP!");
                Debug.Log("Max Health: " + TargetsHealthModule.GetMaxHealth());
                Debug.Log("Current Health: " + TargetsHealthModule.GetHealth());
                Debug.Log("MY NAME: " + this.gameObject.name);
                Debug.Log("THEIR NAME: " + ThisEnemy.gameObject.name);*/
                return ThisEnemy;
            }
        }

        // if that fails, search for target in this list that is not itself
        foreach (GameObject ThisEnemy in Enemies_List)
        {
            if (ThisEnemy.gameObject.name != this.gameObject.name)
            {
                //Debug.Log("Had to look for literally any enemy at all.");
                var TargetsHealthModule = ThisEnemy.gameObject.GetComponent<Health_Module>();
                //if (TargetsHealthModule.HasThisCharacterTakenDamage())
                //{
                //Debug.Log("I found a random damaged enemy!");
                return ThisEnemy;
                //}
            }
        }

        // If that ALSO fails somehow...return null
        //Debug.Log("Couldn't find any enemy at all.");
        return null;
    }
    // (BEGIN) Go To Target Scripts (BEGIN)

    private void doPlayer_Chase(NavMeshAgent AI_Navigation_agent, GameObject PlayerAsTarget)
    {
        if (PlayerAsTarget != null)
        {
            AI_Navigation_agent.destination = PlayerAsTarget.transform.position;
        }
    }

    private void doStrafeAndShoot(NavMeshAgent AI_Navigation_agent, GameObject PlayerAsTarget)
    {
        if (PlayerAsTarget != null)
        {
            AI_Navigation_agent.destination = PlayerAsTarget.transform.position;
        }
    }

    private void doAlly_Chase(NavMeshAgent AI_Navigation_agent, GameObject FriendlyTarget)
    {
        AI_Navigation_agent.destination = FriendlyTarget.transform.position;
    }


    private void do_circle_around_player()
    {

    }
    private void move_Between_Goalposts(NavMeshAgent AI_Navigation_agent, int current_Goalpost_Index, GameObject PlayerAsTarget)
    {
        if (AI_Navigation_agent.remainingDistance <= 5 && PlayerAsTarget != null)
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

        doPlayer_Chase(AI_Navigation_agent, PlayerAsTarget);

    }

    void Begin_Ranger_Behavior_Package()
    {
        // I am the Ranger Enemy Type
        // Pew Pew Pew
        // AI: Find player position. Go within a certain distance of the player, open fire

        float distanceToPlayer = Vector3.Distance(this.gameObject.transform.position, PlayerAsTarget.transform.position);
        if (distanceToPlayer >= 30)
        {
            doPlayer_Chase(AI_Navigation_agent, PlayerAsTarget);
        }
        else
        {
            // Strafe and shoot the player
            //doStrafeAndShoot(AI_Navigation_agent, PlayerAsTarget);
        }

    }
    void Begin_Shark_Behavior_Package()
    {
        // I am the Shark Enemy type
        // I am a bit smarter but predictable

        // AI: move to the player, then move around them for a bit, then charge at them.
        // Alternatively, move to the player, then immediately charge
        do_circle_around_player();

    }
    void Begin_Bulldozer_Behavior_Package()
    {
        // I am the Bulldozer Enemy type
        // Something something "I am the %&#$#& wall" something something

        // AI: Find player, Intercept the player

        // For now it uses same AI as the Standard enemy: this will change so that it intercepts the player
        doPlayer_Chase(AI_Navigation_agent, PlayerAsTarget);
    }
    void Begin_Medic_Behavior_Package()
    {
        // I am the Medic Enemy type
        // I am going to heal my friends

        // AI: find nearest enemy that has less than max health. Go to enemy and heal them up to max.
        // Otherwise, I will charge the player
        // Repeat
        FriendlyTarget = SearchFor_WoundedAlly();
        if (FriendlyTarget == null)
        {
            doPlayer_Chase(AI_Navigation_agent, PlayerAsTarget);
        }
        else
        {
            NavMeshAgent AI_Navigation_agent = GetComponent<NavMeshAgent>();
            doAlly_Chase(AI_Navigation_agent, FriendlyTarget);
        }

    }
    void Begin_Officer_Behavior_Package()
    {
        // I am the Officer Enemy type
        // I am going to buff all my friends

        // AI: Find the highest value teammate, follow them
    }
}
