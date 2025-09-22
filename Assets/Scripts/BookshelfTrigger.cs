using UnityEngine;
using UnityEngine.UI;


public class BookshelfTrigger : MonoBehaviour
{

    
    public GameObject backdrop;

    private bool isNearDoor = false;
    private bool doorIsOpen = false;

    public float timer = -1;
    public Transform theBook;
    public bool moveTheBook = false;
    public bool finalizeTheBook = false;
    public Transform player;
    public Transform target;
    public Animator doorHinge;

    float t = 0;
    Quaternion rot1;
    Quaternion rot2;
    Vector3 pos1;
    Vector3 pos2;
    float bookMoveSpeed = 0.5f;
    void Update()
    {
               
        //this is what is called a finite state machine, or FSM
        if(isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            //move the book
            theBook.gameObject.SetActive(true);
            theBook.position = player.position + Vector3.up + player.right;
            moveTheBook = true;

            //get current rotation/pos           
            rot1 = theBook.rotation;
            pos1 = theBook.position;
            
            //get final rotation/pos
            rot2 = target.rotation;
            pos2 = target.position;

            //begin counting
            t = 0;
        }

        if(moveTheBook)
        {
            
            //lerp the rotation/position the T way
            theBook.rotation = Quaternion.Lerp(rot1, rot2, t);
            theBook.position = Vector3.Lerp(pos1, pos2, t);

            //lerp the rotation/position the dt way
            //theBook.rotation = Quaternion.Lerp(theBook.rotation, rot2, Time.deltaTime * bookMoveSpeed);          
            //theBook.position = Vector3.Lerp(theBook.position, pos2, Time.deltaTime * bookMoveSpeed);

            t += Time.deltaTime * bookMoveSpeed; 
            if (t > 1)
            {
                t = 0;
                finalizeTheBook = true;
                moveTheBook = false;

            }
        }

        if (finalizeTheBook)
        {

            t += Time.deltaTime * bookMoveSpeed;
            if(t >= 1)
            {
                finalizeTheBook = false;

                //child the book to the shelf
                theBook.parent = target;

                Debug.Log("Open the door");
                doorHinge.SetTrigger("OpenDoor");
            }
            else
            {
                //i know the book is inline with the target, so just push the book forward into the shelf
                theBook.Translate(theBook.forward * Time.deltaTime * bookMoveSpeed, Space.World);

            }
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
                player = other.transform;
               
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
