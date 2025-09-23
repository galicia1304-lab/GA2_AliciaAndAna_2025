using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {

        Transform camT = Camera.main.transform;
        if (transform.parent == null)
        {
            transform.position = camT.position + camT.forward * 2.0f;
            transform.LookAt(camT);
        }
    }
}
