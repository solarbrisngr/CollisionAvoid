using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourInterface
{
    public enum SteeringBehaviour
    {
        Seek,
        Flee,
        Align,
        Arrive,
        Face,
        Look,
        Pursue,
        PathFollow,
        ObstacleAvoid,
        CollisionAvoid
    }
}
