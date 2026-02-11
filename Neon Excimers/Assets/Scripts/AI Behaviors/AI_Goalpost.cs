using UnityEngine;
using System;
using System.Collections.Generic;
// An AI goalpost is a way to test for AI moving to a given location.
// Goalposts have an index, the AI will move to the goalpost that has an index + 1 that it is at.
// Indexes begin at 1

// All goalposts are stored globally
public class AI_Goalpost : MonoBehaviour
{
    public List<UnityEngine.GameObject> goalposts = new List<UnityEngine.GameObject>();
    [SerializeField] private int Goalpost_Index = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goalposts.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
