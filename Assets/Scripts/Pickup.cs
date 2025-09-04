using UnityEngine;

public class Pickup : MonoBehaviour
{
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
        
        if(other.tag == "Player")
        {

            Debug.Log("pickup");
            Inventory inv = other.transform.GetComponent<Inventory>();

            if(inv.Add(transform.gameObject))
            {
                transform.gameObject.SetActive(false);
                transform.position += Vector3.down * 666;
            }


        }
    }
}
