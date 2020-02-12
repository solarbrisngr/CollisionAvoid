using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperation : MonoBehaviour
{

    public Kinematic character;
    float maxAcceleration;

    public Kinematic[] targets;

    float threshold;

    float decayCoefficient;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        foreach (Kinematic target in targets)
        {
            Vector3 direction = target.transform.position - character.transform.position;
            float distance = direction.magnitude;

            if (distance < threshold)
            {
                float strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);

                direction.Normalize();
                result.linear += strength * direction;
            }
        }
        return result;
    }
}
