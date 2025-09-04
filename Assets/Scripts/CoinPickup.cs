using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour
{

    private int coinValue = 0;

    public GameObject coinCollect;
    public int coinMaxValue = 8;
    public int coinMinValue = 5;

    private void OnTriggerEnter(Collider player) //walk into coin - triggerenter
    {
        if (player.CompareTag("Player"))
        {
            Instantiate(coinCollect, transform.position, Quaternion.Euler(-90,0,0));
            Destroy(gameObject, 0.2f); //coin disappears
            coinValue = Random.Range(coinMinValue, coinMaxValue);
            print(coinValue);
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
            playerInventory.coinCount += coinValue;
            playerInventory.coinText.GetComponent<Text>().text = "Coins: " + playerInventory.coinCount;
        }
    }
}
