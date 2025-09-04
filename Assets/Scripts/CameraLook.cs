using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public Transform target;
    public float distance = 5.5f;
    public float height = 3.5f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame

    void LateUpdate()
    {
        //move camera to player
        transform.position = target.position - target.forward * distance + target.up * height;
        //look at player
        transform.LookAt(target);
    }
}
