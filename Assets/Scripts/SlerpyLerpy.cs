using UnityEngine;

public class SlerpyLerpy : MonoBehaviour
{

    public Transform target;
    public float speed = 0.5f;
    public bool slerporlerp = false;

    public Vector3 startPos;
    public Quaternion startRot;
    public float t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if(false)
        {
            transform.position = Vector3.Lerp(startPos, target.position, t);
            transform.LookAt(target);

            t += Time.deltaTime * speed;
            if (t >= 1.0f)
                t = 1.0f;
        }

        Vector3 targetRot = new Vector3(0, -90, 0);
        Quaternion targQuat = Quaternion.Euler(targetRot);

        if(slerporlerp)
            transform.rotation = Quaternion.Slerp(startRot, targQuat, t);   
        else
            transform.rotation = Quaternion.Lerp(startRot, targQuat, t);

        t += Time.deltaTime * speed;
        if (t >= 1.0f)
            t = 1.0f;
    }
}
