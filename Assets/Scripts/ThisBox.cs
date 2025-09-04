using UnityEngine;
using UnityEngine.UI;

public class ThisBox : MonoBehaviour
{  
    public Text storyText;          //the 2d gui test
     
    public Transform nextDialog;    //the next transform in the tree

    bool hasTouched = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //we start with the "root" top of the tre, in this case named "TheStory"
        nextDialog = transform;
    }

    // Update is called once per frame
    void Update()
    {

        //check for single key press
        if (Input.GetKeyDown(KeyCode.Space) || hasTouched )
        {
            hasTouched = false;

            //generate a random, and choose the child (0 or 1)
            int child = Random.Range(0, nextDialog.childCount);
            
            //get this transform's child at child index
            nextDialog = nextDialog.GetChild(child);
            
            //get and show the dialog text
            Dialog dia = nextDialog.GetComponent<Dialog>();
            storyText.text = dia.storyline;

            //if we have no more children, start again
            if(nextDialog.childCount == 0)
            {
                nextDialog = transform;  //set it back to root
            }

           

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hasTouched = true;
        }
    }
}
