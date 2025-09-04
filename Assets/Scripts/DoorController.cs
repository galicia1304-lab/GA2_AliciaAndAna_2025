using UnityEngine;

public class DoorController : MonoBehaviour
{

    public bool openthedoor = false;
   
    
    private Animation anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = transform.GetComponent<Animation>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (openthedoor)
        {
            anim.Play();
            openthedoor = false;
            this.enabled = false;
        } 
    }
}
