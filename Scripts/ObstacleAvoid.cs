using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoid : Seek
{
    float avoidDistance = 3f;
    float lookahead = 5f;

    protected override Vector3 getTargetPosition()
    {
        // Cast a ray and see if there's anything to avoid
        RaycastHit hit;
        
        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead))
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity * hit.distance, Color.yellow, 0.5f);
            Debug.Log("Hit " + hit.collider);
            return hit.point - (hit.normal * avoidDistance);
        }
        else
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity * lookahead, Color.white, 0.5f);
            Debug.Log("No collision");
        }

        return base.getTargetPosition();
    }

}