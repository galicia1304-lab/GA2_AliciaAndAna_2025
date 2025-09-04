using UnityEngine;
using UnityEngine.VFX;

public class DoorControllerAdvanced : MonoBehaviour
{

    private Animator doorAnim;
    private AudioSource audio;
    private VisualEffect vfx;

    //for the polling aproach (see method aproach below)
    //public bool openthedoor = false;
    //public bool closethedoor = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAnim = transform.GetComponent<Animator>();
        audio = transform.GetComponent<AudioSource>();
        vfx = transform.GetComponent<VisualEffect>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //example of using polling for bools
        if(openthedoor)
        {

            //tell animator to open door
            doorAnim.SetTrigger("OpenDoor");
            
            openthedoor = false;
        }

        if(closethedoor)
        {
            //tell animator to close door
            doorAnim.SetTrigger("CloseDoor");
            closethedoor = false;
        }
        */
    }

    public void OpenDoor()
    {
        doorAnim.SetTrigger("OpenDoor");
        audio.Play();
        vfx.Play();
    }

    public void CloseDoor()
    {
        doorAnim.SetTrigger("CloseDoor");
        audio.Stop();
        vfx.Stop();
    }

}
