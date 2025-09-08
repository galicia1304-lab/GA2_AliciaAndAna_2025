using System.Collections.Generic;
using UnityEngine;

public class NPCnameGenerator : MonoBehaviour
{

    public List<string> firstNames;
    public List<string> lastNames;

    //I know I need 4 NPCs

    public string[] fullnames = new string[4];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Random.InitState(1);
        
        for(int i = 0; i < fullnames.Length; i++)
        {

            int f = Random.Range(0, firstNames.Count);
            int l = Random.Range(0, lastNames.Count);


            fullnames[i] = firstNames[f] + " " + lastNames[l];

            firstNames.RemoveAt(f);
            lastNames.RemoveAt(l);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
