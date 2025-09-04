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
