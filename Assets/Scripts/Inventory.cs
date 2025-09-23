using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject[] stuff;
   // public RawImage[] slots;
    private int nextSlot = 0;

    private int[] placeFromSlot = { -1, -1, -1, -1, -1, -1, -1, -1 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    int itemtoPlace = 0;
    public void placeItem(GameObject obj)
    {
        for (int i = 0; i < stuff.Length; i++)
        {
            if (obj == stuff[i])
            {
                Debug.Log("I got the one I want");
                placeFromSlot[itemtoPlace] = i;
                itemtoPlace++;
            }


        }
    }

    // Update is called once per frame
    void Update()
    {

        //handle from keypress
        for (int i = 0; i < stuff.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                GameObject gobj = stuff[i];
                if (stuff[i] != null)
                {
                    gobj.SetActive(true);
                    gobj.transform.position = transform.position + transform.forward * 2.0f + Vector3.up * 2;
                    gobj.transform.GetChild(0).GetComponent<Pickup>().AllowPickup();

                    ReorderInventory(i);

                }

            }
        }

        for (int i = 0; i < placeFromSlot.Length; i++)
        {
            if (placeFromSlot[i] >= 0)
            {


                GameObject gobj = stuff[placeFromSlot[i]];
                if (stuff[placeFromSlot[i]] != null)
                {
                    gobj.SetActive(true);
                    gobj.transform.position = transform.position + transform.forward * 2.0f + Vector3.up * 2;

                    ReorderInventory(placeFromSlot[i]);

                }

                placeFromSlot[i] = -1;

            }

        }

    }

    void ReorderInventory(int index)
    {


        for (int i = index; i < stuff.Length - 1; i++)
        {
            stuff[i] = stuff[i + 1];
            //slots[i].texture = slots[i + 1].texture;

            if (stuff[i] == null)
            {
                //slots[i].enabled = false;
            }
        }

        for (int i = index; i < stuff.Length; i++)
        {
            if (stuff[i] == null)
            {
                nextSlot = i;
                return;
            }
        }

    }

    public bool Add(GameObject obj)
    {
        if (nextSlot < stuff.Length)
        {
            stuff[nextSlot] = obj;
            //slots[nextSlot].gameObject.SetActive(true);
            //slots[nextSlot].texture = obj.transform.GetChild(0).GetComponent<Pickup>().img;
            //slots[nextSlot].enabled = true;
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