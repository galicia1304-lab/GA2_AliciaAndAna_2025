using UnityEngine;

public class DropPaper : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;

    public bool isHeld = false;
   
    void Start()
    {
        // Save where the paper starts
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        originalParent = transform.parent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) )
        {
            // Put paper back in drawer
            transform.SetParent(originalParent);
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            isHeld = false;

        }
    }
}
