using UnityEngine;

public class SimpleDoorTrigger : MonoBehaviour
{
    public SimpleDoorController door; 
    public GameObject prompt;         
    public LayerMask doorMask = 1 << 9; 

    bool near = false;
    float timer = -1f;

    void Start()
    {
        if (prompt) prompt.SetActive(false); // hidden on start
    }

    void Update()
    {
        // safety: if not near, keep the prompt hidden
        if (!near && prompt && prompt.activeSelf) prompt.SetActive(false);

        if (near && Input.GetKeyDown(KeyCode.E))
        {
            if (!door.IsOpen)
            {
                door.OpenDoor();
                timer = Time.time;
            }
            else
            {
                door.CloseDoor();
                timer = -1f;
            }
        }

        // auto-close after 7s
        if (door.IsOpen && timer > 0f && Time.time - timer > 7f)
        {
            door.CloseDoor();
            timer = -1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            near = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // show only when looking at the door
        Transform t = other.transform;
        bool lookingAtDoor = Physics.Raycast(t.position, t.forward, out _, 10f, doorMask);

        if (prompt) prompt.SetActive(lookingAtDoor && !door.IsOpen);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        near = false;
        if (prompt) prompt.SetActive(false);
    }
}
