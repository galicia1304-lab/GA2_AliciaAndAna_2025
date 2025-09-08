using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject[] stuff;
    private int nextSlot = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject gobj = stuff[0];
            gobj.SetActive(true);
            gobj.transform.position = transform.position + transform.forward * 2.0f + Vector3.up * 2;
            gobj.transform.GetChild(0).GetComponent<Pickup>().hideImage();

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject gobj = stuff[1];
            gobj.SetActive(true);
            gobj.transform.position = transform.position + transform.forward * 2.0f + Vector3.up * 2;
            gobj.transform.GetChild(0).GetComponent<Pickup>().hideImage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject gobj = stuff[2];
            gobj.SetActive(true);
            gobj.transform.position = transform.position + transform.forward * 2.0f + Vector3.up * 2;
            gobj.transform.GetChild(0).GetComponent<Pickup>().hideImage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject gobj = stuff[3];
            gobj.SetActive(true);
            gobj.transform.position = transform.position + transform.forward * 2.0f + Vector3.up * 2;
            gobj.transform.GetChild(0).GetComponent<Pickup>().hideImage();
        }
    }

    public bool Add(GameObject obj)
    {
        if(nextSlot < stuff.Length)
        {
            stuff[nextSlot] = obj;
            nextSlot++;
            return true;
        }
        else
        {
            Debug.Log("no slots");
            return false;
        }
        
    }
}
