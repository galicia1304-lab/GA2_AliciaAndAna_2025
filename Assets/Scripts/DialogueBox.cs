using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{

    public string text = "Nonesense goes here.";

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            player.GetComponent<PlayerInventory>().dialogueBox.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = text;
            player.GetComponent<PlayerInventory>().dialogueBox.Play("TextDropIn");
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            player.GetComponent<PlayerInventory>().dialogueBox.Play("TextDropOut");
        }
    }

}
