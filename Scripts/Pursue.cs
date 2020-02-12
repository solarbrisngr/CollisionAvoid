using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{

    float maxPrediction;

    Kinematic target;


    public override SteeringOutput getSteering()
    {
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        float speed = character.linearVelocity.magnitude;

        float prediction;

        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        target.transform.position += target.linearVelocity * prediction;

        return getSteering();
    }
}
