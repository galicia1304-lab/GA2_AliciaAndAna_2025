using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    public int treeCount = 30;
    public GameObject treePrefab;
    void Awake()
    {
        Random.InitState(124);

        for(int i = 0; i < treeCount; i++)
        {
            float x = Random.Range(-14.0f, 14.0f);
            float z = Random.Range(-14.0f, 14.0f);
            float y = 100.0f;

            GameObject tree = GameObject.Instantiate(treePrefab);

            tree.transform.position = new Vector3(x, y, z);
            tree.SetActive(true);

            Transform treat = tree.transform;

            treat.parent = transform;


            //cast a ray from the player forward by some distance and see what we hit
            // Bit shift the index of the layer to get a bit mask
            int layerMask = 1 << 9; //ground

            bool didHit = false;
            RaycastHit hit;

            if (Physics.Raycast(treat.position, Vector3.down, out hit, 200.0f, layerMask))            {
                treat.position = hit.point;               
            }
            else
            {
                Debug.Log("for some crazy reason the tree missed the ground");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
