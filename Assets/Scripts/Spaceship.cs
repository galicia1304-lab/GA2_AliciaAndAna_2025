using System;
using UnityEngine;


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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }


    // Update is called once per frame
    void Update()
    {

        
        //I generally do rotation first, translation second
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
        
        
        //transform.rotation = Quaternion.Slerp(transform.rotation, quat, 1.0f); //this does the dt squared

        //Quaternion quat  = rotate(AngularVelocity, transform.rotation);
        //transform.rotation = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime); //this does the dt squared

        //reset final force to the initial force of gravity
        finalForce.Set(0, 0, 0);  //start with a gravity vector if desired


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
