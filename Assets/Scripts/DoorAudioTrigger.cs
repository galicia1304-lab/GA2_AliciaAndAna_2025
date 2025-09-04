using UnityEngine;

public class DoorAudioTrigger : MonoBehaviour
{

    private AudioSource audio;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audio.Play();
        }
    }

}
