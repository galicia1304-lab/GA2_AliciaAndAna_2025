using UnityEngine;
using UnityEngine.UI;


public class DoorTrigger : MonoBehaviour
{

    public DoorControllerAdvanced doorhinge;
    public GameObject backdrop;

    public GameObject keyOpen;
    public Inventory playerStuff;

    private bool isNearDoor = false;
    private bool doorIsOpen = false;

    public float timer = -1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        bool openit = false;

        if(isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            for(int i = 0; i < playerStuff.stuff.Length; i++)
            {
                if( playerStuff.stuff[i] == keyOpen)
                {
                    openit = true;
                }
            }

            if(openit)
            {
                doorhinge.OpenDoor();

                //doorhinge.openthedoor = true;
                //get what time we opened the door
                timer = Time.time;

                doorIsOpen = true;

            }
            else
            {
                //tell the player they need the key
            }
            
        }

        //wait until 7 seconds has passed
        if(Time.time - timer > 7.0f  && timer > 0)
        {
            doorhinge.CloseDoor();
            
            //doorhinge.closethedoor = true;
            //reset timer
            timer = -1;

            doorIsOpen = false;

        }

    }
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        //BAD SUSHI
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E) )
        {
            doorhinge.openthedoor = true;
        }
    }
    */

    private void OnTriggerStay(Collider other)
    {
        //GOOD SUSHI
        if (other.tag == "Player" )
        {
            isNearDoor = true;

            Transform plyr = other.transform;

            //cast a ray from the player forward by some distance and see what we hit
            // Bit shift the index of the layer to get a bit mask
            int layerMask = 1 << 8; //doors

            bool didHit = false;
            RaycastHit hit;

            if (Physics.Raycast(plyr.position, plyr.forward, out hit, 10, layerMask))
            {
                Debug.DrawRay(plyr.position, plyr.forward * hit.distance, Color.red);              

                didHit = true;
            }
            else
            {
                didHit = false;

            }

            if(didHit && !doorIsOpen)
            {
                //if hit and it's a door, pop the message
                backdrop.SetActive(true);
               
            }
            else
            {
                //if not, hide the message
                backdrop.SetActive(false);

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //hide the message
            backdrop.SetActive(false);
            //no longer near door
            isNearDoor = false;
        }
    }
}
