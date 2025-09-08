using System;
using UnityEngine;


public class LanderPhysics : MonoBehaviour
{

    //FEEL FREE TO MODIFY THIS SCRIPT, IT IS SIMPLY A SOLID PLACE TO START

    public float inertia = 1;  //a constant depending on shape of object, just going to assume 1 to start
                               //seems it's just something that is part of the equation, 1 works fine

    public Vector3 AngularVelocity;  //how much we are going to spin (if at all)

    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //increase of movement over time, by player input and gravity

    public Vector3 impulse = new Vector3(0, 0, 0);              //additional explosive force
    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector, part of acceleration
    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame
    
    public float mass = 1.0f;            //a constant that doesn't really matter in this case
    public float GRAVITY = -1.6f;        // -9.8 for earth,  -1.6 for moon, Mars? Jupiter? 

    enum Thruster
    {
        LEFT,
        RIGHT,
        FRONT,
        BACK,
        CENTER,
    }
    //pre-allocated 5 thrusters, to be placed on the craft where you see fit (have fun!)
    //you may not need all of them, depending on how you style your game. A single
    //center thruster, with angular control can be enough to move in 3d space
    public Transform[] thrusters = new Transform[5];            //locations
    public float[] thrustForce = new float[] { 0,0,0,0,0 };     //current force

    //You will need to create an input mapping as you see fit. WASD and Space should be enough
    //You will also need to make a camera of some sort
    //You CAN use a rigid body to handle box triggers and collisions but you CAN NOT use it for propulsion
    //You will need to create a 2 GUI indicating fuel consumption
    //Your ship has limited fuel, so the player may consume it all, and crash
    //There should be a cut-off velocity where the player is successful if they land gently

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {

        //do rotation first, translation second

        //torque, or angular acceleration, or spin, same thing
        Vector3 torque = Vector3.zero;


        //HINT if you do not want any spin, put the thrusters
        //at the center of mass (center of the lander object)
        for(int i = 0; i < thrusters.Length; i++)
        {
            if(thrusters[i])
            {
                //get offset vector from center of mass
                Vector3 r = thrusters[i].position - transform.position;
                //mult by current thrust force
                torque += Vector3.Cross(r, thrusters[i].forward) * thrustForce[i] ;  
            }
            
        }


        Vector3 angAcceleration = torque / inertia; //this constant can simply be 1

        AngularVelocity += angAcceleration * Time.deltaTime * Time.deltaTime;  //dt squared

        //yeah yeah deprecated, but we are working in radians at the moment
        transform.rotation = transform.rotation * Quaternion.EulerAngles(AngularVelocity);
        
       
        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY, 0);
        
        
        
        //reset thrust such that a thrust on center thruster, opposite to gravity
        //would hold the ship level, counteracting gravity 
        thrust.Set(0, 0, 0);


        for(int i = 0; i < thrusters.Length;i++)
        {
            if (thrusters[i])
            {
                //add our thrust vectors, mult by scalar (0 would be no thrust on a thruster)
                thrust += thrusters[i].forward * thrustForce[i];  
                //this is applying a force IN THE DIRECTION of the vector (blue axis)
                //thus for an upward force, the blue axis of the thruster should be pointing up
                //however, your particle effect, or however you show the thrust
                //would be pointing down
                
            }
        }


        finalForce += thrust;

        acceleration = finalForce / mass;
        velocity += acceleration * Time.deltaTime;

        //if we want an explosion that instantly pushes the lander.
        //zero for no impulse. impulse happens once
        velocity += impulse;  

        //move the object
        transform.position += velocity * Time.deltaTime;

        //impulse is a one time force, so zero it
        impulse = Vector3.zero;


    }

    
}
