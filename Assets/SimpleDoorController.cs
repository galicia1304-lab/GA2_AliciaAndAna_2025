using UnityEngine;

public class SimpleDoorController : MonoBehaviour
{
    public Transform hinge;        
    public float openAngle = 90f;  
    public float speed = 200f;     

    float current;   
    float target;    

    public bool IsOpen => Mathf.Approximately(target, openAngle);

    void Update()
    {
        if (Mathf.Approximately(current, target)) return;

        float next = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
        float delta = next - current;

        // rotate around hinge pivot
        transform.RotateAround(hinge.position, Vector3.up, delta);

        current = next;
    }

    public void OpenDoor() { target = openAngle; }
    public void CloseDoor() { target = 0f; }
    public void ToggleDoor() { target = IsOpen ? 0f : openAngle; }
}
