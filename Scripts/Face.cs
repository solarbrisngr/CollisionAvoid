using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{

    public override float getTargetAngle()
    {
        Vector3 direction = target.transform.position - character.transform.position;
        float targetAngle = Mathf.Atan2(-direction.x, direction.z);
        targetAngle *= Mathf.Rad2Deg;
        return targetAngle;
    }

}