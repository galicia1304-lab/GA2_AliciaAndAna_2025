using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    public Animation dialogueBox;
    public GameObject coinText;
    public GameObject interactiveText;
    public Image blackFade;
    public Transform spinner; 
    
    public int coinCount = 0;

    public Sprite keySprite;

    public bool redKey = false;
    public Color redColor;
    public bool blueKey = false;
    public Color blueColor;
    public bool yellowKey = false;
    public Color yellowColor;
    public GameObject inventoryUI;

    private Image inventory00;
    private Image inventory01;
    private Image inventory02;

    private void Start()
    {
        inventory00 = inventoryUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        inventory01 = inventoryUI.transform.GetChild(1).gameObject.GetComponent<Image>();
        inventory02 = inventoryUI.transform.GetChild(2).gameObject.GetComponent<Image>();
    }

    public void SpriteColorLoad(Image childUI, Color chosenColor)
    {
        childUI.sprite = keySprite;
        childUI.color = chosenColor;
    }


    public void PickUpKey(string key)
    {
        Color keyColor = Color.black;
        if (key == "Red")
        {
            keyColor = redColor;
        }
        if (key == "Blue")
        {
            keyColor = blueColor;
        }
        if (key == "Yellow")
        {
            keyColor = yellowColor;
        }

        if (!inventory00.sprite)
        {
            SpriteColorLoad(inventory00, keyColor);
        }
        else if (!inventory01.sprite)
        {
            SpriteColorLoad(inventory01, keyColor);
        }
        else if (!inventory02.sprite)
        {
            SpriteColorLoad(inventory02, keyColor);
        }  
    }
    //could have more here, e.g. items etc. 
}
