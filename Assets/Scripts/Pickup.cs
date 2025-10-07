using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Image img;  // this pickup's image in the 2D GUI
    public bool allowPickup = true;

    // ADDED: sound fields 
    [Header("Pickup Sound Settings")]
    public AudioClip pickupClip;
    [Range(0f, 1f)] public float volume = 1f;
   

    void Start() { }
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && allowPickup)
        {
            Debug.Log("pickup");
            Inventory inv = other.transform.GetComponent<Inventory>();

            // trigger is a child; parent is the actual item
            if (inv != null && inv.Add(transform.parent.gameObject))
            {
                // play sound FIRST using a temp source that survives deactivation
                PlayPickupSound();

                // then hide/move the 3D object
                transform.parent.gameObject.SetActive(false);
                transform.parent.position += Vector3.down * 666;

                // show the image in the 2D GUI
                if (img != null) img.gameObject.SetActive(true);
            }

            allowPickup = false;
        }
    }

    public void AllowPickup()
    {
        allowPickup = true;
    }

    // ADDED: sound playback that won't be cut off 
    private void PlayPickupSound()
    {
        if (pickupClip == null) return;
        AudioSource.PlayClipAtPoint(pickupClip, transform.position, volume);
    }
    
}
