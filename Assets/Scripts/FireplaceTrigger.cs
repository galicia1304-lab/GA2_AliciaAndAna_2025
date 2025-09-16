using UnityEngine;
using UnityEngine.UI;

public class FireplaceTrigger : MonoBehaviour
{
    public ParticleSystem[] flames;
    public GameObject backdrop;
    public Text message;
    public AudioSource fireaudio;
    public Light firelight;


    private bool nearFireplace = false;
    private bool ramptdown = false;

    private void Update()
    {
        if (nearFireplace && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Stop fire");
            flames[1].Stop();
            ramptdown = true;

        }
        if (ramptdown)
        {
            if (fireaudio.volume > 0)
            {
                fireaudio.volume -= Time.deltaTime * 0.1f;
            }
            else
            {
                fireaudio.Stop();
                ramptdown = false;
            }
            if (firelight.intensity > 0)
            {
                firelight.intensity -= Time.deltaTime * 5.0f;
            }
            
        }
    }     


    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            //To test if the trigger is working
            //Debug.Log("in fireplace");

            Transform plyr = other.transform;

            //cast a ray from the player forward by some distance and see what we hit
            // Bit shift the index of the layer to get a bit mask
            int layerMask = 1 << 8; //fireplace or other "interactable"

            bool didHit = false;
            RaycastHit hit;

            if (Physics.Raycast(plyr.position, plyr.forward, out hit, 100, layerMask))
            {
                Debug.DrawRay(plyr.position, plyr.forward * hit.distance, Color.red);

                didHit = true;
            }
            else
            {
                didHit = false;

            }

            if (didHit )
            {
                //if hit and it's the logholder, pop the message
                message.text = "press E to put out the fire";
                backdrop.SetActive(true);
                nearFireplace = true;

            }
            else
            {
                //if not, hide the message
                backdrop.SetActive(false);

            }

            nearFireplace = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //hide the message
            backdrop.SetActive(false);
            nearFireplace = false;

        }
    }
}
