using UnityEngine;

public class PaperPickup : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;

    private bool isHeld = false;
    public Transform holdPoint; // Empty GameObject in front of the player where paper will appear

    void Start()
    {
        // Save where the paper starts
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalParent = transform.parent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Put paper back in drawer
            transform.SetParent(originalParent);
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            isHeld = false;

        }
    }
}
