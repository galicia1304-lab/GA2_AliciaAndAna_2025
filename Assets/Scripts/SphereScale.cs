using UnityEngine;

public class SphereScale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        float s = Mathf.Sin(Time.time);

        scale.Set(-s, s * 2.0f, s * 1.5f );

        transform.localScale = scale;
        
    }
}
