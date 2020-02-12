using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;  // Millington calls this rotation
    // because I'm attached to a gameobject, we also have:
    // rotation <<< Millington calls this orientation
    // position
    

    // An empty array that gets used later to create the drop down menus in the inspector
    public BehaviourInterface.SteeringBehaviour[] behaviourSwitch = new BehaviourInterface.SteeringBehaviour[2];

    // Finds the current player object in the seen
    public GameObject myTarget;

    public SteeringOutput steering = new SteeringOutput();

    public Kinematic[] targets;

    // Functions to create different Steering Behaviours
    public void createSeek(bool toFleeOrNotToFlee, SteeringOutput steering)
    {
        Seek mySeek = new Seek
        {
            character = this,
            target = myTarget
        };
        mySeek.flee = toFleeOrNotToFlee;
        steering = mySeek.getSteering();
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;
    }
    public void createAlign(SteeringOutput steering)
    {
        Align myAlign = new Align
        {
            character = this,
            target = myTarget
        };
        steering = myAlign.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
    }
    public void createArrive(SteeringOutput steering)
    {
        Arrive myArrive = new Arrive
        {
            character = this,
            target = myTarget
        };
        steering = myArrive.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        else
        {
            linearVelocity = Vector3.zero;
        }
    }
    public void createFace(SteeringOutput steering)
    {
        Face myAlign = new Face
        {
            character = this,
            target = myTarget
        };
        steering = myAlign.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
    }
    public void createLook(SteeringOutput steering)
    {
        LookWhereYoureGoing myAlign = new LookWhereYoureGoing
        {
            character = this,
            target = myTarget
        };
        steering = myAlign.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
    }
    public void createPursue(SteeringOutput steering)
    {
        Pursue myPursue = new Pursue
        {
            character = this,
            target = myTarget
        };
        steering = myPursue.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
    }
    public void createPathFollow(SteeringOutput steering)
    {
        PathFollowing myPath = new PathFollowing
        {
            character = this,
            target = myTarget
        };
        steering = myPath.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
    }

    public void createObstacleAvoid(SteeringOutput steering)
    {
        ObstacleAvoid myObstacleAvoid = new ObstacleAvoid
        {
            character = this,
            target = myTarget
        };
        steering = myObstacleAvoid.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
    }

    public void createCollisionAvoid(SteeringOutput steering)
    {
        CollisionAvoid myCollisionAvoid = new CollisionAvoid
        {
            character = this,
            //targets = new Kinematic[3]
        };
        steering = myCollisionAvoid.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
    }

    public void useBehaviour(BehaviourInterface.SteeringBehaviour behaviour)
    {
        //This switch statement that sets what steering behaviour the player object has
        switch (behaviour)
        {
            case BehaviourInterface.SteeringBehaviour.Seek:
                createSeek(false, steering);
                break;
            case BehaviourInterface.SteeringBehaviour.Flee:
                createSeek(true, steering);
                break;
            case BehaviourInterface.SteeringBehaviour.Align:
                createAlign(steering);
                break;
            case BehaviourInterface.SteeringBehaviour.Arrive:
                createArrive(steering);
                break;
            case BehaviourInterface.SteeringBehaviour.Face:
                createFace(steering);
                break;
            case BehaviourInterface.SteeringBehaviour.Look:
                createLook(steering);
                break;
            case BehaviourInterface.SteeringBehaviour.PathFollow:
                createPathFollow(steering);
                break;
            case BehaviourInterface.SteeringBehaviour.Pursue:
                createPursue(steering);
                break;
            case BehaviourInterface.SteeringBehaviour.ObstacleAvoid:
                createObstacleAvoid(steering);
                break;
            case BehaviourInterface.SteeringBehaviour.CollisionAvoid:
                createCollisionAvoid(steering);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myTarget = GameObject.Find("Player");
        // update my position and rotation
        this.transform.position += linearVelocity * Time.deltaTime;
        Vector3 v = new Vector3(0, angularVelocity, 0); // TODO - don't make a new Vector3 every update you silly person
        this.transform.eulerAngles += v * Time.deltaTime;

        //This sets each behaviour while the game is playing to use whatever behaviour it is set to
        foreach (BehaviourInterface.SteeringBehaviour behaviour in behaviourSwitch)
        {
            useBehaviour(behaviour);
        }
    }
}
