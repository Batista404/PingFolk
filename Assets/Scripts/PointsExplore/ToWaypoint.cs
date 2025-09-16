using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToWaypoint : MonoBehaviour
{
    // Waypoint especifícado
    [SerializeField] GameObject EspecificWP; // Direções +1 = Up, -1 = Down, +2 = Right, -2 = Left
    public int DirectionWP;

    // Transform do waypoint, para conseguir a posição 
    public Vector3 PositionWP;
    private void Awake()
    {
        PositionWP = EspecificWP.transform.position;
    }
}
