using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoid
{
    public Kinematic character;
    public float maxAcceleration = 1f;

    // A list of targets
    public Kinematic[] targets;

    // The collision radius of a character
    float radius = .1f; // .1

    void Start()
    {
        GameObject[] theThingsToAvoid = GameObject.FindGameObjectsWithTag("AvoidThis");
        targets = new Kinematic[theThingsToAvoid.Length - 1];
        int j = 0;
        for (int i = 0; i < theThingsToAvoid.Length - 1; i++)
        {
            //if (theThingsToAvoid[i] == this)
            //{
            //    continue;
            //}
            targets[j++] = theThingsToAvoid[i].GetComponent<Kinematic>();
        }

    }

    public SteeringOutput getSteering()
    {
        // Find the target that's closes to collision
        float shortestTime = float.PositiveInfinity;

        // Stores some collision data that we need to be able to avoid it
        Kinematic firstTarget = null;
        float firstMinSeparation = float.PositiveInfinity;
        float firstDistance = float.PositiveInfinity;
        Vector3 firstRelativePos = Vector3.positiveInfinity;
        Vector3 firstRelativeVel = Vector3.zero;

        Vector3 relativePos = Vector3.positiveInfinity;
        foreach (Kinematic target in targets)
        {
            // calculate the time to collision
            relativePos = target.transform.position - character.transform.position;
            // the next line is a bug in Millington
            // the fix is on the following line // Copied from bslease for a fix
            //Vector3 relativeVel = target.linearVelocity - character.linearVelocity;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = (Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed));

            // Is there actually going to be a collision
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2 * radius)
            {
                continue;
            }

            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                // store the time, target and other data
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        // if we have no target, then exit
        if (firstTarget == null)
        {
            return null;
        }

        //if (firstMinSeparation <= 0 || firstDistance < 2*radius)
        //{
        //    relativePos = firstTarget.transform.position - character.transform.position;
        //}
        //else // otherwise cacluate the future relative position
        //{
        //    relativePos = firstRelativePos + firstRelativeVel * shortestTime;
        //}

        // Avoid the target.
        //SteeringOutput result = new SteeringOutput();
        //relativePos.Normalize();
        //result.linear = relativePos * maxAcceleration;
        //result.angular = 0;
        //return result;

        // Also copied from bslease for a fix that worked to the above commented block

        SteeringOutput result = new SteeringOutput();

        // is the collision head-on
        float dotResult = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);
        if (dotResult < -0.9)
        {
            // veer sideways
            result.linear = -firstTarget.transform.right;
        }
        else
        {
            // steer to pass behind our moving target
            result.linear = -firstTarget.linearVelocity;
        }
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;
        return result;
    }
}
