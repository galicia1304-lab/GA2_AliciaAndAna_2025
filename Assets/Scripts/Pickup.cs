using UnityEngine;
using UnityEngine.UI;
public class Pickup : MonoBehaviour
{
    public RawImage img;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {

            //Debug.Log("pickup");
            Inventory inv = other.transform.GetComponent<Inventory>();

            if(inv.Add(transform.parent.gameObject))
            {
                transform.parent.gameObject.SetActive(false);
                transform.parent.position += Vector3.down * 666;

                img.gameObject.SetActive(true);

            }


        }
    }

    public void hideImage()
    {
        img.gameObject.SetActive(false);
    }
}
