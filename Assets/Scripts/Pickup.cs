using UnityEngine;
using UnityEngine.UI;
public class Pickup : MonoBehaviour
{
    public RawImage img;  //this pickup's image in the 2d gui

    public bool allowPickup = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player" && allowPickup)
        {

            Debug.Log("pickup");
            Inventory inv = other.transform.GetComponent<Inventory>();

            //Because the trigger volume is a child of the rigid body
            //we get its parent as the actual object picked up.
            //due to the simple PlayerController not entering the collision volume
            if(inv.Add(transform.parent.gameObject))
            {
                transform.parent.gameObject.SetActive(false);
                transform.parent.position += Vector3.down * 666;

                img.gameObject.SetActive(true); //show the image in the 2d GUI

            }

            allowPickup = false;
        }
    }

    public void hideImage()
    {
        img.gameObject.SetActive(false);
        allowPickup = true;
    }
}
