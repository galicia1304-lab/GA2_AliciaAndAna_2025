using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;


public class Spaceship : MonoBehaviour
{

    public Transform[] thrusters = new Transform[8];
    public float[] thrustForce = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f } ;

    public float inertia = 1;  //a constant depending on shape of object, just going to assume 1 to start
                               //seems it's just something that is part of the equation

    public Vector3 AngularVelocity;
    

    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity

    public Vector3 impulse = new Vector3(0, 0, 0);              //additional explosive force
    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector
    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame
    
    public float mass = 1.0f;

    public float rectify = 0;

    public bool snapshot = false;
    public Vector3 SnapshotAngularVelocity;
    public Quaternion SnapshotRotation;
    public Vector3 SnapshotVelocity;

    GamepadInput gamepad;
    private enum Thruster
    {
        LEFT = 0,
        RIGHT = 1,
        CENTER = 2,
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamepad = GetComponent<GamepadInput>();
    }


    // Update is called once per frame
    void Update()
    {
        if (rectify >= 1)
        {
            rectify = 0;
        }


        bool joyPressed = false;
        //get stick inputs, rotate thrust 0 and 1 (left right)
        Vector2 left = gamepad.leftStick;
        Vector2 right = gamepad.rightStick;

        //for whatever reason (local global?) xy inverted - I prefer working global
        thrusters[(int)Thruster.LEFT].Rotate(left.y, -left.x, 0);
        thrusters[(int)Thruster.RIGHT].Rotate(right.y, -right.x, 0);
        //may also have something to do with my initial rotations of the prims



        //get current thrust force values and show on yellow spheres (gonna ramp these)
        //thrustForce[(int)Thruster.LEFT] = 0.0f;
        //thrustForce[(int)Thruster.RIGHT] = 0.0f;
        if (gamepad.leftTrigger)
        {
            thrustForce[(int)Thruster.LEFT] += Time.deltaTime;
            if (thrustForce[(int)Thruster.LEFT] > 1.0f)
                thrustForce[(int)Thruster.LEFT] = 1.0f;

            joyPressed = true;
        }
        else
        {
            thrustForce[(int)Thruster.LEFT] -= Time.deltaTime;
            if (thrustForce[(int)Thruster.LEFT] < 0.0f)
                thrustForce[(int)Thruster.LEFT] = 0.0f;
            
        }


        if (gamepad.rightTrigger)
        {
            thrustForce[(int)Thruster.RIGHT] += Time.deltaTime;
            if (thrustForce[(int)Thruster.RIGHT] > 1.0f)
                thrustForce[(int)Thruster.RIGHT] = 1.0f;
            joyPressed = true;
        }
        else
        {
            thrustForce[(int)Thruster.RIGHT] -= Time.deltaTime;
            if (thrustForce[(int)Thruster.RIGHT] < 0.0f)
                thrustForce[(int)Thruster.RIGHT] = 0.0f;
        }


        if(gamepad.anyDpad)
        {
            thrustForce[(int)Thruster.CENTER] += Time.deltaTime;
            if (thrustForce[(int)Thruster.CENTER] > 1.0f)
                thrustForce[(int)Thruster.CENTER] = 1.0f;
            joyPressed = true;
        }
        else
        {
            thrustForce[(int)Thruster.CENTER] -= Time.deltaTime;
            if (thrustForce[(int)Thruster.CENTER] < 0.0f)
                thrustForce[(int)Thruster.CENTER] = 0.0f;
        }

        //No input, let's get a picture of the state at this moment
        if (!joyPressed && !snapshot)
        {
            SnapshotAngularVelocity = AngularVelocity;
            SnapshotRotation = transform.rotation;
            SnapshotVelocity = velocity;
            snapshot = true;
        }
            

        //this is totally dependent on your ship geometry, I'm using prims for thrusters
        //next comes a VFX graph!
        {
            Vector3 scale = thrusters[(int)Thruster.LEFT].GetChild(0).localScale;
            scale.Set(scale.x, scale.y, thrustForce[(int)Thruster.LEFT]);
            thrusters[(int)Thruster.LEFT].GetChild(0).localScale = scale;
        }
        {
            Vector3 scale = thrusters[(int)Thruster.RIGHT].GetChild(0).localScale;
            scale.Set(scale.x, scale.y, thrustForce[(int)Thruster.RIGHT]);
            thrusters[(int)Thruster.RIGHT].GetChild(0).localScale = scale;
        }
        {
            Vector3 scale = thrusters[(int)Thruster.CENTER].GetChild(0).localScale;
            scale.Set(scale.x, scale.y, thrustForce[(int)Thruster.CENTER]);
            thrusters[(int)Thruster.CENTER].GetChild(0).localScale = scale;
        }
        //created scope here, proly will make it into a method

        //do rotation first, translation second
        Vector3 torque = Vector3.zero;

        for(int i = 0; i < thrusters.Length; i++)
        {
            if(thrusters[i])
            {
                Vector3 r = thrusters[i].position - transform.position;                
                torque += Vector3.Cross(r, thrusters[i].forward) * thrustForce[i] ;  //mult by scalar?, I'm just using 1 for now
            }
            
        }


        Vector3 angAcceleration = torque / inertia; //again not sure about this constant

        AngularVelocity += angAcceleration * Time.deltaTime * Time.deltaTime;  //dt squared

        //yeah yeah deprecated, fuck off
        transform.rotation = transform.rotation * Quaternion.EulerAngles(AngularVelocity);
        
                
        //reset final force to the initial force of gravity
        finalForce.Set(0, 0, 0);  //start with a gravity vector if desired

        //accumulate our thrust
        thrust = Vector3.zero;
        for(int i = 0; i < thrusters.Length;i++)
        {
            if (thrusters[i])
            {
                //add our thrust vectors
                thrust += thrusters[i].forward * thrustForce[i];  //mult by scalar, I'm just using 1 for now

            }
        }


        finalForce += thrust;

        acceleration = finalForce / mass;
        velocity += acceleration * Time.deltaTime;
        velocity += impulse;  //if we want an explosion

        //move the object
        transform.position += velocity * Time.deltaTime;

        //impulse is a one time force
        impulse = Vector3.zero;

        if (joyPressed)
            rectify = 0;

        //now try to stabilize
        if ( !joyPressed)
        {

            //lerp out angular velocity
            AngularVelocity = Vector3.Lerp(AngularVelocity,Vector3.zero, Time.deltaTime);

            //lerp in ship facing to match velocity
            // get a point from ship pos to velocity
            Vector3 newfwd = transform.position + velocity * 10.0f ;
            Quaternion curLook = transform.rotation;

            transform.LookAt(newfwd);

            Quaternion newLook = transform.rotation;

            transform.rotation = curLook;

            transform.rotation = Quaternion.Lerp(curLook, newLook, Time.deltaTime);



        }

    }

    /// <summary>Apply angular velocity to the quaternion</summary>
    public Quaternion rotate(Vector3 angularVelocity, Quaternion curQuat)
    {
        Vector3 vec = angularVelocity;// * deltaTime;
        float length = vec.magnitude;
        if (length < 1E-6F)
            return curQuat;    // Otherwise we'll have division by zero when trying to normalize it later on

        // Convert the rotation vector to quaternion. The following 4 lines are very similar to CreateFromAxisAngle method.
        float half = length * 0.5f;
        float sin = MathF.Sin(half);
        float cos = MathF.Cos(half);
        // Instead of normalizing the axis, we multiply W component by the length of it. This method normalizes result in the end.
        Quaternion q = new Quaternion(vec.x * sin, vec.x * sin, vec.z * sin, length * cos);

        q = q * curQuat;
        q.Normalize();

        //if (q.w < 0) q = Quaternion.Inverse(q);
        
        return q;
    }
}
