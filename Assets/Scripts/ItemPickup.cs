using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool redKey = false;
    public bool blueKey = false;
    public bool yellowKey = false;

    //when colliding, disappear, and add to inventory 
    private void OnTriggerEnter(Collider player) //walk into key - triggerenter
    {
        if (player.CompareTag("Player"))
        {
            
            Destroy(gameObject, 0.2f); 
            
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
            if (redKey == true)
            {
                playerInventory.redKey = true;
                playerInventory.PickUpKey("Red");
            }
            if (blueKey == true)
            {
                playerInventory.blueKey = true;
                playerInventory.PickUpKey("Blue");
            }
            if (yellowKey == true)
            {
                playerInventory.yellowKey = true;
                playerInventory.PickUpKey("Yellow");
            }
            
            //check if UI is full? 
            //playerInventory.coinText.GetComponent<Text>().text = "Coins: " + playerInventory.coinCount;
        }
    }

}
