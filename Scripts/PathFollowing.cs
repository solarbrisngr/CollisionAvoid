using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : Seek
{
    public GameObject[] path;
    float targetRadius = 1f;
    int currentPathIndex;

    public override SteeringOutput getSteering()
    {
        if (target == null)
        {
            currentPathIndex = 0;
            target = path[currentPathIndex];
        }

        Vector3 vectorToTarget = target.transform.position - character.transform.position;
        float distanceToTarget = vectorToTarget.magnitude;

        if (distanceToTarget < targetRadius)
        {
            currentPathIndex++;
            if (currentPathIndex > path.Length - 1)
            {
                currentPathIndex = 0;
            }
        }

        target = path[currentPathIndex];
        return base.getSteering();
    }
}
