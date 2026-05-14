using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Comparable_RaycastHit_Class : MonoBehaviour
{
    
}

[System.Serializable]
public class RaycastHit_Comparable : IEquatable<RaycastHit_Comparable> , IComparable<RaycastHit_Comparable>
{
    public RaycastHit TheRaycastHitItself;
    public float Distance_To_Laser_Origin = 0f;

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        RaycastHit_Comparable objAsPart = obj as RaycastHit_Comparable;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }


    public int CompareTo(RaycastHit_Comparable compareDistance)
    {
          // A null value means that this object is greater.
        if (compareDistance == null)
            return 1;

        else
            return this.Distance_To_Laser_Origin.CompareTo(compareDistance.Distance_To_Laser_Origin);
    }

    public bool Equals(RaycastHit_Comparable other)
    {
        if (other == null) return false;
        return (this.Distance_To_Laser_Origin.Equals(other.Distance_To_Laser_Origin));
    }
        
};