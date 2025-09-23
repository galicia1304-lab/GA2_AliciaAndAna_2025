using UnityEngine;

public class DrawerTrigger : MonoBehaviour
{
    public GameObject key;
    public DrawerController drawerController;
    
    private Transform player;
    private bool hasKey = false;
    private bool hasPaper = false;

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
        if (other.tag == "Player")
        {
            player = other.transform;

           

            Inventory inv = player.GetComponent<Inventory>();
            for(int i = 0; i < inv.stuff.Length; i++)
            {
                if (inv.stuff[i] == key)
                {
                    hasKey = true;
                }

            }

            if(hasKey)
            {
                drawerController.OpenDrawer();         
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            player = other.transform;

            //cast a ray from the player forward by some distance and see what we hit
            // Bit shift the index of the layer to get a bit mask
            int layerMask = 1 << 11; //paper

            bool didHit = false;
            RaycastHit hit;

            Transform cam = Camera.main.transform;

            if (Physics.Raycast(cam.position, cam.forward, out hit, 20, layerMask))
            {
                Debug.DrawRay(cam.position, cam.forward * hit.distance, Color.red);

                didHit = true;
            }
            else
            {
                didHit = false;

            }
        
            if(didHit)
            {
                

                if(drawerController.isDrawerOpen() && !hasPaper)
                {
                    hasPaper = true;
                    Debug.Log("Hit Paper");
                    Transform paper = hit.transform;

                    paper.parent = null;

                   

                }
            }

        }

    }

}
