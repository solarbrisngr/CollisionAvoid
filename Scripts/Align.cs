using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align
{

    public Kinematic character; // pulibc? what's the matter with you?
    public GameObject target; // shut it I'm just trying to get this working ye great ape

    float maxAngularAcceleration = 50f;
    float maxRotation = 10f;

    float targetRadius = 1.5f;

    float targetRotation = 50f;

    float slowRadius = 3f;

    float timeToTarget = .1f;

    public virtual float getTargetAngle()
    {
        return target.transform.eulerAngles.y;
    }

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        result.angular = target.transform.rotation.y - character.angularVelocity;

        result.angular = Mathf.DeltaAngle(character.transform.eulerAngles.y, getTargetAngle());
        float rotationSize = Mathf.Abs(result.angular);

        if (rotationSize < targetRadius)
        {
            return null;
        }

        if (rotationSize > slowRadius)
        {
            targetRotation = maxRotation;
        }
        else
        {
            targetRotation = maxRotation * (rotationSize / slowRadius);
        }

        targetRotation *= result.angular / rotationSize;

        result.angular = targetRotation - character.angularVelocity;

        result.angular /= timeToTarget;

        float angularAcceleration = Mathf.Abs(result.angular);
        if (angularAcceleration > maxAngularAcceleration)
        {
            result.angular /= angularAcceleration;
            result.angular *= maxAngularAcceleration;
        }

        result.linear = Vector3.zero;
        return result;

    }
}